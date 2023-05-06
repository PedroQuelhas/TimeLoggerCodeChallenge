
using ServerApi.CodeGen.Models;
using Timelogger.Services;

namespace Timelogger.Api.Handlers
{
    public interface ICustomerHandler : IBaseHandler<CustomerDTO>
    {
    }
    public class CustomerHandler : BaseHandler<CustomerDTO>, ICustomerHandler
    {
        public CustomerHandler(ICustomerService service) : base(service)
        {
        }
    }
}

