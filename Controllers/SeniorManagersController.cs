using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkplaceManager.Data;
using WorkplaceManager.Models;

namespace WorkplaceManager.Controllers
{
    public class SeniorManagersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeniorManagersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SeniorManagers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SeniorManagers.Include(s => s.IdentityUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SeniorManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seniorManager = await _context.SeniorManagers
                .Include(s => s.IdentityUser)
                .FirstOrDefaultAsync(m => m.SeniorManagerId == id);
            if (seniorManager == null)
            {
                return NotFound();
            }

            return View(seniorManager);
        }

        // GET: SeniorManagers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SeniorManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SeniorManager seniorManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seniorManager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seniorManager);
        }

        // GET: SeniorManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seniorManager = await _context.SeniorManagers.FindAsync(id);
            if (seniorManager == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", seniorManager.IdentityUserId);
            return View(seniorManager);
        }

        // POST: SeniorManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeniorManagerId,Name,IdentityUserId")] SeniorManager seniorManager)
        {
            if (id != seniorManager.SeniorManagerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seniorManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeniorManagerExists(seniorManager.SeniorManagerId))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", seniorManager.IdentityUserId);
            return View(seniorManager);
        }

        // GET: SeniorManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seniorManager = await _context.SeniorManagers
                .Include(s => s.IdentityUser)
                .FirstOrDefaultAsync(m => m.SeniorManagerId == id);
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
            var seniorManager = await _context.SeniorManagers.FindAsync(id);
            _context.SeniorManagers.Remove(seniorManager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeniorManagerExists(int id)
        {
            return _context.SeniorManagers.Any(e => e.SeniorManagerId == id);
        }
    }
}
