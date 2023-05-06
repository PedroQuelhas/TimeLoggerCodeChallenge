using AutoMapper;
using ServerApi.CodeGen.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timelogger.Model;
using Timelogger.Repos;

namespace Timelogger.Services
{
    public interface IProjectService : IBaseService<ProjectDTO>
    {
        Task<(IEnumerable<ProjectReportDTO> data, PaginationDTO pagination)> GetProjectsOverview(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder);
        Task<(IEnumerable<TimeslotDTO> data, PaginationDTO pagination)> GetProjectTimeslots(Guid id, int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder);
    }
    public class ProjectService : BaseService<ProjectDTO, Project>, IProjectService
    {
        private readonly IProjectRepository _repo;
        private readonly ITimeslotRepository _timeslotRepository;

        public ProjectService(IProjectRepository repo,ITimeslotRepository timeslotRepository,IMapper mapper) : base(repo,mapper) 
        {
            _repo = repo;
            _timeslotRepository = timeslotRepository;
        }

        public Task<(IEnumerable<ProjectReportDTO> data, PaginationDTO pagination)> GetProjectsOverview(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder)
        {
            var (key, order) = ParseSortOrder(sortKey, sortOrder);
            var (data, count) = _repo.GetAllProjectsExpanded(offset, limit, filterKey, filterValue, key, order);
            var pag = new PaginationDTO { Page = offset ?? 0, PerPage = limit ?? 0, TotalRecords = count };
            var reports = CreateProjectReports(data);
            return Task.FromResult((reports, pag));
        }

        private IEnumerable<ProjectReportDTO> CreateProjectReports(IEnumerable<Project> projects)
        {
            return projects.Select(p => new ProjectReportDTO
            {
                CustomerName = p.Customer.Name,
                ProjectId = p.ID,
                ProjectName = p.Name,
                StartDate = p.StartDate.ToString(),
                EndDate = p.EndDate.ToString(),
                Deadline = p.Deadline.ToString(),
                Completed = p.Completed,
                TotalRecords = p.Timeslots.Count(),
                TotalTime = p.Timeslots.Sum(t=>(int)t.Duration.TotalMinutes)
            });
        }

        public Task<(IEnumerable<TimeslotDTO> data, PaginationDTO pagination)> GetProjectTimeslots(Guid id, int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder)
        {
            var (key, order) = ParseSortOrder(sortKey, sortOrder);
            var (query, count) = _timeslotRepository.GetProjectTimeslots(id, offset, limit, filterKey, filterValue, key, order);
            var pag = new PaginationDTO { Page = offset ?? 0 , PerPage = limit ?? 0 , TotalRecords = count };
            return Task.FromResult((query.Select(i => Mapper.Map<TimeslotDTO>(i)), pag));
        }
    }
}
