using eLab.Data;
using eLab.DTO;
using eLab.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLab.Controllers
{
    public class ApiMaterijaliController : BaseApiController
    {
        private readonly IMaterijaliService _materijaliService;
        private readonly ApplicationDbContext _context;

        public ApiMaterijaliController(ApplicationDbContext context, IMaterijaliService materijaliService)
        {
            _context = context;
            _materijaliService = materijaliService;
        }


        [HttpGet]
        public async Task<IActionResult> GetMaterijali()
        {
            return Ok(await _materijaliService.GetMaterialAsync());
        }

        [HttpDelete("{materijalId}")]
        public async Task<IActionResult> DeleteMaterijal(Guid materijalId)
        {
            await _materijaliService.DeleteMaterijaliAsync(materijalId);

            return Ok("Deleted succesfully");
        }
        
        [HttpPost]
        public async Task<IActionResult> AddMaterijal(AddMaterijalRequest addMaterijalRequest)
        {
            await _materijaliService.AddMaterijal(addMaterijalRequest);
            return Ok("You succeeded");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMaterijal(UpdateMaterijaliRequest updateMaterijaliRequest)
        {
            await _materijaliService.UpdateMaterijal(updateMaterijaliRequest);
            return Ok("You succeeded");
        }

        [HttpGet("{materijalId}")]
        public async Task<IActionResult> GetCurrentData(Guid materijalId)
        {
            return Ok(await _materijaliService.GetByIdAsync(materijalId));
        }
    }
}
