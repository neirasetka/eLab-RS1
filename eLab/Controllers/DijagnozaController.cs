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
    public class DijagnozaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DijagnozaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dijagnoza
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dijagnoza.ToListAsync());
        }
        // GET: Dijagnoza/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dijagnoza = await _context.Dijagnoza
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dijagnoza == null)
            {
                return NotFound();
            }

            return View(dijagnoza);
        }

        // GET: Dijagnoza/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dijagnoza/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,LatinskiNaziv,LokalniNaziv,SifraMKB10")] Dijagnoza dijagnoza)
        {
            if (ModelState.IsValid)
            {
                //generate POCO
                dijagnoza.DateCreated = DateTime.UtcNow;
                dijagnoza.DateUpdated = DateTime.UtcNow;
                dijagnoza.Timestamp = DateTime.UtcNow;
                dijagnoza.Id = Guid.NewGuid();
                _context.Add(dijagnoza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dijagnoza);
        }

        // GET: Dijagnoza/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dijagnoza = await _context.Dijagnoza.FindAsync(id);
            if (dijagnoza == null)
            {
                return NotFound();
            }
            return View(dijagnoza);
        }

        // POST: Dijagnoza/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,LatinskiNaziv,LokalniNaziv,SifraMKB10")] Dijagnoza dijagnoza)
        {
            if (id != dijagnoza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dijagnoza.DateUpdated = DateTime.UtcNow;
                    _context.Update(dijagnoza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DijagnozaExists(dijagnoza.Id))
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
            return View(dijagnoza);
        }

        // GET: Dijagnoza/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dijagnoza = await _context.Dijagnoza
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dijagnoza == null)
            {
                return NotFound();
            }

            return View(dijagnoza);
        }

        // POST: Dijagnoza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dijagnoza = await _context.Dijagnoza.FindAsync(id);
            _context.Dijagnoza.Remove(dijagnoza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DijagnozaExists(Guid id)
        {
            return _context.Dijagnoza.Any(e => e.Id == id);
        }
    }
}
