using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.ProjectAggregate;

namespace PMS.Infrastructure.EntityConfigurations
{
    class ProjectEntityTypeConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("projects", "PMS");
            builder.HasKey(b => b.Id);
        }
    }
}
