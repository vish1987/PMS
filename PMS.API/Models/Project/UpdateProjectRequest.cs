using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.API.Models.Project
{
    public class UpdateProjectRequest
    {
        [Required]
        public int? ProjectId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}
