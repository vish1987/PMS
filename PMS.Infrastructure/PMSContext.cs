using Microsoft.EntityFrameworkCore;
using PMS.Domain.ProjectAggregate;
using PMS.Domain.TaskAggregate;

namespace PMS.Infrastructure
{
    public class PMSContext : DbContext
    {
        public PMSContext(DbContextOptions<PMSContext> options) : base(options)
        {

        }

        public const string DEFAULT_SCHEMA = "pms";
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }




}
