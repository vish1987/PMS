using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Domain.TaskAggregate
{
    class Task
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public StateType State { get; set; }
    }
}
