﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkplaceManager.Contracts;
using WorkplaceManager.Models;

namespace WorkplaceManager.Data
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public void CreateEmployee(Employee employee) => Create(employee);
        public void DeleteEmployee(Employee employee) => Delete(employee);
        public void UpdateEmployee(Employee employee) => Update(employee);

        public async Task<Employee> GetEmployeeById(int? employeeId)
        {
            var result = await FindByCondition(e => e.EmployeeId == employeeId);
            var employee = result.Include(e => e.IdentityUser).Include(e => e.Manager).SingleOrDefault();
            return employee;
        }

        public async Task<Employee> GetEmployeeWithUserId(string userId)
        {
            var result = await FindByCondition(e => e.IdentityUserId == userId);
            var employee = result.Include(e => e.IdentityUser).Include(e => e.Manager).SingleOrDefault();
            return employee;
        }

        public async Task<List<Employee>> GetAllEmployees(int? managerId)
        {
            var results = await FindByCondition(e => e.ManagerId == managerId);
            List<Employee> employees = results.Include(e => e.IdentityUser).ToList();
            return employees;
        }

        
    }
}
