using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Timelogger.Model;

namespace Timelogger.Repos
{
    public interface ITimeslotRepository : IBaseRepository<Timeslot>
    {
        public (IEnumerable<Timeslot> query, int count) GetProjectTimeslots(Guid id, int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder);
    }
    public class TimeslotRepository : BaseRepository<Timeslot, DbSet<Timeslot>>, ITimeslotRepository
    {
        public TimeslotRepository(ILogger<TimeslotRepository> logger, ApiContext ctx) : base(logger, ctx, ctx.Timeslots)
        {
        }

        public (IEnumerable<Timeslot> query, int count) GetProjectTimeslots(Guid id, int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder)
        {
            filterKey ??= new List<string>();
            filterValue ??= new List<string>();
            filterKey.Add(nameof(Timeslot.ProjectId));
            filterValue.Add(id.ToString());
            return GetAll(offset, limit, filterKey, filterValue, sortKey, sortOrder);
        }
    }
}
