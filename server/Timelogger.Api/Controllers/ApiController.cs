using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServerApi.CodeGen.Controllers;
using ServerApi.CodeGen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Timelogger.Api.Handlers;

namespace Timelogger.Api.Controllers
{
    public class ApiController : DefaultApiController
    {
        private readonly IProjectHandler _projectHandler;
        private readonly ICustomerHandler _customerHandler;

        public ApiController(IProjectHandler projectHandler, ICustomerHandler customerHandler)
        {
            _projectHandler = projectHandler;
            _customerHandler = customerHandler;
        }
        public override Task<IActionResult> AddCustomer([FromBody] CustomerDTO customerDTO)
            => _customerHandler.Add(customerDTO);

        public override Task<IActionResult> AddProject([FromBody] ProjectDTO projectDTO)
            => _projectHandler.Add(projectDTO);

        public override Task<IActionResult> AddProjectTimeslot([FromRoute(Name = "id"), Required] Guid id, [FromBody] TimeslotDTO timeslotDTO)
            => throw new NotImplementedException();
        public override Task<IActionResult> DeleteCustomer([FromRoute(Name = "id"), Required] Guid id)
            => _customerHandler.Delete(id);


        public override Task<IActionResult> DeleteProject([FromRoute(Name = "id"), Required] Guid id)
            => _projectHandler.Delete(id);

        public override Task<IActionResult> GetCustomer([FromRoute(Name = "id"), Required] Guid id)
            => _customerHandler.Get(id);


        public override Task<IActionResult> GetCustomers([FromQuery(Name = "offset")] int? offset, [FromQuery(Name = "limit")] int? limit, [FromQuery(Name = "filter key")] List<string> filterKey, [FromQuery(Name = "filter value")] List<string> filterValue, [FromQuery(Name = "sort-key")] string sortKey, [FromQuery(Name = "sort-order")] string sortOrder)
            => _customerHandler.GetAll(offset, limit, filterKey, filterValue, sortKey, sortOrder);

        public override Task<IActionResult> GetProject([FromRoute(Name = "id"), Required] Guid id)
            => _projectHandler.Get(id);


        public override Task<IActionResult> GetProjects([FromQuery(Name = "offset")] int? offset, [FromQuery(Name = "limit")] int? limit, [FromQuery(Name = "filter key")] List<string> filterKey, [FromQuery(Name = "filter value")] List<string> filterValue, [FromQuery(Name = "sort-key")] string sortKey, [FromQuery(Name = "sort-order")] string sortOrder)
            => _projectHandler.GetAll(offset, limit, filterKey, filterValue, sortKey, sortOrder);

        public override Task<IActionResult> GetProjectsOverview([FromQuery(Name = "offset")] int? offset, [FromQuery(Name = "limit")] int? limit, [FromQuery(Name = "filter key")] List<string> filterKey, [FromQuery(Name = "filter value")] List<string> filterValue, [FromQuery(Name = "sort-key")] string sortKey, [FromQuery(Name = "sort-order")] string sortOrder)
            => _projectHandler.GetProjectsOverview(offset, limit, filterKey, filterValue, sortKey, sortOrder);

        public override Task<IActionResult> GetProjectTimeslots([FromRoute(Name = "id"), Required] Guid id, [FromQuery(Name = "offset")] int? offset, [FromQuery(Name = "limit")] int? limit, [FromQuery(Name = "filter key")] List<string> filterKey, [FromQuery(Name = "filter value")] List<string> filterValue, [FromQuery(Name = "sort-key")] string sortKey, [FromQuery(Name = "sort-order")] string sortOrder)
            => _projectHandler.GetProjectTimeslots(id, offset, limit, filterKey, filterValue, sortKey, sortOrder);


        public override Task<IActionResult> SearchProjects([FromQuery(Name = "offset"), Required] int offset, [FromQuery(Name = "limit"), Required] int limit, [FromQuery(Name = "search-key"), Required] List<string> searchKey, [FromQuery(Name = "search-value"), Required] List<string> searchValue, [FromQuery(Name = "filter key")] List<string> filterKey, [FromQuery(Name = "filter value")] List<string> filterValue, [FromQuery(Name = "sort-key")] string sortKey, [FromQuery(Name = "sort-order")] string sortOrder)
            => _projectHandler.SearchAll(offset, limit, filterKey, filterValue, searchKey, searchValue, sortKey, sortOrder);


        public override async Task<IActionResult> UpdateCustomer([FromRoute(Name = "id"), Required] Guid id, [FromBody] object body)
        {
            var patchDocument = JsonConvert.DeserializeObject<JsonPatchDocument>(JsonConvert.SerializeObject(body));
            return await _customerHandler.Update(id, patchDocument);
        }

        public override async Task<IActionResult> UpdateProject([FromRoute(Name = "id"), Required] Guid id, [FromBody] object body)
        {
            var patchDocument = JsonConvert.DeserializeObject<JsonPatchDocument>(JsonConvert.SerializeObject(body));
            return await _projectHandler.Update(id, patchDocument);
        }
    }
}
