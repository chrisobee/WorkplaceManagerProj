using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.ViewModels
{
    public class ManagerIndexVM
    {
        public Manager Manager { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Project> Projects { get; set; }
        public List<Job> Jobs { get; set; }
    }
}
