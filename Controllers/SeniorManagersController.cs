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
    public class SeniorManagersController : Controller
    {
        private IRepositoryWrapper _repo;

        public SeniorManagersController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET: SeniorManagers
        public async Task<IActionResult> Index()
        {
            return View();
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
                _repo.SeniorManager.CreateSeniorManager(seniorManager);
                await _repo.Save();
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
    }
}
