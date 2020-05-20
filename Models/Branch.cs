using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkplaceManager.Models
{
    public class Branch
    { 
        [Key]
        public string BranchId { get; set; }
        public string Name { get; set; }

        [ForeignKey("SeniorManager")]
        public int SeniorManagerId { get; set; }
        public SeniorManager SeniorManager { get; set; }
    }
}
