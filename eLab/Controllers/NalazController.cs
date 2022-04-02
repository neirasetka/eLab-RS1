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
using Microsoft.Extensions.Logging;
using Rotativa.AspNetCore;

namespace eLab.Controllers
{
    public class NalazController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NalazController> _logger;

        public NalazController(ApplicationDbContext context, ILogger<NalazController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Nalaz
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Nalaz.Include(n => n.Pacijent).Include(n => n.Analiza);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Nalaz/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                _logger.LogError($"Nalaz - id nije pronadjen");
                return NotFound();
            }

            var nalaz = await _context.Nalaz
                .Include(n => n.Pacijent)
                .Include(n => n.Analiza)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nalaz == null)
            {
                return NotFound();
            }

            return View(nalaz);
        }

        // GET: Nalaz/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["PacijentId"] = new SelectList(_context.Pacijent, "Id", "Name");
            ViewData["AnalizaId"] = new SelectList(_context.Analiza, "Id", "Name");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Report(Guid id)
        {
            var nalaz = await _context.Nalaz
                   .Include(p => p.Pacijent)
                   .Include(a => a.Analiza)
                   .FirstOrDefaultAsync(m => m.Id == id);

            var report = new ViewAsPdf("Report", nalaz, null);
            return report;
        }

        // POST: Nalaz/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Description, PacijentId, AnalizaId,IsUrgent")] Nalaz nalaz)
        {
            if (ModelState.IsValid)
            {
                nalaz.DateCreated = nalaz.DateCreated;
                nalaz.DateUpdated = nalaz.DateUpdated;
                nalaz.Timestamp = nalaz.Timestamp;
                nalaz.Id = Guid.NewGuid();
                nalaz.PacijentId = nalaz.PacijentId;
                nalaz.AnalizaId = nalaz.AnalizaId;
                nalaz.IsUrgent = nalaz.IsUrgent;
                _context.Nalaz.Add(nalaz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _logger.LogError($"Nalaz - invalid model state");
            }
            ViewData["PacijentId"] = new SelectList(_context.Pacijent, "Id", "Name", nalaz.PacijentId);
            ViewData["AnalizaId"] = new SelectList(_context.Analiza, "Id", "Name", nalaz.AnalizaId);
            return View(nalaz);
        }

        // GET: Nalaz/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nalaz = await _context.Nalaz.FindAsync(id);
            if (nalaz == null)
            {
                return NotFound();
            }
            ViewData["PacijentId"] = new SelectList(_context.Pacijent, "Id", "Name", nalaz.PacijentId);
            ViewData["AnalizaId"] = new SelectList(_context.Analiza, "Id", "Name", nalaz.AnalizaId);
            return View(nalaz);
        }

        // POST: Nalaz/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Description, PacijentId, AnalizaId")] Nalaz nalaz)
        {
            if (id != nalaz.Id)
            {
                _logger.LogError($"Nalaz - invalid id {id} != {nalaz?.Id}");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    nalaz.DateUpdated = DateTime.UtcNow;
                    _context.Update(nalaz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NalazExists(nalaz.Id))
                    {
                        _logger.LogError($"Nalaz - not found");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacijentId"] = new SelectList(_context.Pacijent, "Id", "Name", nalaz.PacijentId);
            ViewData["AnalizaId"] = new SelectList(_context.Analiza, "Id", "Name", nalaz.AnalizaId);
            return View(nalaz);
        }

        // GET: Nalaz/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                _logger.LogError($"Nalaz - no id");
                return NotFound();
            }

            var nalaz = await _context.Nalaz
                .Include(n => n.Pacijent)
                .Include(n => n.Analiza)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nalaz == null)
            {
                return NotFound();
            }

            return View(nalaz);
        }

        // POST: Nalaz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var nalaz = await _context.Nalaz.FindAsync(id);
            _context.Nalaz.Remove(nalaz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NalazExists(Guid id)
        {
            return _context.Nalaz.Any(e => e.Id == id);
        }
    }
}