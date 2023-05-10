using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timelogger.Model;

namespace Timelogger.Repos
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<RequestResultStatus> AddProjectTimeSlot(Guid projectId, Timeslot timeSlot);
        (IEnumerable<Project> data, int count) GetAllProjectsExpanded(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder);
    }
    public class ProjectRepository : BaseRepository<Project, DbSet<Project>>, IProjectRepository
    {
        public ProjectRepository(ILogger<ProjectRepository> logger, ApiContext ctx) : base(logger, ctx, ctx.Projects)
        {
        }

        public async Task<RequestResultStatus> AddProjectTimeSlot(Guid projectId, Timeslot timeSlot)
        {
            var entity = await DbSet.Include(p=>p.Timeslots).FirstOrDefaultAsync(p=>p.ID==projectId);
            if (entity == null)
                return RequestResultStatus.NOT_FOUND;
            entity.Timeslots ??= new List<Timeslot>();
            entity.Timeslots.Add(timeSlot);
            await Ctx.SaveChangesAsync();
            return RequestResultStatus.SUCCESS;
        }

        public (IEnumerable<Project> data, int count) GetAllProjectsExpanded(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder)
        {
            var (data, count) = GetAll(offset, limit, filterKey, filterValue, sortKey, sortOrder, p => p.Timeslots, p => p.Customer);
            return (data, count);
        }
    }
}
