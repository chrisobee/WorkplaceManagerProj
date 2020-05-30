using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.Contracts
{
    public interface IEmployeeJobRepository : IRepositoryBase<EmployeeJob>
    {
        void CreateEmployeeJob(int employeeId, int jobId);
        Task<List<Job>> FindAssignedTasks(int employeeId);
        Task<Employee> GetAssignedEmployee(int jobId);
    }
}
