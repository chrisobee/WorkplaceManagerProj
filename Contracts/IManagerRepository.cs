using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.Contracts
{
    public interface IManagerRepository : IRepositoryBase<Manager>
    {
        void CreateManager(Manager manager);
        void UpdateManager(Manager manager);
        void DeleteManager(Manager manager);
        Task<Manager> GetManagerById(int? managerId);
        Task<Manager> GetManagerByUserId(string userId);

    }

}
