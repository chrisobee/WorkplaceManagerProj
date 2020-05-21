using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class BranchRepository : RepositoryBase<Branch>, IBranchRepository
    {
        public BranchRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void CreateBranch(Branch branch) => Create(branch);

        public void DeleteBranch(Branch branch) => Delete(branch);

        public async Task<List<Branch>> GetAllBranches(int? seniorManagerId)
        {
            var results = await FindByCondition(b => b.SeniorManagerId == seniorManagerId);
            List<Branch> branches = results.Include(b => b.SeniorManager).ToList();
            return branches;
        }

        public async Task<Branch> GetBranchById(string branchId)
        {
            var result = await FindByCondition(b => b.BranchId == branchId);
            var branch = result.Include(b => b.SeniorManager).SingleOrDefault();
            return branch;
        }

        public void UpdateBranch(Branch branch) => Update(branch);
    }
}
