using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Domain.TaskAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Infrastructure.EntityConfigurations
{
    class TaskEntityTypeConfigurations : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("tasks", "PMS");
            builder.HasKey(b => b.Id);
        }
    }
}
