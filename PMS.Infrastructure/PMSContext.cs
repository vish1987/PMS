using Microsoft.EntityFrameworkCore;
using PMS.Domain.ProjectAggregate;
using PMS.Domain.TaskAggregate;
using PMS.Infrastructure.EntityConfigurations;

namespace PMS.Infrastructure
{
    public class PMSContext : DbContext
    {
        public PMSContext(DbContextOptions<PMSContext> options) : base(options)
        {
           
        }

        public const string DEFAULT_SCHEMA = "PMS";
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProjectEntityTypeConfigurations());
            builder.ApplyConfiguration(new TaskEntityTypeConfigurations());
        }
    }
}
