using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eLab.Data;
using eLab.Models;
using eLab.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Rotativa.AspNetCore;

namespace eLab.Controllers
{
    public class UputnicaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UputnicaController> _logger;

        public UputnicaController(ApplicationDbContext context, ILogger<UputnicaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Uputnica
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var helpers = new HelperMethods();
            var applicationDbContext = _context.Uputnica.Include(u => u.Korisnik).Include(u => u.Pacijent).Include(u => u.TipUzorka).Include(u => u.Uzorkovanje);
            ViewBag.TotalNalaz = _context.Nalaz.Count();
            ViewBag.TotalUputnica = _context.Uputnica.Count();
            ViewBag.PercentTotal = helpers.GetPercent(_context.Nalaz.Count(), _context.Uputnica.Count());
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Uputnica/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                _logger.LogError($"Id uputnice nije proslijedjen");
                return NotFound();
            }

            var uputnica = await _context.Uputnica
                .Include(u => u.Korisnik)
                .Include(u => u.Pacijent)
                .Include(u => u.TipUzorka)
                .Include(u => u.Uzorkovanje)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uputnica == null)
            {
                return NotFound();
            }

            return View(uputnica);
        }

        // GET: Uputnica/Create
        [Authorize]
        public IActionResult Create()
        {
            if (User == null || User.Identity.Name != "lab@elab.ba")
            {
                _logger.LogError($"[IP: {this.HttpContext?.Connection?.RemoteIpAddress}, User: {User?.Identity?.Name}] - Neautorizirani pokusaj kreiranja uputnice");
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Name");
            ViewData["PacijentId"] = new SelectList(_context.Pacijent, "Id", "Name");
            ViewData["TipUzorkaId"] = new SelectList(_context.TipUzorka, "Id", "Name");
            ViewData["UzorkovanjeId"] = new SelectList(_context.Uzorkovanje, "Id", "Name");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Report(Guid id)
        {
            var uputnica = await _context.Uputnica
                   .Include(u => u.Korisnik)
                   .Include(u => u.Pacijent)
                   .Include(u => u.TipUzorka)
                   .Include(u => u.Uzorkovanje)
                   .FirstOrDefaultAsync(m => m.Id == id);

            var report = new ViewAsPdf("Report", uputnica, null);
            return report;
        }

        // POST: Uputnica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Name,KorisnikId,TipUzorkaId,UzorkovanjeId,PacijentId")] Uputnica uputnica)
        {
            if (User == null || User.Identity.Name != "lab@elab.ba")
            {
                _logger.LogError($"[IP: {this.HttpContext?.Connection?.RemoteIpAddress}, User: {User?.Identity?.Name}] - Neautorizirani pokusaj kreiranja uputnice");
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                uputnica.DateCreated = DateTime.UtcNow;
                uputnica.DateUpdated = DateTime.UtcNow;
                uputnica.Timestamp = DateTime.UtcNow;
                uputnica.KorisnikId = new Guid("3ef5bdd8-d21c-43bb-8c51-6a3d1e1dfe3a");
                uputnica.Id = Guid.NewGuid();
                _context.Add(uputnica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } else
            {
                _logger.LogError($"Uputnica - invalid model state");
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Name", uputnica.KorisnikId);
            ViewData["PacijentId"] = new SelectList(_context.Pacijent, "Id", "Name", uputnica.PacijentId);
            ViewData["TipUzorkaId"] = new SelectList(_context.TipUzorka, "Id", "Name", uputnica.TipUzorkaId);
            ViewData["UzorkovanjeId"] = new SelectList(_context.Uzorkovanje, "Id", "Name", uputnica.UzorkovanjeId);
            return View(uputnica);
        }

        // GET: Uputnica/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (User == null || User.Identity.Name != "lab@elab.ba")
            {
                _logger.LogWarning($"[IP: {this.HttpContext?.Connection?.RemoteIpAddress?.ToString()}, User: {User?.Identity?.Name}] - Neautorizirani pokusaj edita uputnice");
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }

            var uputnica = await _context.Uputnica.FindAsync(id);
            if (uputnica == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Name", uputnica.KorisnikId);
            ViewData["PacijentId"] = new SelectList(_context.Pacijent, "Id", "Name", uputnica.PacijentId);
            ViewData["TipUzorkaId"] = new SelectList(_context.TipUzorka, "Id", "Name", uputnica.TipUzorkaId);
            ViewData["UzorkovanjeId"] = new SelectList(_context.Uzorkovanje, "Id", "Name", uputnica.UzorkovanjeId);
            return View(uputnica);
        }

        // POST: Uputnica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Name,KorisnikId,TipUzorkaId,UzorkovanjeId,PacijentId")] Uputnica uputnica)
        {
            if (User == null || User.Identity.Name != "lab@elab.ba")
            {
                _logger.LogWarning($"[IP: {this.HttpContext?.Connection?.RemoteIpAddress}, User: {User?.Identity?.Name}] - Neautorizirani pokusaj edita uputnice");
                return NotFound();
            }
            if (id != uputnica.Id)
            {
                _logger.LogError($"Uputnica - invalid id {id} != {uputnica?.Id}");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uputnica.DateUpdated = DateTime.UtcNow;
                    _context.Update(uputnica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UputnicaExists(uputnica.Id))
                    {
                        _logger.LogError($"Uputnica - not found");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Name", uputnica.KorisnikId);
            ViewData["PacijentId"] = new SelectList(_context.Pacijent, "Id", "Name", uputnica.PacijentId);
            ViewData["TipUzorkaId"] = new SelectList(_context.TipUzorka, "Id", "Name", uputnica.TipUzorkaId);
            ViewData["UzorkovanjeId"] = new SelectList(_context.Uzorkovanje, "Id", "Name", uputnica.UzorkovanjeId);
            return View(uputnica);
        }

        // GET: Uputnica/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                _logger.LogError($"Uputnica - no id");
                return NotFound();
            }

            var uputnica = await _context.Uputnica
                .Include(u => u.Korisnik)
                .Include(u => u.Pacijent)
                .Include(u => u.TipUzorka)
                .Include(u => u.Uzorkovanje)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uputnica == null)
            {
                return NotFound();
            }

            return View(uputnica);
        }

        // POST: Uputnica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var uputnica = await _context.Uputnica.FindAsync(id);
            _context.Uputnica.Remove(uputnica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UputnicaExists(Guid id)
        {
            return _context.Uputnica.Any(e => e.Id == id);
        }
    }
}
