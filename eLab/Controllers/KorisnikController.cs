using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eLab.Data;
using eLab.Models;
using Microsoft.AspNetCore.Identity;

namespace eLab.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly ApplicationDbContext _context;
        public KorisnikController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Korisnik
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Korisnik.Include(k => k.TipKorisnika).Include(k => k.Ustanova);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Korisnik/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnik
                .Include(k => k.TipKorisnika)
                .Include(k => k.Ustanova)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // GET: Korisnik/Create
        public IActionResult Create()
        {
            ViewData["TipKorisnikaId"] = new SelectList(_context.TipKorisnika, "Id", "Name");
            ViewData["UstanovaId"] = new SelectList(_context.Ustanova, "Id", "Name");
            return View();
        }

        // POST: Korisnik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Ime,Prezime,Email,TipKorisnikaId,UstanovaId,CoreUserId")] Korisnik korisnik)
        {
            // generate POCO
            korisnik.DateCreated = DateTime.UtcNow;
            korisnik.DateUpdated = DateTime.UtcNow;
            korisnik.Timestamp = DateTime.UtcNow;

            korisnik.CoreUserId = Guid.Empty;

            if (ModelState.IsValid)
            {
                korisnik.Id = Guid.NewGuid();
                _context.Add(korisnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipKorisnikaId"] = new SelectList(_context.TipKorisnika, "Id", "Name", korisnik.TipKorisnikaId);
            ViewData["UstanovaId"] = new SelectList(_context.Ustanova, "Id", "Name", korisnik.UstanovaId);
            return View(korisnik);
        }

        // GET: Korisnik/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnik.FindAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }
            ViewData["TipKorisnikaId"] = new SelectList(_context.TipKorisnika, "Id", "Name", korisnik.TipKorisnikaId);
            ViewData["UstanovaId"] = new SelectList(_context.Ustanova, "Id", "Name", korisnik.UstanovaId);
            return View(korisnik);
        }

        // POST: Korisnik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Ime,Prezime,Email,TipKorisnikaId,UstanovaId,CoreUserId")] Korisnik korisnik)
        {
            if (id != korisnik.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    korisnik.DateUpdated = DateTime.UtcNow;
                    _context.Update(korisnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorisnikExists(korisnik.Id))
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
            ViewData["TipKorisnikaId"] = new SelectList(_context.TipKorisnika, "Id", "Name", korisnik.TipKorisnikaId);
            ViewData["UstanovaId"] = new SelectList(_context.Ustanova, "Id", "Name", korisnik.UstanovaId);
            return View(korisnik);
        }

        // GET: Korisnik/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnik
                .Include(k => k.TipKorisnika)
                .Include(k => k.Ustanova)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // POST: Korisnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var korisnik = await _context.Korisnik.FindAsync(id);
            _context.Korisnik.Remove(korisnik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorisnikExists(Guid id)
        {
            return _context.Korisnik.Any(e => e.Id == id);
        }
    }
}
