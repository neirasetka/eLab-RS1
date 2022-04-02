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
    public class ReferentneVrijednostiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReferentneVrijednostiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReferentneVrijednosti
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReferentneVrijednosti.ToListAsync());
        }

        // GET: ReferentneVrijednosti/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referentneVrijednosti = await _context.ReferentneVrijednosti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referentneVrijednosti == null)
            {
                return NotFound();
            }

            return View(referentneVrijednosti);
        }

        // GET: ReferentneVrijednosti/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReferentneVrijednosti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,MinRange,MaxRange,Unit")] ReferentneVrijednosti referentneVrijednosti)
        {
            if (ModelState.IsValid)
            {
                referentneVrijednosti.DateCreated = DateTime.UtcNow;
                referentneVrijednosti.DateUpdated = DateTime.UtcNow;
                referentneVrijednosti.Timestamp = DateTime.UtcNow;
                referentneVrijednosti.Id = Guid.NewGuid();
                _context.Add(referentneVrijednosti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(referentneVrijednosti);
        }

        // GET: ReferentneVrijednosti/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referentneVrijednosti = await _context.ReferentneVrijednosti.FindAsync(id);
            if (referentneVrijednosti == null)
            {
                return NotFound();
            }
            return View(referentneVrijednosti);
        }

        // POST: ReferentneVrijednosti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,MinRange,MaxRange,Unit")] ReferentneVrijednosti referentneVrijednosti)
        {
            if (id != referentneVrijednosti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    referentneVrijednosti.DateUpdated = DateTime.UtcNow;
                    _context.Update(referentneVrijednosti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferentneVrijednostiExists(referentneVrijednosti.Id))
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
            return View(referentneVrijednosti);
        }

        // GET: ReferentneVrijednosti/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referentneVrijednosti = await _context.ReferentneVrijednosti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referentneVrijednosti == null)
            {
                return NotFound();
            }

            return View(referentneVrijednosti);
        }

        // POST: ReferentneVrijednosti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var referentneVrijednosti = await _context.ReferentneVrijednosti.FindAsync(id);
            _context.ReferentneVrijednosti.Remove(referentneVrijednosti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferentneVrijednostiExists(Guid id)
        {
            return _context.ReferentneVrijednosti.Any(e => e.Id == id);
        }
    }
}
