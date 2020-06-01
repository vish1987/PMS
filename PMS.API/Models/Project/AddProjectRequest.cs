using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.API.Models.Project
{
    public class AddProjectRequest
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime FinishDate { get; set; }
    }
}
