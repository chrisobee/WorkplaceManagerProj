using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkplaceManager.Models
{
    public class QualityOfWork
    {
        [Key]
        public int QualityOfWorkId { get; set; }
        public double Quality { get; set; }
        public DateTime? Timestamp { get; set; }
        [ForeignKey("Branch")]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
