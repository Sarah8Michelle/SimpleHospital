using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleHospital.Data;
using SimpleHospital.Models;

namespace SimpleHospital.Controllers
{
    public class CollaboratorTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollaboratorTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CollaboratorTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CollaboratorType.ToListAsync());
        }

        // GET: CollaboratorTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboratorType = await _context.CollaboratorType
                .FirstOrDefaultAsync(m => m.CollaboratorTypeId == id);
            if (collaboratorType == null)
            {
                return NotFound();
            }

            return View(collaboratorType);
        }

        // GET: CollaboratorTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CollaboratorTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CollaboratorTypeId,CollaboratorTypes,Department")] CollaboratorType collaboratorType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collaboratorType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(collaboratorType);
        }

        // GET: CollaboratorTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboratorType = await _context.CollaboratorType.FindAsync(id);
            if (collaboratorType == null)
            {
                return NotFound();
            }
            return View(collaboratorType);
        }

        // POST: CollaboratorTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CollaboratorTypeId,CollaboratorTypes,Department")] CollaboratorType collaboratorType)
        {
            if (id != collaboratorType.CollaboratorTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collaboratorType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaboratorTypeExists(collaboratorType.CollaboratorTypeId))
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
            return View(collaboratorType);
        }

        // GET: CollaboratorTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaboratorType = await _context.CollaboratorType
                .FirstOrDefaultAsync(m => m.CollaboratorTypeId == id);
            if (collaboratorType == null)
            {
                return NotFound();
            }

            return View(collaboratorType);
        }

        // POST: CollaboratorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collaboratorType = await _context.CollaboratorType.FindAsync(id);
            _context.CollaboratorType.Remove(collaboratorType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollaboratorTypeExists(int id)
        {
            return _context.CollaboratorType.Any(e => e.CollaboratorTypeId == id);
        }
    }
}
