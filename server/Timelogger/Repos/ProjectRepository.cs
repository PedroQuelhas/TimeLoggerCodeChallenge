using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Timelogger.Model;

namespace Timelogger.Repos
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        (IEnumerable<Project> data, int count) GetAllProjectsExpanded(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder);
    }
    public class ProjectRepository : BaseRepository<Project, DbSet<Project>>, IProjectRepository
    {
        public ProjectRepository(ILogger<ProjectRepository> logger, ApiContext ctx) : base(logger, ctx, ctx.Projects)
        {
        }

        public (IEnumerable<Project> data, int count) GetAllProjectsExpanded(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder)
        {
            var (data, count) = GetAll(offset, limit, filterKey, filterValue, sortKey, sortOrder, p=>p.Timeslots,p=>p.Customer);
            return (data, count);
        }
    }
}
