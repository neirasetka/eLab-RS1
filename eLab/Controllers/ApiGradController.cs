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
    public class ApiGradController : BaseApiController
    {
        private readonly IGradService _gradService;
        private readonly ApplicationDbContext _context;

        public ApiGradController(ApplicationDbContext context, IGradService gradService)
        {
            _context = context;
            _gradService = gradService;
        }


        [HttpGet]
        public async Task<IActionResult> GetGrad()
        {
            return Ok(await _gradService.GetGradAsync());
        }

        [HttpDelete("{gradId}")]
        public async Task<IActionResult> DeleteGrad(Guid gradId)
        {
            await _gradService.DeleteGradiAsync(gradId);

            return Ok("Deleted succesfully");
        }

        [HttpPost]
        public async Task<IActionResult> AddGrad(GradPostRequest addGradRequest)
        {
            await _gradService.AddGrad(addGradRequest);
            return Ok("You succeeded");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGrad(GradPostRequest updateGradRequest)
        {
            await _gradService.UpdateGrad(updateGradRequest);
            return Ok("You succeeded");
        }

        [HttpGet("{gradId}")]
        public async Task<IActionResult> GetCurrentData(Guid gradId)
        {
            return Ok(await _gradService.GetByIdAsync(gradId));
        }
    }
}
