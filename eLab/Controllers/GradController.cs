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
    public class GradController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Grad
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Grad.ToListAsync());
        }

        // GET: Grad/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grad = await _context.Grad
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grad == null)
            {
                return NotFound();
            }

            return View(grad);
        }

        // GET: Grad/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Name,PTT")] Grad grad)
        {
            //generate POCO
            grad.DateCreated = DateTime.UtcNow;
            grad.DateUpdated = DateTime.UtcNow;
            grad.Timestamp = DateTime.UtcNow;

            if (ModelState.IsValid)
            {
                grad.Id = Guid.NewGuid();
                _context.Add(grad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grad);
        }

        // GET: Grad/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grad = await _context.Grad.FindAsync(id);
            if (grad == null)
            {
                return NotFound();
            }
            return View(grad);
        }

        // POST: Grad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Name,PTT")] Grad grad)
        {
            if (id != grad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    grad.DateUpdated = DateTime.UtcNow;

                    _context.Update(grad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradExists(grad.Id))
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
            return View(grad);
        }

        // GET: Grad/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grad = await _context.Grad
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grad == null)
            {
                return NotFound();
            }

            return View(grad);
        }

        // POST: Grad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var grad = await _context.Grad.FindAsync(id);
            _context.Grad.Remove(grad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradExists(Guid id)
        {
            return _context.Grad.Any(e => e.Id == id);
        }
    }
}
