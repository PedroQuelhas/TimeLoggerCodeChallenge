using Microsoft.AspNetCore.Mvc;
using Timelogger.Model;

namespace Timelogger.Api.Utils
{
    public static class RequestResultStatusExtensions
    {
        public static IActionResult GetActionResult(this RequestResultStatus status)
        {
            switch (status)
            {
                case RequestResultStatus.CONFLICT:
                    return new ConflictResult();
                case RequestResultStatus.NOT_FOUND:
                    return new NotFoundResult();
                case RequestResultStatus.BAD_REQUEST:
                    return new BadRequestResult();
                case RequestResultStatus.ERROR:
                    return new StatusCodeResult(500);
                default:
                    return new OkResult();
            }
        }

        public static IActionResult GetActionResult<T>(this RequestResultStatus status, T okResult)
        {
            switch (status)
            {
                case RequestResultStatus.CONFLICT:
                    return new ConflictResult();
                case RequestResultStatus.NOT_FOUND:
                    return new NotFoundResult();
                case RequestResultStatus.BAD_REQUEST:
                    return new BadRequestResult();
                case RequestResultStatus.ERROR:
                    return new StatusCodeResult(500);
                default:
                    return new OkObjectResult(okResult);
            }
        }
    }
}
