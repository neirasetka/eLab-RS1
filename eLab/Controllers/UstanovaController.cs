using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eLab.Data;
using eLab.Models;
using Microsoft.AspNetCore.Authorization;

namespace eLab.Controllers
{
    public class UstanovaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UstanovaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ustanova
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ustanova.Include(u => u.Grad);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ustanova/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ustanova = await _context.Ustanova
                .Include(u => u.Grad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ustanova == null)
            {
                return NotFound();
            }

            return View(ustanova);
        }

        // GET: Ustanova/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["GradId"] = new SelectList(_context.Grad, "Id", "Name");
            return View();
        }

        // POST: Ustanova/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Adresa,GradId,Telefon")] Ustanova ustanova)
        {
            // generate POCO
            ustanova.DateCreated = DateTime.UtcNow;
            ustanova.DateUpdated = DateTime.UtcNow;
            ustanova.Timestamp = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                ustanova.Id = Guid.NewGuid();
                _context.Add(ustanova);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradId"] = new SelectList(_context.Grad, "Id", "Id", ustanova.GradId);
            return View(ustanova);
        }

        // GET: Ustanova/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ustanova = await _context.Ustanova.FindAsync(id);
            if (ustanova == null)
            {
                return NotFound();
            }
            ViewData["GradId"] = new SelectList(_context.Grad, "Id", "Name", ustanova.GradId);
            return View(ustanova);
        }

        // POST: Ustanova/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Adresa,GradId,Telefon")] Ustanova ustanova)
        {
            if (id != ustanova.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ustanova.DateUpdated = DateTime.UtcNow;
                    _context.Update(ustanova);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UstanovaExists(ustanova.Id))
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
            ViewData["GradId"] = new SelectList(_context.Grad, "Id", "Id", ustanova.GradId);
            return View(ustanova);
        }

        // GET: Ustanova/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ustanova = await _context.Ustanova
                .Include(u => u.Grad)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ustanova == null)
            {
                return NotFound();
            }

            return View(ustanova);
        }

        // POST: Ustanova/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ustanova = await _context.Ustanova.FindAsync(id);
            _context.Ustanova.Remove(ustanova);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UstanovaExists(Guid id)
        {
            return _context.Ustanova.Any(e => e.Id == id);
        }
    }
}
