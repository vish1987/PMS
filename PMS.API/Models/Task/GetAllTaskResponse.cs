using System;

namespace PMS.API.Models.Task
{
    public class GetAllTaskResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string State { get; set; }
        public ParentTask ParentTask { get; set; }
        public ParentProject Project { get; set; }
    }

    public class ParentTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
