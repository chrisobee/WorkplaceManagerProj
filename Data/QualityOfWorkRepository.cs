using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class QualityOfWorkRepository:RepositoryBase<QualityOfWork>, IQualityOfWorkRepository
    {
        public QualityOfWorkRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
        }

        public void CreateQualityOfWork(double averageQuality)
        {
            QualityOfWork quality = new QualityOfWork()
            {
                Quality = averageQuality,
                Timestamp = DateTime.Now.Date
            };
            Create(quality);
        }

        public async Task<List<QualityOfWork>> GetQualityOfWorks(int? branchId)
        {
            var results = await FindByCondition(q => q.BranchId == branchId);
            var qualityOfWorks = results.ToList();
            return qualityOfWorks;
        }
    }
}
