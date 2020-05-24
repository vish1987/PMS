using System;

namespace PMS.API.Models
{
    public class ProjectRequest
    {
        public ProjectRequest()
        {
            ParentId = -1;
        }

        public int? ProjectId { get;set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int? ParentId { get; set; }

    }
}
