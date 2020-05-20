using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class EmployeeJobRepository : RepositoryBase<EmployeeJob>, IEmployeeJobRepository
    {
        public EmployeeJobRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
