using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkplaceManager.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsComplete { get; set; }

        [NotMapped]
        public List<Job> Jobs { get; set; }

        [NotMapped]
        public int PercentComplete { get; set; }

        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}
