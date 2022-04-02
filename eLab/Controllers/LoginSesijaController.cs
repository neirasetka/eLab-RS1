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
    public class LoginSesijaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginSesijaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LoginSesija
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LoginSesija.Include(l => l.Korisnik);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LoginSesija/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginSesija = await _context.LoginSesija
                .Include(l => l.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loginSesija == null)
            {
                return NotFound();
            }

            return View(loginSesija);
        }

        // GET: LoginSesija/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id");
            return View();
        }

        // POST: LoginSesija/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KorisnikId,DateCreated,DateUpdated,Timestamp")] LoginSesija loginSesija)
        {
            // generate POCO
            loginSesija.DateCreated = DateTime.UtcNow;
            loginSesija.DateUpdated = DateTime.UtcNow;
            loginSesija.Timestamp = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                loginSesija.Id = Guid.NewGuid();
                _context.Add(loginSesija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", loginSesija.KorisnikId);
            return View(loginSesija);
        }

        // GET: LoginSesija/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginSesija = await _context.LoginSesija.FindAsync(id);
            if (loginSesija == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", loginSesija.KorisnikId);
            return View(loginSesija);
        }

        // POST: LoginSesija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,KorisnikId,DateCreated,DateUpdated,Timestamp")] LoginSesija loginSesija)
        {
            if (id != loginSesija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginSesija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginSesijaExists(loginSesija.Id))
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
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", loginSesija.KorisnikId);
            return View(loginSesija);
        }

        // GET: LoginSesija/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loginSesija = await _context.LoginSesija
                .Include(l => l.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loginSesija == null)
            {
                return NotFound();
            }

            return View(loginSesija);
        }

        // POST: LoginSesija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var loginSesija = await _context.LoginSesija.FindAsync(id);
            _context.LoginSesija.Remove(loginSesija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginSesijaExists(Guid id)
        {
            return _context.LoginSesija.Any(e => e.Id == id);
        }
    }
}
