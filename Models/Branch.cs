using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace WorkplaceManager.Models
{
    public class Branch
    { 
        [Key]
        [JsonProperty(PropertyName = "id")]
        public int BranchId { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public Manager AssignedManager { get; set; }
        [NotMapped]
        public List<QualityOfWork> QualityOfWorks { get; set; }

        [ForeignKey("SeniorManager")]
        public int SeniorManagerId { get; set; }
        public SeniorManager SeniorManager { get; set; }
    }
}
