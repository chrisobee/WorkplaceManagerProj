using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.Contracts
{
    public interface ISeniorManagerRepository : IRepositoryBase<SeniorManager>
    {
        void CreateSeniorManager(SeniorManager seniorManager);
        void UpdateSeniorManager(SeniorManager seniorManager);
        void DeleteSeniorManager(SeniorManager seniorManager);
        Task<SeniorManager> GetSeniorManager(string userId);
        Task<SeniorManager> GetSeniorManagerById(int? seniorManagerId);
    }
}
