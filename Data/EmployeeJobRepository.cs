using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class EmployeeJobRepository : RepositoryBase<EmployeeJob>, IEmployeeJobRepository
    {
        public EmployeeJobRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void CreateEmployeeJob(int employeeId, int jobId)
        {
            EmployeeJob employeeJob = new EmployeeJob()
            {
                EmployeeId = employeeId,
                JobId = jobId
            };
            Create(employeeJob);
        }

        public async Task<List<Job>> FindAssignedTasks(int employeeId)
        {
            var results = await FindByCondition(e => e.EmployeeId == employeeId);
            var jobs = results.Select(e => e.Job).ToList();
            return jobs;
        }
    }
}
