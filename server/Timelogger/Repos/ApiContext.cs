using Microsoft.EntityFrameworkCore;
using Timelogger.Model;

namespace Timelogger.Repos
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Timeslot> Timeslots { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
