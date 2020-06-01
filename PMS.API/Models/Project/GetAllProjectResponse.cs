using System;

namespace PMS.API.Models.Project
{
    public class GetAllProjectResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string State { get; set; }
        public ParentProject ParentProject { get; set; }
    }
}
