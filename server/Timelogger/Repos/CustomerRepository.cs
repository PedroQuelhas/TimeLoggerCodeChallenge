using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Timelogger.Model;

namespace Timelogger.Repos
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {

    }
    public class CustomerRepository : BaseRepository<Customer, DbSet<Customer>>, ICustomerRepository
    {
        public CustomerRepository(ILogger<CustomerRepository> logger, ApiContext ctx) : base(logger, ctx, ctx.Customers)
        {
        }
    }
}
