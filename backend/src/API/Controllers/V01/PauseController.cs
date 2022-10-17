using System.Diagnostics;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V01
{
    [Route("api/V01/[controller]")]
    [ApiController]
    public class PauseController : ControllerBase
    {
        private readonly IRepository<Pause> _pauseRepository;

        public PauseController(IRepository<Pause> pauseRepository)
        {
            _pauseRepository = pauseRepository;
        }

        [HttpGet]
        [Route("GetPauseById/{id:Guid}", Name = "GetPauseByIdAsync")]
        public async Task<IActionResult> GetPauseByIdAsync(Guid id)
        {
            Pause? pauseObject = await _pauseRepository.GetByIdAsync(id);
            if (pauseObject == null)
            {
                return NotFound();
            }
            return Ok(pauseObject);
        }

        [HttpPost]
        [Route("AddPause", Name = "AddPauseAsync")]
        public async Task<IActionResult> AddPauseAsync([FromBody] PauseDto pauseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Pause result = new Pause
                {
                    StartDate = pauseDto.StartDate,
                    EndDate = pauseDto.EndDate,
                    ProfileIdQol = pauseDto.ProfileIdQol,
                    HouseholdId = pauseDto.HouseholdId,
                };

                await _pauseRepository.InsertAsync(result);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message + "\n" + e.InnerException);
            }
        }

        [HttpPut]
        [Route("UpdatePause/{id:Guid}", Name = "UpdatePauseAsync")]
        public async Task<IActionResult> UpdatePauseAsync([FromBody] PauseDto pauseDto, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Pause? pause = await _pauseRepository.GetByIdAsync(id);

                pause.StartDate = pauseDto.StartDate;
                pause.EndDate = pauseDto.EndDate;
                pause.ProfileIdQol = pauseDto.ProfileIdQol;
                pause.HouseholdId = pauseDto.HouseholdId;

                await _pauseRepository.UpdateAsync(pause);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message + "\n" + e.InnerException);
            }
        }
    }
}
