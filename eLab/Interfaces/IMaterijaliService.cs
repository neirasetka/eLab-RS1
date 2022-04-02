using eLab.DTO;
using eLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Interfaces
{
    public interface IMaterijaliService
    {
        Task<IEnumerable<GetMaterijaliResponse>> GetMaterialAsync();

        Task UpdateMaterijal(UpdateMaterijaliRequest updateMaterijaliRequest);

        Task DeleteMaterijaliAsync(Guid materijalId);

        Task AddMaterijal(AddMaterijalRequest addMaterijalRequest);

        Task<Materijali> GetByIdAsync(Guid materijalId);
    }
}
