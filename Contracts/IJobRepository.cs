using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.Contracts
{
    public interface IJobRepository : IRepositoryBase<Job>
    {
        void CreateJob(Job job);
        void UpdateJob(Job job);
        void DeleteJob(Job job);
        Task<Job> GetJobById(int jobId);
        Task<List<Job>> GetAllJobs(int projectId);
    }
}
