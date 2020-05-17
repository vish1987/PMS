using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.API.Entities
{
    public class Project
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public enum State { get; set; }
    }

    public enum StateType
    {
        Planned,
        InProgress,
        Completed
    }
}
