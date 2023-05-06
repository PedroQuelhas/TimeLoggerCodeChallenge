using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Reflection;

namespace RetailManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenApiController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return File(System.IO.File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "timelogger_api.yaml")), "text/vnd.yaml");
        }
    }
}
