using System;

namespace PMS.API.Models.Project
{
    public class AddProjectRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}
