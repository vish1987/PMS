using System;

namespace PMS.Domain.TaskAggregate
{
    public class Task
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public StateType State { get; set; }
    }
}
