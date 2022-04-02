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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using eLab.Interfaces;

namespace eLab.Controllers
{
    public class MaterijaliController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public MaterijaliController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        // GET: Materijali



        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Materijali.ToListAsync());
        }

        // GET: Materijali/Details/5
        [Authorize]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materijali = await _context.Materijali
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materijali == null)
            {
                return NotFound();
            }

            return View(materijali);
        }

        // GET: Materijali/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        //POST: Materijali/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Quantity,Description")] Materijali materijali)
        {
            if (ModelState.IsValid)
            {

                materijali.DateCreated = DateTime.UtcNow;
                materijali.DateUpdated = DateTime.UtcNow;
                materijali.Timestamp = DateTime.UtcNow;
                materijali.Id = Guid.NewGuid();
                //materijali.PutanjaDoSlike =
                _context.Add(materijali);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materijali);
        }
        //    var materijali1 = _context.Materijali
        //        .Select(x => new Materijali
        //        {
        //            Id = x.Id,
        //            DateCreated = x.DateCreated,
        //            DateUpdated = x.DateUpdated,
        //            Timestamp = x.Timestamp,
        //            Name = x.Name,
        //            Quantity = x.Quantity,
        //            Description = x.Description,
        //            PutanjaDoSlike = x.PutanjaDoSlike
        //        )};
        //    return View(materijali1);
        //}

        //ovu funkciju dodala

        //private string UploadFile(Materijali x)
        //{
        //    string fileName = null;
        //    if (x.SlikaMaterijali != null)
        //    {
        //        foreach (IFormFile wellness in x.SlikaMaterijali)
        //        {
        //            string uploadDir = Path.Combine(IWebHostEnvironment.WebRootPath, "Slike");
        //            fileName = Guid.NewGuid().ToString() + "-" + wellness.FileName;
        //            string filePath = Path.Combine(uploadDir, fileName);
        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                wellness.CopyTo(fileStream);
        //            }
        //        }
        //    }
        //    return fileName;
        //}

        // GET: Materijali/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materijali = await _context.Materijali.FindAsync(id);
            if (materijali == null)
            {
                return NotFound();
            }
            return View(materijali);
        }

        // POST: Materijali/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateCreated,DateUpdated,Timestamp,Name,Quantity,Description")] Materijali materijali)
        {
            if (id != materijali.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    materijali.DateUpdated = DateTime.UtcNow;
                    _context.Update(materijali);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterijaliExists(materijali.Id))
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
            return View(materijali);
        }

        // GET: Materijali/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materijali = await _context.Materijali
                .FirstOrDefaultAsync(m => m.Id == id);
            if (materijali == null)
            {
                return NotFound();
            }

            return View(materijali);
        }

        // POST: Materijali/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var materijali = await _context.Materijali.FindAsync(id);
            _context.Materijali.Remove(materijali);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterijaliExists(Guid id)
        {
            return _context.Materijali.Any(e => e.Id == id);
        }
    }
}
