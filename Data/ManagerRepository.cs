using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class ManagerRepository : RepositoryBase<Manager>, IManagerRepository
    {
        public ManagerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void CreateManager(Manager manager) => Create(manager);

        public async Task<Manager> GetManagerById(int? managerId)
        {
            var result = await FindByCondition(m => m.ManagerId == managerId);
            var manager = result.SingleOrDefault();
            return manager;
        }
    }
}
