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
    public class UzorkovanjeMaterijaliController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UzorkovanjeMaterijaliController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UzorkovanjeMaterijali
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UzorkovanjeMaterijali.Include(u => u.Materijali).Include(u => u.Uzorkovanje);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UzorkovanjeMaterijali/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uzorkovanjeMaterijali = await _context.UzorkovanjeMaterijali
                .Include(u => u.Materijali)
                .Include(u => u.Uzorkovanje)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uzorkovanjeMaterijali == null)
            {
                return NotFound();
            }

            return View(uzorkovanjeMaterijali);
        }

        // GET: UzorkovanjeMaterijali/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["MaterijaliId"] = new SelectList(_context.Materijali, "Id", "Id");
            ViewData["UzorkovanjeId"] = new SelectList(_context.Uzorkovanje, "Id", "Id");
            return View();
        }

        // POST: UzorkovanjeMaterijali/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,UzorkovanjeId,MaterijaliId")] UzorkovanjeMaterijali uzorkovanjeMaterijali)
        {
            if (ModelState.IsValid)
            {
                uzorkovanjeMaterijali.Id = Guid.NewGuid();
                _context.Add(uzorkovanjeMaterijali);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterijaliId"] = new SelectList(_context.Materijali, "Id", "Id", uzorkovanjeMaterijali.MaterijaliId);
            ViewData["UzorkovanjeId"] = new SelectList(_context.Uzorkovanje, "Id", "Id", uzorkovanjeMaterijali.UzorkovanjeId);
            return View(uzorkovanjeMaterijali);
        }

        // GET: UzorkovanjeMaterijali/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uzorkovanjeMaterijali = await _context.UzorkovanjeMaterijali.FindAsync(id);
            if (uzorkovanjeMaterijali == null)
            {
                return NotFound();
            }
            ViewData["MaterijaliId"] = new SelectList(_context.Materijali, "Id", "Id", uzorkovanjeMaterijali.MaterijaliId);
            ViewData["UzorkovanjeId"] = new SelectList(_context.Uzorkovanje, "Id", "Id", uzorkovanjeMaterijali.UzorkovanjeId);
            return View(uzorkovanjeMaterijali);
        }

        // POST: UzorkovanjeMaterijali/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UzorkovanjeId,MaterijaliId")] UzorkovanjeMaterijali uzorkovanjeMaterijali)
        {
            if (id != uzorkovanjeMaterijali.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uzorkovanjeMaterijali);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UzorkovanjeMaterijaliExists(uzorkovanjeMaterijali.Id))
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
            ViewData["MaterijaliId"] = new SelectList(_context.Materijali, "Id", "Id", uzorkovanjeMaterijali.MaterijaliId);
            ViewData["UzorkovanjeId"] = new SelectList(_context.Uzorkovanje, "Id", "Id", uzorkovanjeMaterijali.UzorkovanjeId);
            return View(uzorkovanjeMaterijali);
        }

        // GET: UzorkovanjeMaterijali/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uzorkovanjeMaterijali = await _context.UzorkovanjeMaterijali
                .Include(u => u.Materijali)
                .Include(u => u.Uzorkovanje)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uzorkovanjeMaterijali == null)
            {
                return NotFound();
            }

            return View(uzorkovanjeMaterijali);
        }

        // POST: UzorkovanjeMaterijali/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var uzorkovanjeMaterijali = await _context.UzorkovanjeMaterijali.FindAsync(id);
            _context.UzorkovanjeMaterijali.Remove(uzorkovanjeMaterijali);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UzorkovanjeMaterijaliExists(Guid id)
        {
            return _context.UzorkovanjeMaterijali.Any(e => e.Id == id);
        }
    }
}
