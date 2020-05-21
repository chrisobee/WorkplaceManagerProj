using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Models;

namespace WorkplaceManager.Contracts
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Task<Employee> GetEmployeeById(int? id);
        Task<Employee> GetEmployeeWithUserId(string userId);
    }
}
