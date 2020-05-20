using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class SubtaskRepository : RepositoryBase<Subtask>, ISubtaskRepository
    {
        public SubtaskRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
