using AutoMapper;
using ServerApi.CodeGen.Models;
using Timelogger.Model;
using Timelogger.Repos;

namespace Timelogger.Services
{
    public interface ITimeslotService : IBaseService<TimeslotDTO>
    {
    }
    public class TimeslotService : BaseService<TimeslotDTO, Timeslot>, ITimeslotService
    {
        public TimeslotService(ITimeslotRepository repo,IMapper mapper) : base(repo,mapper) 
        {
        }
    }
}
