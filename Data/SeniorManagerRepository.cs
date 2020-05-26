using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class SeniorManagerRepository : RepositoryBase<SeniorManager>, ISeniorManagerRepository
    {
        public SeniorManagerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void CreateSeniorManager(SeniorManager seniorManager) => Create(seniorManager);

        public void UpdateSeniorManager(SeniorManager seniorManager) => Update(seniorManager);

        public void DeleteSeniorManager(SeniorManager seniorManager) => Delete(seniorManager);

        public async Task<SeniorManager> GetSeniorManager(string userId)
        {
            var result = await FindByCondition(s => s.IdentityUserId == userId);
            var seniorManager = result.SingleOrDefault();
            return seniorManager;
        }

        public async Task<SeniorManager> GetSeniorManagerById(int? seniorManagerId)
        {
            var result = await FindByCondition(s => s.SeniorManagerId == seniorManagerId);
            var seniorManager = result.SingleOrDefault();
            return seniorManager;
        }
    }
}
