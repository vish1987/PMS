using PMS.Domain.TaskAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Domain.ProjectAggregate
{
    public class Project
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public StateType State { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
