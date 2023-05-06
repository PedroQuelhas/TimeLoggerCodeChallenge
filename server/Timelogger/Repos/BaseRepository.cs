using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Timelogger.Model;

namespace Timelogger.Repos
{
    public enum SortOrder
    {
        ASC,
        DESC
    }
    public interface IBaseRepository<I> where I : BaseEntity
    {
        Task<I> Create(I entity);
        Task<(I entity, RequestResultStatus status)> Update(I entity);
        Task<RequestResultStatus> Delete(Guid id);
        Task<(I entity, RequestResultStatus status)> Get(Guid id);
        Task<(I entity, RequestResultStatus status)> Patch(Guid id, JsonPatchDocument jsonPatch);
        (IQueryable<I> query, int count) GetAll(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, SortOrder sortOrder, params Expression<Func<I, object>>[] includes);
        IQueryable<I> SearchAll(int offset, int limit, List<string> filterKey, List<string> filterValue, List<string> searchKey, List<string> searchValue, string sortKey, SortOrder sortOrder);
    }

    public abstract class BaseRepository<I, T> : IBaseRepository<I> where I : BaseEntity where T : DbSet<I>
    {
        private readonly ILogger _logger;
        protected readonly ApiContext Ctx;
        protected readonly T DbSet;

        public BaseRepository(ILogger logger, ApiContext ctx, T dbSet)
        {
            _logger = logger;
            Ctx = ctx;
            DbSet = dbSet;
        }

        public virtual async Task<I> Create(I entity)
        {
            await DbSet.AddAsync(entity);
            await Ctx.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<(I entity, RequestResultStatus status)> Update(I entity)
        {
            DbSet.Update(entity);
            try
            {
                await Ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var current = await DbSet.FindAsync(entity.ID);
                var msg = $"{nameof(T)} is not up to date, aborting! Submited version {entity.Version}, current version {current?.Version}";
                _logger.LogWarning(msg);
                return (current, RequestResultStatus.CONFLICT);
            }
            return (entity, RequestResultStatus.SUCCESS);
        }

        public virtual async Task<(I entity, RequestResultStatus status)> Patch(Guid id, JsonPatchDocument jsonPatch)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
                return (null, RequestResultStatus.NOT_FOUND);

            jsonPatch.ApplyTo(entity);
            try
            {
                await Ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var current = await DbSet.FindAsync(id);
                return (current, RequestResultStatus.CONFLICT);
            }
            return (entity, RequestResultStatus.SUCCESS);
        }

        public virtual async Task<RequestResultStatus> Delete(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
            {
                var msg = $"{nameof(T)} with ID {id} does not exist!";
                _logger.LogWarning(msg);
                return RequestResultStatus.NOT_FOUND;
            }
            DbSet.Remove(entity);
            await Ctx.SaveChangesAsync();
            return RequestResultStatus.SUCCESS;
        }

        public virtual async Task<(I entity, RequestResultStatus status)> Get(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
            {
                var msg = $"{nameof(T)} with ID {id} does not exist!";
                _logger.LogWarning(msg);
                return (null, RequestResultStatus.NOT_FOUND);
            }
            return (entity, RequestResultStatus.SUCCESS);
        }


        public virtual (IQueryable<I> query, int count) GetAll(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, SortOrder sortOrder, params Expression<Func<I, object>>[] includes)
        {
            var query = DbSet.AsQueryable<I>();
            query = Expand(query,includes);
            var res = ApplyFilter(query, offset, limit, filterKey, filterValue);
            var ordered = OrderResults(res.query, sortKey, sortOrder);
            return (ordered, res.count);
        }

        public virtual IQueryable<I> SearchAll(int offset, int limit, List<string> filterKey, List<string> filterValue, List<string> searchKey, List<string> searchValue, string sortKey, SortOrder sortOrder)
        {
            var query = DbSet.AsQueryable<I>();
            query = Filter(query, filterKey, filterValue);
            query = Search(query, searchKey, searchValue);
            query = OrderResults(query, sortKey, sortOrder);
            return query.Skip(offset).Take(limit);
        }

        //for reference https://gist.github.com/oneillci/3205384
        private static IQueryable<I> Expand(IQueryable<I> query, params Expression<Func<I, object>>[] includes)
        {
            return includes.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        private static IQueryable<I> OrderResults(IQueryable<I> query, string sortKey, SortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case SortOrder.ASC:
                    return query.OrderBy(q => EF.Property<Guid>(q, sortKey));
                case SortOrder.DESC:
                    return query.OrderByDescending(q => EF.Property<Guid>(q, sortKey));
            }
            throw new Exception("Invalid sort parametters");
        }

        private static (IQueryable<I> query, int count) ApplyFilter(IQueryable<I> query, int? offset, int? limit, List<string> filterKey, List<string> filterValue)
        {
            query = Filter(query, filterKey, filterValue);
            var count = query.Count();
            if (offset != null)
                query = query.Skip(offset.Value);
            if (limit != null)
                query = query.Take(limit.Value);

            return (query, count);
        }
        private static IQueryable<I> Filter(IQueryable<I> query, List<string> filterKey, List<string> filterValue)
        {
            if (filterKey != null && filterValue != null)
            {
                if (filterKey.Count != filterValue.Count)
                    throw new Exception("Invalid input filters. filter keys need to be the same number as filter values");
                for (var i = 0; i < filterKey.Count; i++)
                {
                    var idx = i;
                    Guid output;
                    bool isValid = Guid.TryParse(filterValue[idx], out output);
                    if (isValid)
                    {
                        query = query.Where(p => EF.Property<Guid>(p, filterKey[idx]) == output);
                    }
                    else
                    {
                        query = query.Where(p => EF.Property<string>(p, filterKey[idx]) == filterValue[idx]);
                    }

                }
            }
            return query;
        }

        private static IQueryable<I> Search(IQueryable<I> query, List<string> filterKey, List<string> filterValue)
        {
            if (filterKey != null && filterValue != null)
            {
                if (filterKey.Count != filterValue.Count)
                    throw new Exception("Invalid input filters. filter keys need to be the same number as filter values");
                for (var i = 0; i < filterKey.Count; i++)
                {
                    var idx = i;
                    var value = filterValue[idx].ToLower();
                    query = query.Where(p => EF.Property<string>(p, filterKey[idx]).ToLower().Contains(value));
                }
            }
            return query;
        }

    }
}
