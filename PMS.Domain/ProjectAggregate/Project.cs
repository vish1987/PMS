using PMS.Domain.TaskAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PMS.Domain.ProjectAggregate
{
    public class Project
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        [NotMapped]
        public StateType StateType { get; set; }
        public int? ParentId { get; set; }
        public Project ParentProject { get; set; }

        public List<Project> SubProjects { get; set; }
        public List<Task> Tasks { get; set; }

        public StateType CalcualteState(List<Task> tasks)
        {
            if (!tasks.Any(task => task.State == StateType.Planned || task.State == StateType.InProgress))
                return StateType.Completed;
            else if (tasks.Any(task => task.State == StateType.InProgress))
                return StateType.InProgress;
            else
                return StateType.Planned;
        }
    }
}
