using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.Contracts
{
    public interface IQualityOfWorkRepository: IRepositoryBase<QualityOfWork>
    {
        void CreateQualityOfWork(double averageQuality, int branchId);
        Task<List<QualityOfWork>> GetQualityOfWorks(int? branchId);
    }
}
