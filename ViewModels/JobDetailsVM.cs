using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.ViewModels
{
    public class JobDetailsVM
    {
        public Job Job { get; set; }
        public List<Subtask> Subtasks { get; set; }
    }
}
