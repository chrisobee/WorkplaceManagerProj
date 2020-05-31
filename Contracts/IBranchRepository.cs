using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.Contracts
{
    public interface IBranchRepository : IRepositoryBase<Branch>
    {
        void CreateBranch(Branch branch);
        void UpdateBranch(Branch branch);
        void DeleteBranch(Branch branch);
        Task<Branch> GetBranchById(int? branchId);
        Task<List<Branch>> GetAllBranches(int? seniorManagerId);
    }
}
