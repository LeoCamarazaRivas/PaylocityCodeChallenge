using Microsoft.EntityFrameworkCore;
using Paylocity.DAL.Data.Model;

namespace Paylocity.DAL.Data
{
    public class PaylocityDbContext : DbContext
    {
        public PaylocityDbContext()
            :base(){ }
    public PaylocityDbContext(DbContextOptions<PaylocityDbContext> options)
            :base(options) { }

        #region DbSet(s)
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }
        #endregion
    }
}
