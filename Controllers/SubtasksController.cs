using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkplaceManager.Contracts;
using WorkplaceManager.Data;
using WorkplaceManager.Models;

namespace WorkplaceManager.Controllers
{
    public class SubtasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryWrapper _repo;

        public SubtasksController(ApplicationDbContext context, IRepositoryWrapper repo)
        {
            _context = context;
            _repo = repo;
        }
        // GET: Subtasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtask = await _repo.Subtask.GetSubtaskById(id);
            if (subtask == null)
            {
                return NotFound();
            }

            return View(subtask);
        }

        // GET: Subtasks/Create
        public IActionResult Create(int jobId)
        {
            ViewBag.jobId = jobId;
            return View();
        }

        // POST: Subtasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int jobId, string subtaskName)
        {
            Subtask subtask = new Subtask()
            {
                JobId = jobId,
                Name = subtaskName,
                IsComplete = false
            };
            _repo.Subtask.CreateSubtask(subtask);
            await _repo.Save();

            return RedirectToAction("Details", "Jobs", new { id = jobId });
        }

        public async Task<IActionResult> ChangeCheckStatus(int? subtaskId)
        {
            var subtask = await _repo.Subtask.GetSubtaskById(subtaskId);
            subtask.IsComplete = !subtask.IsComplete;
            await _repo.Save();

            return RedirectToAction("EmployeeJobDetails", "Employees", new { id = subtask.JobId });
        }

        // GET: Subtasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subtask = await _context.Subtasks.FindAsync(id);
            if (subtask == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", subtask.JobId);
            return View(subtask);
        }

        // POST: Subtasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubtaskId,Name,IsComplete,JobId")] Subtask subtask)
        {
            if (id != subtask.SubtaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subtask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubtaskExists(subtask.SubtaskId))
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
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobId", subtask.JobId);
            return View(subtask);
        }

        public async Task<IActionResult> DeleteSubtask(int id)
        {
            var subtask = await _repo.Subtask.GetSubtaskById(id);
            var jobId = subtask.JobId;

            _repo.Subtask.DeleteSubtask(subtask);
            await _repo.Save();

            return RedirectToAction("Details", "Jobs", new { id = jobId });
        }

        private bool SubtaskExists(int id)
        {
            return _context.Subtasks.Any(e => e.SubtaskId == id);
        }
    }
}
