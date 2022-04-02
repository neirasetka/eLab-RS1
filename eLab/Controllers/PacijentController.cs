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
using eLab.Repositories;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using X.PagedList.Mvc.Core;
using X.PagedList;

namespace eLab.Controllers
{
    public class PacijentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PacijentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pacijent
        [Authorize]
        public IActionResult Index(int? page)
        {
            var applicationDbContext = _context.Pacijent.Include(k=>k.Karton);
            return View(_context.Pacijent.ToList().ToPagedList(page ?? 1, 3));
        }

        // GET: Pacijent/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacijent = await _context.Pacijent
                .Include(k=>k.Karton)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacijent == null)
            {
                return NotFound();
            }

            return View(pacijent);
        }

        // GET: Pacijent/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["KartonId"] = new SelectList(_context.Karton, "Id", "Id");
            return View();
        }

        // POST: Pacijent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Description,KartonId")] Pacijent pacijent)
        {
            if (ModelState.IsValid)
            {
                pacijent.DateCreated = pacijent.DateCreated;
                pacijent.DateUpdated = pacijent.DateUpdated;
                pacijent.Timestamp = pacijent.Timestamp;
                pacijent.Id = Guid.NewGuid();
                pacijent.KartonId = pacijent.KartonId;
                _context.Pacijent.Add(pacijent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KartonId"] = new SelectList(_context.Karton, "Id", "Allergies", pacijent.KartonId);
            return View(pacijent);
        }

        // GET: Pacijent/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacijent = await _context.Pacijent.FindAsync(id);
            if (pacijent == null)
            {
                return NotFound();
            }
            ViewData["KartonId"] = new SelectList(_context.Karton, "Id", "Id", pacijent.KartonId);
            return View(pacijent);
        }

        // POST: Pacijent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Description,KartonId")] Pacijent pacijent)
        {
            if (id != pacijent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pacijent.DateUpdated = DateTime.UtcNow;
                    _context.Update(pacijent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacijentExists(pacijent.Id))
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
            ViewData["KartonId"] = new SelectList(_context.Karton, "Id", "Allergies", pacijent.KartonId);
            return View(pacijent);
        }

        // GET: Pacijent/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacijent = await _context.Pacijent
                .Include(k=>k.Karton)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacijent == null)
            {
                return NotFound();
            }

            return View(pacijent);
        }

        // POST: Pacijent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pacijent = await _context.Pacijent.FindAsync(id);
            _context.Pacijent.Remove(pacijent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacijentExists(Guid id)
        {
            return _context.Pacijent.Any(e => e.Id == id);
        }
    }
}
