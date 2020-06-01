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

        public async Task<bool> CheckIfJobIsAssigned(int employeeId, int jobId)
        {
            var results = await FindByCondition(j => j.EmployeeId == employeeId && j.JobId == jobId);
            var taskAlreadyAssigned = results.ToList();
            return taskAlreadyAssigned.Count > 0;
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

        public async Task<Employee> GetAssignedEmployee(int jobId)
        {
            var result = await FindByCondition(e => e.JobId == jobId);
            var employee = result.Select(e => e.Employee).FirstOrDefault();
            return employee;
        }
    }
}
