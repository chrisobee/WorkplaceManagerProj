using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.ViewModels
{
    public class EmployeeIndexVM
    {
        public Employee Employee { get; set; }
        public List<Employee> Coworkers { get; set; }
        public List<Project> Projects { get; set; }
    }
}
