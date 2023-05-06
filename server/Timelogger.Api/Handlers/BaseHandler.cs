using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timelogger.Api.Utils;
using Timelogger.Services;

namespace Timelogger.Api.Handlers
{
    public interface IBaseHandler<D> where D : class
    {
        Task<IActionResult> Add(D dto);
        Task<IActionResult> Get(Guid id);
        Task<IActionResult> Delete(Guid id);
        Task<IActionResult> Update(Guid id, JsonPatchDocument jsonPatch);
        Task<IActionResult> GetAll(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder);
        Task<IActionResult> SearchAll(int offset, int limit, List<string> filterKey, List<string> filterValue, List<string> searchKey, List<string> searchValue, string sortKey, string sortOrder);
    }

    public abstract class BaseHandler<D> : IBaseHandler<D> where D : class
    {
        protected readonly IBaseService<D> Service;

        public BaseHandler(IBaseService<D> service)
        {
            Service = service;
        }

        public virtual async Task<IActionResult> Add(D dto)
        {
            var created = await Service.Create(dto);
            return new OkObjectResult(created);
        }

        public virtual async Task<IActionResult> Get(Guid id)
        {
            var (dto, status) = await Service.Get(id);
            return status.GetActionResult(dto);
        }

        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var status = await Service.Delete(id);
            return status.GetActionResult();
        }

        public virtual async Task<IActionResult> Update(Guid id, JsonPatchDocument jsonPatch)
        {
            var (dto, status) = await Service.Patch(id, jsonPatch);
            return status.GetActionResult(dto);
        }

        public virtual async Task<IActionResult> GetAll(int? offset, int? limit, List<string> filterKey, List<string> filterValue, string sortKey, string sortOrder)
        {
            var items = await Service.GetAll(offset, limit, filterKey, filterValue,sortKey,sortOrder);
            return new OkObjectResult(new { Data = items.data, Pagination = items.pagination });
        }
        
        public async Task<IActionResult> SearchAll(int offset, int limit, List<string> filterKey, List<string> filterValue, List<string> searchKey, List<string> searchValue, string sortKey, string sortOrder)
        {
            var items = await Service.SearchAll(offset, limit, filterKey, filterValue, searchKey, searchValue, sortKey, sortOrder);
            return new OkObjectResult(items);
        }
    }
}
