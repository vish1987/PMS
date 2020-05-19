using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Domain.ProjectAggregate
{
    class Project
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public StateType State { get; set; }
    }
}
