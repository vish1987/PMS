using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PMS.Infrastructure
{
    class PMSContextFactory : IDesignTimeDbContextFactory<PMSContext>
    {
        public PMSContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PMSContext>()
                 .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=PMS;Integrated Security=true");

            return new PMSContext(optionsBuilder.Options);
        }
    }
}
