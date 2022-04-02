using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eLab.Data;
using eLab.Models;

namespace eLab.Controllers
{
    public class DijagnozaUputnicaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DijagnozaUputnicaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DijagnozaUputnica
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DijagnozaUputnica.Include(d => d.Dijagnoza).Include(d => d.Uputnica);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DijagnozaUputnica/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dijagnozaUputnica = await _context.DijagnozaUputnica
                .Include(d => d.Dijagnoza)
                .Include(d => d.Uputnica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dijagnozaUputnica == null)
            {
                return NotFound();
            }

            return View(dijagnozaUputnica);
        }

        // GET: DijagnozaUputnica/Create
        public IActionResult Create()
        {
            ViewData["DijagnozaId"] = new SelectList(_context.Dijagnoza, "Id", "Id");
            ViewData["UputnicaId"] = new SelectList(_context.Uputnica, "Id", "Id");
            return View();
        }

        // POST: DijagnozaUputnica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DijagnozaId,UputnicaId")] DijagnozaUputnica dijagnozaUputnica)
        {
            if (ModelState.IsValid)
            {
                dijagnozaUputnica.Id = Guid.NewGuid();
                _context.Add(dijagnozaUputnica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DijagnozaId"] = new SelectList(_context.Dijagnoza, "Id", "Id", dijagnozaUputnica.DijagnozaId);
            ViewData["UputnicaId"] = new SelectList(_context.Uputnica, "Id", "Id", dijagnozaUputnica.UputnicaId);
            return View(dijagnozaUputnica);
        }

        // GET: DijagnozaUputnica/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dijagnozaUputnica = await _context.DijagnozaUputnica.FindAsync(id);
            if (dijagnozaUputnica == null)
            {
                return NotFound();
            }
            ViewData["DijagnozaId"] = new SelectList(_context.Dijagnoza, "Id", "Id", dijagnozaUputnica.DijagnozaId);
            ViewData["UputnicaId"] = new SelectList(_context.Uputnica, "Id", "Id", dijagnozaUputnica.UputnicaId);
            return View(dijagnozaUputnica);
        }

        // POST: DijagnozaUputnica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DijagnozaId,UputnicaId")] DijagnozaUputnica dijagnozaUputnica)
        {
            if (id != dijagnozaUputnica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dijagnozaUputnica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DijagnozaUputnicaExists(dijagnozaUputnica.Id))
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
            ViewData["DijagnozaId"] = new SelectList(_context.Dijagnoza, "Id", "Id", dijagnozaUputnica.DijagnozaId);
            ViewData["UputnicaId"] = new SelectList(_context.Uputnica, "Id", "Id", dijagnozaUputnica.UputnicaId);
            return View(dijagnozaUputnica);
        }

        // GET: DijagnozaUputnica/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dijagnozaUputnica = await _context.DijagnozaUputnica
                .Include(d => d.Dijagnoza)
                .Include(d => d.Uputnica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dijagnozaUputnica == null)
            {
                return NotFound();
            }

            return View(dijagnozaUputnica);
        }

        // POST: DijagnozaUputnica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dijagnozaUputnica = await _context.DijagnozaUputnica.FindAsync(id);
            _context.DijagnozaUputnica.Remove(dijagnozaUputnica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DijagnozaUputnicaExists(Guid id)
        {
            return _context.DijagnozaUputnica.Any(e => e.Id == id);
        }
    }
}
