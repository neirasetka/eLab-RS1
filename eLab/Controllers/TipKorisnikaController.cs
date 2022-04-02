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
    public class TipKorisnikaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipKorisnikaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipKorisnika
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipKorisnika.ToListAsync());
        }

        // GET: TipKorisnika/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipKorisnika = await _context.TipKorisnika
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipKorisnika == null)
            {
                return NotFound();
            }

            return View(tipKorisnika);
        }

        // GET: TipKorisnikas/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipKorisnikas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Description")] TipKorisnika tipKorisnika)
        {
            // generate POCO
            tipKorisnika.DateCreated = DateTime.UtcNow;
            tipKorisnika.DateUpdated = DateTime.UtcNow;
            tipKorisnika.Timestamp = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                tipKorisnika.Id = Guid.NewGuid();
                _context.Add(tipKorisnika);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipKorisnika);
        }

        // GET: TipKorisnikas/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipKorisnika = await _context.TipKorisnika.FindAsync(id);
            if (tipKorisnika == null)
            {
                return NotFound();
            }
            return View(tipKorisnika);
        }

        // POST: TipKorisnikas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Description")] TipKorisnika tipKorisnika)
        {
            if (id != tipKorisnika.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tipKorisnika.DateUpdated = DateTime.UtcNow;
                    _context.Update(tipKorisnika);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipKorisnikaExists(tipKorisnika.Id))
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
            return View(tipKorisnika);
        }

        // GET: TipKorisnikas/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipKorisnika = await _context.TipKorisnika
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipKorisnika == null)
            {
                return NotFound();
            }

            return View(tipKorisnika);
        }

        // POST: TipKorisnikas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tipKorisnika = await _context.TipKorisnika.FindAsync(id);
            _context.TipKorisnika.Remove(tipKorisnika);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipKorisnikaExists(Guid id)
        {
            return _context.TipKorisnika.Any(e => e.Id == id);
        }
    }
}
