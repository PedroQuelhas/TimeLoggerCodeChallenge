using AutoMapper;
using ServerApi.CodeGen.Models;
using Timelogger.Model;
using Timelogger.Repos;

namespace Timelogger.Services
{
    public interface ICustomerService : IBaseService<CustomerDTO>
    {
    }
    public class CustomerService : BaseService<CustomerDTO, Customer>, ICustomerService
    {
        public CustomerService(ICustomerRepository repo,IMapper mapper) : base(repo,mapper) 
        {
        }
    }
}
