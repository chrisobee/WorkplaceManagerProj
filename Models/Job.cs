using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkplaceManager.Models
{
    public class Job
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string JobId { get; set; }
        public string Name { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsComplete { get; set; }

        [ForeignKey("Project")]
        public string ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
