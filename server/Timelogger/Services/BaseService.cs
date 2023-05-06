
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using ServerApi.CodeGen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timelogger.Model;
using Timelogger.Repos;

namespace Timelogger.Services
{
    public interface IBaseService<D> where D : class
    {
        Task<D> Create(D dto);
        Task<RequestResultStatus> Delete(Guid id);
        Task<(D dto, RequestResultStatus status)> Get(Guid id);
        Task<(IEnumerable<D> data, PaginationDTO pagination)> GetAll(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder);
        Task<IEnumerable<D>> SearchAll(int offset, int limit, List<string> filterKey, List<string> filterValue, List<string> searchKey, List<string> searchValue, string sortKey, string sortOrder);
        Task<(D buyout, RequestResultStatus success)> Patch(Guid id, JsonPatchDocument jsonPatch);
    }
    public abstract class BaseService<D, E> : IBaseService<D> where E : BaseEntity where D : class
    {
        protected IBaseRepository<E> Repo { get; }
        protected IMapper Mapper { get; }


        public BaseService(IBaseRepository<E> repo, IMapper mapper)
        {
            Repo = repo;
            Mapper = mapper;
        }


        public virtual async Task<D> Create(D dto)
        {
            var entity = Mapper.Map<E>(dto);
            var created = await Repo.Create(entity);
            return Mapper.Map<D>(created);
        }


        public virtual Task<RequestResultStatus> Delete(Guid id)
        {
            return Repo.Delete(id);
        }

        public virtual async Task<(D dto, RequestResultStatus status)> Get(Guid id)
        {
            var (entity, status) = await Repo.Get(id);
            if (status != RequestResultStatus.SUCCESS)
            {
                return (null, status);
            }
            return (Mapper.Map<D>(entity), status);
        }

        protected (string key, SortOrder order) ParseSortOrder(string sortKey, string sortOrder)
        {
            if (sortKey == null || sortOrder == null)
                return ("ID", SortOrder.DESC);
            var result = Enum.TryParse<SortOrder>(sortOrder, out var val);
            if (!result)
                throw new Exception("Invalid sort order value!");
            return (sortKey, val);
        }

        public virtual Task<(IEnumerable<D> data, PaginationDTO pagination)> GetAll(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder)
        {
            var (key, order) = ParseSortOrder(sortKey, sortOrder);
            var (query, count) = Repo.GetAll(offset, limit, filterKey, filterValue, key, order);
            var pag = new PaginationDTO { Page = offset ?? 0, PerPage = limit ?? 0, TotalRecords = count };
            var data = query.Select(i => Mapper.Map<D>(i));
            return Task.FromResult((data.AsEnumerable(), pag));
        }

        public virtual Task<IEnumerable<D>> SearchAll(int offset, int limit, List<string> filterKey, List<string> filterValue, List<string> searchKey, List<string> searchValue, string sortKey, string sortOrder)
        {
            var (key, order) = ParseSortOrder(sortKey, sortOrder);
            var data = Repo.SearchAll(offset, limit, filterKey, filterValue, searchKey, searchValue, key, order).Select(i => Mapper.Map<D>(i));
            return Task.FromResult(data.AsEnumerable());
        }


        public virtual async Task<(D buyout, RequestResultStatus success)> Patch(Guid id, JsonPatchDocument jsonPatch)
        {
            var res = await Repo.Patch(id, jsonPatch);
            if (res.status == RequestResultStatus.SUCCESS)
            {
                return (Mapper.Map<D>(res.entity), res.status);
            }
            return (null, res.status);
        }
    }
}