using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class JobRepository : RepositoryBase<Job>, IJobRepository
    {
        public JobRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void CreateJob(Job job) => Create(job);

        public void DeleteJob(Job job) => Delete(job);

        public async Task<List<Job>> GetAllJobs(string projectId)
        {
            var results = await FindByCondition(j => j.ProjectId == projectId);
            var jobs = results.Include(j => j.Project).ToList();
            return jobs;
        }

        public async Task<Job> GetJobById(string jobId)
        {
            var result = await FindByCondition(j => j.JobId == jobId);
            var job = result.Include(j => j.Project).SingleOrDefault();
            return job;
        }

        public void UpdateJob(Job job) => Update(job);
    }
}
