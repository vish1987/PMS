using PMS.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.API.Models
{
    public class TaskRequest
    {
        public TaskRequest()
        {
            ParentId = -1;
        }

        public int? TaskId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required] 
        public DateTime FinishDate { get; set; }
        [Required]
        public StateType State { get; set; }
        public int? ParentId { get; set; }
    }
}
