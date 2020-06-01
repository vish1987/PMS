using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.API.Models.Project
{
    public class AddSubProjectRequest
    {
        [Required]
        public int? ParentId { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? FinishDate { get; set; }
    }
}
