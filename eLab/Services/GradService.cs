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
    public class GradService : IGradService
    {
        private readonly ApplicationDbContext _context;
        public GradService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddGrad(GradPostRequest addGradRequest)
        {
            if (addGradRequest == null)
            {
                throw new ArgumentException("Invalid request");
            }

            var Grad = new Grad()
            {
                PTT = addGradRequest.PTT,
                Name = addGradRequest.Name,
                Id = Guid.NewGuid()
            };

            await _context.Grad.AddAsync(Grad);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGradiAsync(Guid GradId)
        {
            var Grad = await _context.Grad.FirstOrDefaultAsync(x => x.Id == GradId);

            _context.Grad.Remove(Grad);
            await _context.SaveChangesAsync();
        }

        public async Task<Grad> GetByIdAsync(Guid GradId)
        {
            var currentData = await _context.Grad.FirstOrDefaultAsync(x => x.Id == GradId);

            if (currentData == null)
            {
                throw new ArgumentException("Invalid request");
            }

            return currentData;
        }

        public async Task<IEnumerable<GradGetRequest>> GetGradAsync()
        {
            var Grad = await _context.Grad
                .Select(x => new GradGetRequest
                {
                    Id = x.Id,
                    PTT = x.PTT,
                    Name = x.Name
                }).ToListAsync();

            return Grad;
        }

        public async Task UpdateGrad(GradPostRequest updateGradiRequest)
        {
            var Grad = _context.Grad.Find(updateGradiRequest.Id);
            Grad.Name = updateGradiRequest.Name;
            Grad.PTT = updateGradiRequest.PTT;

            _context.Grad.Update(Grad);
            await _context.SaveChangesAsync();

        }
    }
}
