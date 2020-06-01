using PMS.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.API.Models.Task
{
    public class UpdateTaskRequest
    {
        [Required]
        public int? TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public StateType? State { get; set; }
    }
}
