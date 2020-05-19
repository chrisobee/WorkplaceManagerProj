﻿using System;
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

        [ForeignKey("Job")]
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
