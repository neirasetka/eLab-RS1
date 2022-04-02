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
    public class TipUzorkaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipUzorkaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipUzorka
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipUzorka.ToListAsync());
        }

        // GET: TipUzorka/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipUzorka = await _context.TipUzorka
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipUzorka == null)
            {
                return NotFound();
            }

            return View(tipUzorka);
        }

        // GET: TipUzorka/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipUzorka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Description")] TipUzorka tipUzorka)
        {
            if (ModelState.IsValid)
            {
                tipUzorka.DateCreated = DateTime.UtcNow;
                tipUzorka.DateUpdated = DateTime.UtcNow;
                tipUzorka.Timestamp = DateTime.UtcNow;
                tipUzorka.Id = Guid.NewGuid();
                _context.Add(tipUzorka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipUzorka);
        }

        // GET: TipUzorka/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipUzorka = await _context.TipUzorka.FindAsync(id);
            if (tipUzorka == null)
            {
                return NotFound();
            }
            return View(tipUzorka);
        }

        // POST: TipUzorka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Description")] TipUzorka tipUzorka)
        {
            if (id != tipUzorka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tipUzorka.DateUpdated = DateTime.UtcNow;
                    _context.Update(tipUzorka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipUzorkaExists(tipUzorka.Id))
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
            return View(tipUzorka);
        }

        // GET: TipUzorka/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipUzorka = await _context.TipUzorka
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipUzorka == null)
            {
                return NotFound();
            }

            return View(tipUzorka);
        }

        // POST: TipUzorka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tipUzorka = await _context.TipUzorka.FindAsync(id);
            _context.TipUzorka.Remove(tipUzorka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipUzorkaExists(Guid id)
        {
            return _context.TipUzorka.Any(e => e.Id == id);
        }
    }
}
