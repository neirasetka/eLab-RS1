using eLab.DTO;
using eLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Interfaces
{
    public interface IGradService
    {
        Task<IEnumerable<GradGetRequest>> GetGradAsync();

        Task UpdateGrad(GradPostRequest updateGradiRequest);

        Task DeleteGradiAsync(Guid gradId);

        Task AddGrad(GradPostRequest addGradRequest);

        Task<Grad> GetByIdAsync(Guid GradId);
    }
}
