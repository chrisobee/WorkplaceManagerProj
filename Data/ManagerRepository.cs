using Microsoft.EntityFrameworkCore;
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
        public void UpdateManager(Manager manager) => Update(manager);
        public void DeleteManager(Manager manager) => Delete(manager);

        public async Task<Manager> GetManagerById(int? managerId)
        {
            var result = await FindByCondition(m => m.ManagerId == managerId);
            var manager = result.Include(m => m.IdentityUser).SingleOrDefault();
            return manager;
        }

        public async Task<Manager> GetManagerByUserId(string userId)
        {
            var result = await FindByCondition(m => m.IdentityUserId == userId);
            var manager = result.Include(m => m.IdentityUser).SingleOrDefault();
            return manager;
        }

        public async Task<List<Manager>> GetAllManagers(List<int?> branchIds)
        {
            var results = await FindByCondition(m => branchIds.Contains(m.BranchId));
            var managers = results.ToList();
            return managers;
        }

        public async Task<Manager> GetManagerByBranchId(int? branchId)
        {
            var result = await FindByCondition(m => m.BranchId == branchId);
            var manager = result.FirstOrDefault();
            return manager;
        }
    }
}
