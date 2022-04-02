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
    public class AnalizaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnalizaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Analiza
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Analiza.ToListAsync());
        }

        // GET: Analiza/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analiza = await _context.Analiza
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analiza == null)
            {
                return NotFound();
            }

            return View(analiza);
        }

        // GET: Analiza/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Analiza/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Description")] Analiza analiza)
        {
            if (ModelState.IsValid)
            {
                analiza.DateCreated = DateTime.UtcNow;
                analiza.DateUpdated = DateTime.UtcNow;
                analiza.Timestamp = DateTime.UtcNow;
                analiza.Id = Guid.NewGuid();
                _context.Add(analiza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(analiza);
        }

        // GET: Analiza/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analiza = await _context.Analiza.FindAsync(id);
            if (analiza == null)
            {
                return NotFound();
            }
            return View(analiza);
        }

        // POST: Analiza/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Description")] Analiza analiza)
        {
            if (id != analiza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    analiza.DateUpdated = DateTime.UtcNow;
                    _context.Update(analiza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalizaExists(analiza.Id))
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
            return View(analiza);
        }

        // GET: Analiza/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analiza = await _context.Analiza
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analiza == null)
            {
                return NotFound();
            }

            return View(analiza);
        }

        // POST: Analiza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var analiza = await _context.Analiza.FindAsync(id);
            _context.Analiza.Remove(analiza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnalizaExists(Guid id)
        {
            return _context.Analiza.Any(e => e.Id == id);
        }
    }
}
