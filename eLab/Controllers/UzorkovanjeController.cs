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
    public class UzorkovanjeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UzorkovanjeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Uzorkovanje
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Uzorkovanje.ToListAsync());
        }

        // GET: Uzorkovanje/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uzorkovanje = await _context.Uzorkovanje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uzorkovanje == null)
            {
                return NotFound();
            }

            return View(uzorkovanje);
        }

        // GET: Uzorkovanje/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uzorkovanje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Description")] Uzorkovanje uzorkovanje)
        {
            if (ModelState.IsValid)
            {
                uzorkovanje.DateCreated = DateTime.UtcNow;
                uzorkovanje.DateUpdated = DateTime.UtcNow;
                uzorkovanje.Timestamp = DateTime.UtcNow;
                uzorkovanje.Id = Guid.NewGuid();
                _context.Add(uzorkovanje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uzorkovanje);
        }

        // GET: Uzorkovanje/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uzorkovanje = await _context.Uzorkovanje.FindAsync(id);
            if (uzorkovanje == null)
            {
                return NotFound();
            }
            return View(uzorkovanje);
        }

        // POST: Uzorkovanje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Description")] Uzorkovanje uzorkovanje)
        {
            if (id != uzorkovanje.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uzorkovanje.DateUpdated = DateTime.UtcNow;
                    _context.Update(uzorkovanje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UzorkovanjeExists(uzorkovanje.Id))
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
            return View(uzorkovanje);
        }

        // GET: Uzorkovanje/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uzorkovanje = await _context.Uzorkovanje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uzorkovanje == null)
            {
                return NotFound();
            }

            return View(uzorkovanje);
        }

        // POST: Uzorkovanje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var uzorkovanje = await _context.Uzorkovanje.FindAsync(id);
            _context.Uzorkovanje.Remove(uzorkovanje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UzorkovanjeExists(Guid id)
        {
            return _context.Uzorkovanje.Any(e => e.Id == id);
        }
    }
}
