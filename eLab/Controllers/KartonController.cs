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
    public class KartonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KartonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Karton
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Karton.ToListAsync());
        }

        // GET: Karton/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karton = await _context.Karton
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karton == null)
            {
                return NotFound();
            }

            return View(karton);
        }

        // GET: Karton/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Karton/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Allergies,Anamnesis,Height,Weight")] Karton karton)
        {
            if (ModelState.IsValid)
            {
                karton.DateCreated = DateTime.UtcNow;
                karton.DateUpdated = DateTime.UtcNow;
                karton.Timestamp = DateTime.UtcNow;
                karton.Id = Guid.NewGuid();
                _context.Add(karton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(karton);
        }

        // GET: Karton/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karton = await _context.Karton.FindAsync(id);
            if (karton == null)
            {
                return NotFound();
            }
            return View(karton);
        }

        // POST: Karton/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Allergies,Anamnesis,Height,Weight")] Karton karton)
        {
            if (id != karton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    karton.DateUpdated = DateTime.UtcNow;
                    _context.Update(karton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KartonExists(karton.Id))
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
            return View(karton);
        }

        // GET: Karton/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karton = await _context.Karton
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karton == null)
            {
                return NotFound();
            }

            return View(karton);
        }

        // POST: Karton/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var karton = await _context.Karton.FindAsync(id);
            _context.Karton.Remove(karton);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KartonExists(Guid id)
        {
            return _context.Karton.Any(e => e.Id == id);
        }
    }
}
