
using Microsoft.AspNetCore.Mvc;
using ServerApi.CodeGen.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timelogger.Services;

namespace Timelogger.Api.Handlers
{
    public interface IProjectHandler : IBaseHandler<ProjectDTO>
    {
        Task<IActionResult> GetProjectsOverview(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder);
        Task<IActionResult> GetProjectTimeslots(Guid id, int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder);
    }
    public class ProjectHandler : BaseHandler<ProjectDTO>, IProjectHandler
    {
        private readonly IProjectService _service;

        public ProjectHandler(IProjectService service) : base(service)
        {
            _service = service;
        }

        public async Task<IActionResult> GetProjectsOverview(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder)
        {
            var (data, pagination) = await _service.GetProjectsOverview(offset, limit, filterKey, filterValue, sortKey, sortOrder);
            return new OkObjectResult(new { Data = data, Pagination = pagination });
        }

        public async Task<IActionResult> GetProjectTimeslots(Guid id, int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder)
        {
            var (data, pagination) = await _service.GetProjectTimeslots(id, offset, limit, filterKey, filterValue, sortKey, sortOrder);
            return new OkObjectResult(new { Data = data, Pagination = pagination });
        }
    }
}

