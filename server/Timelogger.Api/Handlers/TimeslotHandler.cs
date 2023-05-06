
using ServerApi.CodeGen.Models;
using Timelogger.Services;

namespace Timelogger.Api.Handlers
{
    public interface ITimeslotHandler : IBaseHandler<TimeslotDTO>
    {
    }
    public class TimeslotHandler : BaseHandler<TimeslotDTO>, ITimeslotHandler
    {
        public TimeslotHandler(ITimeslotService service) : base(service)
        {
        }
    }
}

