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
using WorkplaceManager.ViewModels;

namespace WorkplaceManager.Controllers
{
    public class SeniorManagersController : Controller
    {
        private IRepositoryWrapper _repo;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public SeniorManagersController(IRepositoryWrapper repo, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _repo = repo;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: SeniorManagers
        public async Task<IActionResult> Index()
        {
            SeniorManagerIndexVM indexVM = new SeniorManagerIndexVM();

            indexVM.SeniorManager = await GetCurrentSeniorManager();
            if(indexVM.SeniorManager == null)
            {
                return RedirectToAction("Create");
            }
            indexVM.Branches = await _repo.Branch.GetAllBranches(indexVM.SeniorManager.SeniorManagerId);
            await SetBranchesAssignedManagers(indexVM);
            await SetBranchesQualityOfWorks(indexVM.Branches);

            return View(indexVM);
        }

        // GET: SeniorManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seniorManager = await _repo.SeniorManager.GetSeniorManagerById(id);
            if (seniorManager == null)
            {
                return NotFound();
            }

            return View(seniorManager);
        }

        // GET: SeniorManagers/Create
        public IActionResult Create()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.userId = userId;
            return View();
        }

        // POST: SeniorManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SeniorManager seniorManager, string userId)
        {
            if (ModelState.IsValid)
            {
                seniorManager.IdentityUserId = userId;
                _repo.SeniorManager.CreateSeniorManager(seniorManager);
                await _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(seniorManager);
        }

        // GET: Branches/Create
        public async Task<IActionResult> CreateBranch()
        {
            BranchCreateVM createVM = new BranchCreateVM();
            var seniorManager = await GetCurrentSeniorManager();
            ViewBag.seniorManagerId = seniorManager.SeniorManagerId;

            return View(createVM);
        }

        // POST: Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBranch(BranchCreateVM createVM, int seniorManagerId)
        {
            if (ModelState.IsValid)
            {
                //Creating Branch
                createVM.Branch.SeniorManagerId = seniorManagerId;
                _repo.Branch.CreateBranch(createVM.Branch);
                await _repo.Save();

                //Creating Manager
                createVM.Manager.BranchId = createVM.Branch.BranchId;
                string randomInts = GetRandomIntsForPassword();
                await CreateManager(createVM.Manager, randomInts);

                return RedirectToAction("DetailsForManager", new { managerId = createVM.Manager.ManagerId, randomInts });
            }
            return View(createVM);
        }

        // GET: SeniorManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seniorManager = await _repo.SeniorManager.GetSeniorManagerById(id);
            if (seniorManager == null)
            {
                return NotFound();
            }
            return View(seniorManager);
        }

        // POST: SeniorManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SeniorManager seniorManager)
        {
            if (id != seniorManager.SeniorManagerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.SeniorManager.UpdateSeniorManager(seniorManager);
                    await _repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await SeniorManagerExists(seniorManager.SeniorManagerId))
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
            return View(seniorManager);
        }

        // GET: SeniorManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seniorManager = await _repo.SeniorManager.GetSeniorManagerById(id);
            if (seniorManager == null)
            {
                return NotFound();
            }

            return View(seniorManager);
        }

        // POST: SeniorManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seniorManager = await _repo.SeniorManager.GetSeniorManagerById(id);
            _repo.SeniorManager.DeleteSeniorManager(seniorManager);
            await _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SeniorManagerExists(int id)
        {
            try
            {
                var seniorManager = await _repo.SeniorManager.GetSeniorManagerById(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        

        //GET: DetailsForManager
        public async Task<IActionResult> DetailsForManager(int? managerId, string randomInts)
        {
            Manager manager = await _repo.Manager.GetManagerById(managerId);
            string password = $"{manager.FirstName[0]}{manager.LastName[0].ToString().ToLower()}-{randomInts}";
            ViewBag.password = password;
            return View(manager);
        }

        //HELPER METHODS**
        public async Task<SeniorManager> GetCurrentSeniorManager()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            SeniorManager seniorManager = await _repo.SeniorManager.GetSeniorManager(userId);
            return seniorManager;
        }
        public async Task CreateManager(Manager manager, string randomInts)
        {
            _repo.Manager.CreateManager(manager);
            await AddManagerIdentity(manager, randomInts);
            await _repo.Save();
        }

        public async Task AddManagerIdentity(Manager manager, string randomInts)
        {
            //Sets Manager email with Manager name
            string email = $"{manager.FirstName}{manager.LastName}@gmail.com";

            //Generate Random password
            string password = $"{manager.FirstName[0]}{manager.LastName[0].ToString().ToLower()}-{randomInts}";

            //Add Manager info to user table
            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                manager.IdentityUserId = user.Id;
                if(await _roleManager.RoleExistsAsync("Manager"))
                {
                    await _userManager.AddToRoleAsync(user, "Manager");
                }
            }
        }

        public string GetRandomIntsForPassword()
        {
            Random rand = new Random();
            string randomInts = "";
            for (int i = 0; i < 5; i++)
            {
                int tempNum = rand.Next(0, 9);
                randomInts += tempNum.ToString();
            }
            return randomInts;
        }

        public async Task SetBranchesAssignedManagers(SeniorManagerIndexVM indexVM)
        {
            foreach(Branch branch in indexVM.Branches)
            {
                branch.AssignedManager = await _repo.Manager.GetManagerByBranchId(branch.BranchId);
            }
        }

        public async Task SetBranchesQualityOfWorks(List<Branch> branches)
        {
            foreach(Branch branch in branches)
            {
                branch.QualityOfWorks = await _repo.QualityOfWork.GetQualityOfWorks(branch.BranchId);
            }
        }
    }
}
