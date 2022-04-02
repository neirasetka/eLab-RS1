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
    public class KrvnaGrupaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KrvnaGrupaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KrvnaGrupa
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.KrvnaGrupa.ToListAsync());
        }

        // GET: KrvnaGrupa/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var krvnaGrupa = await _context.KrvnaGrupa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (krvnaGrupa == null)
            {
                return NotFound();
            }

            return View(krvnaGrupa);
        }

        // GET: KrvnaGrupa/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: KrvnaGrupa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,BloodType,RhFactor")] KrvnaGrupa krvnaGrupa)
        {
            if (ModelState.IsValid)
            {
                krvnaGrupa.DateCreated = DateTime.UtcNow;
                krvnaGrupa.DateUpdated = DateTime.UtcNow;
                krvnaGrupa.Timestamp = DateTime.UtcNow;
                krvnaGrupa.Id = Guid.NewGuid();
                _context.Add(krvnaGrupa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(krvnaGrupa);
        }

        // GET: KrvnaGrupa/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var krvnaGrupa = await _context.KrvnaGrupa.FindAsync(id);
            if (krvnaGrupa == null)
            {
                return NotFound();
            }
            return View(krvnaGrupa);
        }

        // POST: KrvnaGrupa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,BloodType,RhFactor")] KrvnaGrupa krvnaGrupa)
        {
            if (id != krvnaGrupa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    krvnaGrupa.DateUpdated = DateTime.UtcNow;
                    _context.Update(krvnaGrupa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KrvnaGrupaExists(krvnaGrupa.Id))
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
            return View(krvnaGrupa);
        }

        // GET: KrvnaGrupa/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var krvnaGrupa = await _context.KrvnaGrupa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (krvnaGrupa == null)
            {
                return NotFound();
            }

            return View(krvnaGrupa);
        }

        // POST: KrvnaGrupa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var krvnaGrupa = await _context.KrvnaGrupa.FindAsync(id);
            _context.KrvnaGrupa.Remove(krvnaGrupa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KrvnaGrupaExists(Guid id)
        {
            return _context.KrvnaGrupa.Any(e => e.Id == id);
        }
    }
}
