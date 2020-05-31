using PMS.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.API.Models.Task
{
    public class AddSubTaskRequest
    {
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
        [Required]
        public int ParentId { get; set; }
    }
}
