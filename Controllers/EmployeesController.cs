using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkplaceManager.Contracts;
using WorkplaceManager.Data;
using WorkplaceManager.Models;
using WorkplaceManager.ViewModels;

namespace WorkplaceManager.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IRepositoryWrapper _repo;

        public EmployeesController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            EmployeeIndexVM indexVM = new EmployeeIndexVM();

            indexVM.Employee = await GetCurrentUser();
            indexVM.Coworkers = await _repo.Employee.GetAllEmployees(indexVM.Employee.ManagerId);
            indexVM.Projects = await _repo.Project.GetAllProjects(indexVM.Employee.ManagerId);
            await GetJobsForProjects(indexVM);
            await GetCoworkersAssignedJobs(indexVM);
            GetPercentageOfTasksDone(indexVM);

            indexVM.Coworkers.Remove(indexVM.Employee);
            return View(indexVM);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _repo.Employee.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public async Task<IActionResult> EmployeeJobDetails(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            JobDetailsVM detailsVM = new JobDetailsVM();
            detailsVM.Job = await _repo.Job.GetJobById(id);

            if(detailsVM.Job == null)
            {
                return NotFound();
            }

            detailsVM.Subtasks = await _repo.Subtask.GetAllSubtasks(detailsVM.Job.JobId);

            return View(detailsVM);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _repo.Employee.CreateEmployee(employee);
                await _repo.Save();
                return RedirectToAction("RequireLink", new { employeeId = employee.EmployeeId});
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _repo.Employee.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Employee.UpdateEmployee(employee);
                    await _repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _repo.Employee.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _repo.Employee.GetEmployeeById(id);
            _repo.Employee.DeleteEmployee(employee);
            await _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            try
            {
                _repo.Employee.GetEmployeeById(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //HELPER METHODS**
        public async Task<Employee> GetCurrentUser()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var employee = await _repo.Employee.GetEmployeeWithUserId(userId);
            return employee;
        }

        public async Task GetJobsForProjects(EmployeeIndexVM indexVM)
        {
            foreach(Project project in indexVM.Projects)
            {
                project.Jobs = await _repo.Job.GetAllJobs(project.ProjectId);
            }
        }

        public async Task GetCoworkersAssignedJobs(EmployeeIndexVM indexVM)
        {
            foreach(Employee employee in indexVM.Coworkers)
            {
                employee.AssignedJobs = await _repo.EmployeeJob.FindAssignedTasks(employee.EmployeeId);
            }
        }

        public void GetPercentageOfTasksDone(EmployeeIndexVM indexVM)
        {
            foreach (Project project in indexVM.Projects)
            {
                double jobsComplete = 0;
                double totalJobs = project.Jobs.Count();

                if (totalJobs == 0)
                {
                    continue;
                }

                foreach (Job job in project.Jobs)
                {
                    if (job.IsComplete)
                    {
                        jobsComplete++;
                    }
                }
                double result = (jobsComplete / totalJobs) * 100;
                project.PercentComplete = (int)Math.Round(result);
            }
        }
    }
}
