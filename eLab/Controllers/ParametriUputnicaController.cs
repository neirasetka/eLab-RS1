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
    public class ParametriUputnicaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParametriUputnicaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ParametriUputnica
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ParametriUputnica.Include(p => p.Parametri).Include(p => p.Uputnica);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ParametriUputnica/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametriUputnica = await _context.ParametriUputnica
                .Include(p => p.Parametri)
                .Include(p => p.Uputnica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parametriUputnica == null)
            {
                return NotFound();
            }

            return View(parametriUputnica);
        }

        // GET: ParametriUputnica/Create
        public IActionResult Create()
        {
            ViewData["ParametriId"] = new SelectList(_context.Parametri, "Id", "Id");
            ViewData["UputnicaId"] = new SelectList(_context.Uputnica, "Id", "Id");
            return View();
        }

        // POST: ParametriUputnica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParametriId,UputnicaId")] ParametriUputnica parametriUputnica)
        {
            if (ModelState.IsValid)
            {
                parametriUputnica.Id = Guid.NewGuid();
                _context.Add(parametriUputnica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParametriId"] = new SelectList(_context.Parametri, "Id", "Id", parametriUputnica.ParametriId);
            ViewData["UputnicaId"] = new SelectList(_context.Uputnica, "Id", "Id", parametriUputnica.UputnicaId);
            return View(parametriUputnica);
        }

        // GET: ParametriUputnica/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametriUputnica = await _context.ParametriUputnica.FindAsync(id);
            if (parametriUputnica == null)
            {
                return NotFound();
            }
            ViewData["ParametriId"] = new SelectList(_context.Parametri, "Id", "Id", parametriUputnica.ParametriId);
            ViewData["UputnicaId"] = new SelectList(_context.Uputnica, "Id", "Id", parametriUputnica.UputnicaId);
            return View(parametriUputnica);
        }

        // POST: ParametriUputnica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ParametriId,UputnicaId")] ParametriUputnica parametriUputnica)
        {
            if (id != parametriUputnica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parametriUputnica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParametriUputnicaExists(parametriUputnica.Id))
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
            ViewData["ParametriId"] = new SelectList(_context.Parametri, "Id", "Id", parametriUputnica.ParametriId);
            ViewData["UputnicaId"] = new SelectList(_context.Uputnica, "Id", "Id", parametriUputnica.UputnicaId);
            return View(parametriUputnica);
        }

        // GET: ParametriUputnica/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametriUputnica = await _context.ParametriUputnica
                .Include(p => p.Parametri)
                .Include(p => p.Uputnica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parametriUputnica == null)
            {
                return NotFound();
            }

            return View(parametriUputnica);
        }

        // POST: ParametriUputnica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var parametriUputnica = await _context.ParametriUputnica.FindAsync(id);
            _context.ParametriUputnica.Remove(parametriUputnica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParametriUputnicaExists(Guid id)
        {
            return _context.ParametriUputnica.Any(e => e.Id == id);
        }
    }
}
