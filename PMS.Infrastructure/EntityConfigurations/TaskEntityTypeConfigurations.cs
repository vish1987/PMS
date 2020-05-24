using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.TaskAggregate;

namespace PMS.Infrastructure.EntityConfigurations
{
    public class TaskEntityTypeConfigurations : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("tasks", PMSContext.DEFAULT_SCHEMA);
            builder.HasKey(b => b.Id);

            builder.HasOne(x => x.Project)
                .WithMany(w => w.Tasks)
                .HasForeignKey(h => h.ProjectId);

            builder.HasMany(p => p.SubTasks)
                .WithOne(p => p.ParentTask)
                .HasForeignKey(h => h.ParentId);
        }
    }
}
