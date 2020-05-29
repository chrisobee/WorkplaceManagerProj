using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkplaceManager.Contracts;
using WorkplaceManager.Data;
using WorkplaceManager.Models;
using WorkplaceManager.Services;
using WorkplaceManager.ViewModels;

namespace WorkplaceManager.Controllers
{
    public class ManagersController : Controller
    {
        private IRepositoryWrapper _repo;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public ManagersController(IRepositoryWrapper repo, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _repo = repo;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        
        //ALL METHODS WITH VIEWS**

        // GET: Managers
        public async Task<IActionResult> Index()
        {
            ManagerIndexVM indexVM = new ManagerIndexVM();
            var manager = await GetCurrentUser();
            if(manager == null)
            {
                return RedirectToAction("Create");
            }

            indexVM.Manager = manager;
            indexVM.Employees = await _repo.Employee.GetAllEmployees(indexVM.Manager.ManagerId);
            indexVM.Projects = await _repo.Project.GetAllProjects(manager.ManagerId);
            await SetEmployeesAssignedTasks(indexVM);
            await GetJobsForEachProject(indexVM);
            GetPercentageOfTasksDone(indexVM);

            return View(indexVM);
        }

        public async Task<IActionResult> AssignEmployeeJob(int employeeId, int jobId)
        {
            _repo.EmployeeJob.CreateEmployeeJob(employeeId, jobId);
            await _repo.Save();
            return RedirectToAction("Index", "Home");
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _repo.Manager.GetManagerById(id);

            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _repo.Manager.GetManagerById(id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Manager manager)
        {
            if (id != manager.ManagerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Manager.UpdateManager(manager);
                    await _repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ManagerExists(manager.ManagerId))
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
            return View(manager);
        }

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _repo.Manager.GetManagerById(id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manager = await _repo.Manager.GetManagerById(id);
            _repo.Manager.DeleteManager(manager);
            await _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ManagerExists(int id)
        {
            try
            {
                await _repo.Manager.GetManagerById(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //GET: Create view for Creating Employee
        public async Task<IActionResult> CreateEmployee()
        {
            var manager = await GetCurrentUser();
            ViewBag.managerId = manager.ManagerId;
            return View();
        }

        //POST: Post for creation of Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee, int? managerId)
        {
            employee.ManagerId = managerId;
            _repo.Employee.CreateEmployee(employee);
            string randomInts = GetRandomIntsForPassword();
            await AddEmployeeIdentity(employee, randomInts);
            await _repo.Save();

            return RedirectToAction("DetailsOfEmployee", new { employeeId = employee.EmployeeId, randomInts });
        }

        //GET: Details of employee to allow manager to get email and password and send it to employee
        public async Task<IActionResult> DetailsOfEmployee(int employeeId, string randomInts)
        {
            var employee = await _repo.Employee.GetEmployeeById(employeeId);
            var password = $"{employee.FirstName[0]}{employee.LastName[0].ToString().ToLower()}-{randomInts}";
            ViewBag.password = password;
            return View(employee);
        }


        //HELPER METHODS**

        public async Task AddEmployeeIdentity(Employee employee, string randomInts)
        {
            //Sets employee email with employee name
            string email = $"{employee.FirstName}{employee.LastName}@gmail.com";

            //Generate Random password
            string password = $"{employee.FirstName[0]}{employee.LastName[0].ToString().ToLower()}-{randomInts}";

            //Add Employee info to user table
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                employee.IdentityUserId = user.Id;
                if(await _roleManager.RoleExistsAsync("Employee"))
                {
                    await _userManager.AddToRoleAsync(user, "Employee");
                }
            }
        }

        public string GetRandomIntsForPassword()
        {
            Random rand = new Random();
            string randomInts = "";
            for(int i = 0; i < 5; i++)
            {
                int tempNum = rand.Next(0, 9);
                randomInts += tempNum.ToString();
            }
            return randomInts;
        }

        public async Task<Manager> GetCurrentUser()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var manager = await _repo.Manager.GetManagerByUserId(userId);
            return manager;
        }

        public async Task SetEmployeesAssignedTasks(ManagerIndexVM indexVM)
        {
            foreach (Employee employee in indexVM.Employees)
            {
                employee.AssignedJobs = await _repo.EmployeeJob.FindAssignedTasks(employee.EmployeeId);
            }
        }

        public async Task GetJobsForEachProject(ManagerIndexVM indexVM)
        {
            foreach(Project project in indexVM.Projects)
            {
                project.Jobs = await _repo.Job.GetAllJobs(project.ProjectId);

                //All jobs so that the jobs can have an employee assigned to it in the index view
                if(indexVM.Jobs == null)
                {
                    indexVM.Jobs = project.Jobs;
                }
                else
                {
                    indexVM.Jobs = indexVM.Jobs.Concat(project.Jobs).ToList();
                }
            }
        }

        public void GetPercentageOfTasksDone(ManagerIndexVM indexVM)
        {
            foreach(Project project in indexVM.Projects)
            {
                double jobsComplete = 0;
                double totalJobs = project.Jobs.Count();

                if(totalJobs == 0)
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
