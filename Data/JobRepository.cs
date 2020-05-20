using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class JobRepository : RepositoryBase<Job>, IJobRepository
    {
        public JobRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
