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

namespace WorkplaceManager.Controllers
{
    public class JobsController : Controller
    {
        private readonly IRepositoryWrapper _repo;

        public JobsController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        //GET: Jobs/UpdateJob/4
        public async Task<IActionResult> UpdateJob(int? id)
        {
            var job = await _repo.Job.GetJobById(id);
            job.IsComplete = !job.IsComplete;
            await _repo.Save();

            return RedirectToAction("Index", "Home");
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _repo.Job.GetJobById(id);

            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create(int? projectId)
        {
            ViewBag.projectId = projectId;
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Job job, int projectId)
        {
            if (ModelState.IsValid)
            {
                job.ProjectId = projectId;
                _repo.Job.CreateJob(job);
                await _repo.Save();
                return RedirectToAction("Index", "Managers");
            }
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _repo.Job.GetJobById(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobId,Name,Deadline,IsComplete,ProjectId")] Job job)
        {
            if (id != job.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Job.UpdateJob(job);
                    await _repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await JobExists(job.JobId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Managers");
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _repo.Job.GetJobById(id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _repo.Job.GetJobById(id);
            _repo.Job.DeleteJob(job);
            await _repo.Save();
            return RedirectToAction("Index", "Managers");
        }

        private async Task<bool> JobExists(int id)
        {
            try
            {
                await _repo.Job.GetJobById(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //HELPER METHODS**
        public async Task<Manager> GetManager()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var manager = await _repo.Manager.GetManagerByUserId(userId);
            return manager;
        }
    }
}
