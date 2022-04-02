using eLab.Data;
using eLab.DTO;
using eLab.Interfaces;
using eLab.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Services
{
    public class MaterijalService : IMaterijaliService
    {
        private readonly ApplicationDbContext _context;
        public MaterijalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task AddMaterijal(AddMaterijalRequest addMaterijalRequest)
        {
            if(addMaterijalRequest == null)
            {
                throw new ArgumentException("Invalid request");
            }

            var materijal = new Materijali()
            {
                Description = addMaterijalRequest.Description,
                Name = addMaterijalRequest.Name,
                Quantity = addMaterijalRequest.Quantity,
                Id = Guid.NewGuid()
            };

            await _context.Materijali.AddAsync(materijal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMaterijaliAsync(Guid materijalId)
        {
            var materijal =await _context.Materijali.FirstOrDefaultAsync(x => x.Id == materijalId);

            _context.Materijali.Remove(materijal);
            await _context.SaveChangesAsync();
        }

        public async Task<Materijali> GetByIdAsync(Guid materijalId)
        {
            var currentData = await _context.Materijali.FirstOrDefaultAsync(x => x.Id == materijalId);

            if(currentData == null)
            {
                throw new ArgumentException("Invalid request");
            }

            return currentData;
        }

        public async Task<IEnumerable<GetMaterijaliResponse>> GetMaterialAsync()
        {
            var materijali = await _context.Materijali
                .Select(x => new GetMaterijaliResponse
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name,
                    Quantity = x.Quantity
                }).ToListAsync();

            return materijali;
        }

        public async Task UpdateMaterijal(UpdateMaterijaliRequest updateMaterijaliRequest)
        {
            var materijal = _context.Materijali.Find(updateMaterijaliRequest.Id);
            materijal.Name = updateMaterijaliRequest.Name;
            materijal.Quantity = updateMaterijaliRequest.Quantity;
            materijal.Description = updateMaterijaliRequest.Description;

            _context.Materijali.Update(materijal);
            await _context.SaveChangesAsync();
            
        }
    }
}
