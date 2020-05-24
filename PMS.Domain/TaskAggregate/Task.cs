using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PMS.Domain.ProjectAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Domain.TaskAggregate
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public StateType State { get; set; }

        public int? ParentId { get; set; }
        public Task ParentTask { get; set; }
        public List<Task> SubTasks { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
