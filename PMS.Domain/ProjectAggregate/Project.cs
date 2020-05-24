using PMS.Domain.TaskAggregate;
using System;
using System.Collections.Generic;

namespace PMS.Domain.ProjectAggregate
{
    public class Project
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public StateType StateType { get; set; }

        public int? ParentId { get; set; }
        public Project ParentProject { get; set; }

        public List<Project> SubProjects { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
