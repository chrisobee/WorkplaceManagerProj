using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkplaceManager.Models
{
    public class Subtask
    {
        [Key]
        public int SubtaskId { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }

        [ForeignKey("Job")]
        public string JobId { get; set; }
        public Job Job { get; set; }
    }
}
