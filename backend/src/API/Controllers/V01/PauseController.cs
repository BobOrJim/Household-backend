using System.Diagnostics;
using Core.Entities;
using Core.Interfaces;
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
        [Route("GetPauseByHouseholdId/{id:Guid}", Name = "GetPauseByHouseholdIdAsync")]
        public async Task<IActionResult> GetPauseByHouseholdIdAsync(Guid id)
        {
            IEnumerable<Pause>? pauseObjects = await _pauseRepository.GetListAsync(p => p.HouseholdId == id);
            if (pauseObjects == null)
            {
                return NotFound();
            }
            IEnumerable<PauseOutDto> pauseDtoList = pauseObjects.Select(pause => new PauseOutDto
            {
                Id = pause.Id,
                StartDate = pause.StartDate,
                EndDate = pause.EndDate,
                ProfileIdQol = pause.ProfileIdQol,
                HouseholdId = pause.HouseholdId
            });

            return Ok(pauseDtoList);
        }

        [HttpPost]
        [Route("AddPause", Name = "AddPauseAsync")]
        public async Task<IActionResult> AddPauseAsync([FromBody] PauseOutDto pauseDto)
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
        public async Task<IActionResult> UpdatePauseAsync([FromBody] PauseOutDto pauseDto, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Pause? pause = await _pauseRepository.GetByIdAsync(id);
                if (pause == null)
                {
                    return NotFound();
                }
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
