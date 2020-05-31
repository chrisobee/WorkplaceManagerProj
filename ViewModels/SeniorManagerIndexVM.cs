using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.ViewModels
{
    public class SeniorManagerIndexVM
    {
        public SeniorManager SeniorManager { get; set; }
        public List<Branch> Branches { get; set; }
        public List<Manager> Managers { get; set; }
    }
}
