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
    public class CollaboratorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CollaboratorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Collaborators
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Collaborator.Include(c => c.CollaboratorType).Include(c => c.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Collaborators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = await _context.Collaborator
                .Include(c => c.CollaboratorType)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.CollaboratorId == id);
            if (collaborator == null)
            {
                return NotFound();
            }

            return View(collaborator);
        }

        // GET: Collaborators/Create
        public IActionResult Create()
        {
            ViewData["CollaboratorTypeId"] = new SelectList(_context.CollaboratorType, "CollaboratorTypeId", "CollaboratorTypes");
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "Email");
            return View();
        }

        // POST: Collaborators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CollaboratorId,PersonId,CollaboratorTypeId")] Collaborator collaborator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(collaborator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CollaboratorTypeId"] = new SelectList(_context.CollaboratorType, "CollaboratorTypeId", "CollaboratorTypes", collaborator.CollaboratorTypeId);
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "Email", collaborator.PersonId);
            return View(collaborator);
        }

        // GET: Collaborators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = await _context.Collaborator.FindAsync(id);
            if (collaborator == null)
            {
                return NotFound();
            }
            ViewData["CollaboratorTypeId"] = new SelectList(_context.CollaboratorType, "CollaboratorTypeId", "CollaboratorTypes", collaborator.CollaboratorTypeId);
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "Email", collaborator.PersonId);
            return View(collaborator);
        }

        // POST: Collaborators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CollaboratorId,PersonId,CollaboratorTypeId")] Collaborator collaborator)
        {
            if (id != collaborator.CollaboratorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collaborator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollaboratorExists(collaborator.CollaboratorId))
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
            ViewData["CollaboratorTypeId"] = new SelectList(_context.CollaboratorType, "CollaboratorTypeId", "CollaboratorTypes", collaborator.CollaboratorTypeId);
            ViewData["PersonId"] = new SelectList(_context.Person, "PersonId", "Email", collaborator.PersonId);
            return View(collaborator);
        }

        // GET: Collaborators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var collaborator = await _context.Collaborator
                .Include(c => c.CollaboratorType)
                .Include(c => c.Person)
                .FirstOrDefaultAsync(m => m.CollaboratorId == id);
            if (collaborator == null)
            {
                return NotFound();
            }

            return View(collaborator);
        }

        // POST: Collaborators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collaborator = await _context.Collaborator.FindAsync(id);
            _context.Collaborator.Remove(collaborator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollaboratorExists(int id)
        {
            return _context.Collaborator.Any(e => e.CollaboratorId == id);
        }
    }
}
