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
    public class ParametriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParametriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parametri
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parametri.ToListAsync());
        }

        // GET: Parametri/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametri = await _context.Parametri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parametri == null)
            {
                return NotFound();
            }

            return View(parametri);
        }

        // GET: Parametri/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parametri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Kratica,MikrobioloskiUzorak")] Parametri parametri)
        {
            if (ModelState.IsValid)
            {
                //generate POCO
                parametri.DateCreated = DateTime.UtcNow;
                parametri.DateUpdated = DateTime.UtcNow;
                parametri.Timestamp = DateTime.UtcNow; 
                parametri.Id = Guid.NewGuid();
                _context.Add(parametri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parametri);
        }

        // GET: Parametri/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametri = await _context.Parametri.FindAsync(id);
            if (parametri == null)
            {
                return NotFound();
            }
            return View(parametri);
        }

        // POST: Parametri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Kratica,MikrobioloskiUzorak")] Parametri parametri)
        {
            if (id != parametri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    parametri.DateUpdated = DateTime.UtcNow;
                    _context.Update(parametri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParametriExists(parametri.Id))
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
            return View(parametri);
        }

        // GET: Parametri/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametri = await _context.Parametri
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parametri == null)
            {
                return NotFound();
            }

            return View(parametri);
        }

        // POST: Parametri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var parametri = await _context.Parametri.FindAsync(id);
            _context.Parametri.Remove(parametri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParametriExists(Guid id)
        {
            return _context.Parametri.Any(e => e.Id == id);
        }
    }
}
