using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.ProjectAggregate;

namespace PMS.Infrastructure.EntityConfigurations
{
    public class ProjectEntityTypeConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("projects", PMSContext.DEFAULT_SCHEMA);
            builder.HasKey(b => b.Id);

            builder.HasMany(p => p.SubProjects)
                .WithOne(p => p.ParentProject)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
