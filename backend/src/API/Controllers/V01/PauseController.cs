using System.Diagnostics;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
            PauseOutDto pauseDto = new PauseOutDto
            {
                Id = pauseObject.Id,
                StartDate = pauseObject.StartDate,
                EndDate = pauseObject.EndDate,
                ProfileIdQol = pauseObject.ProfileIdQol,
                HouseholdId = pauseObject.HouseholdId
            };
            return Ok(pauseDto);
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
            IEnumerable<PauseOutDto> pauseOutDtoList = pauseObjects.Select(pause => new PauseOutDto
            {
                Id = pause.Id,
                StartDate = pause.StartDate,
                EndDate = pause.EndDate,
                ProfileIdQol = pause.ProfileIdQol,
                HouseholdId = pause.HouseholdId
            });

            return Ok(pauseOutDtoList);
        }

        [HttpPost]
        [Route("AddPause", Name = "AddPauseAsync")]
        [Authorize]
        public async Task<IActionResult> AddPauseAsync([FromBody] PauseInDto pauseInDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Pause pause = new Pause
                {
                    StartDate = pauseInDto.StartDate,
                    EndDate = pauseInDto.EndDate,
                    ProfileIdQol = pauseInDto.ProfileIdQol,
                    HouseholdId = pauseInDto.HouseholdId,
                };

                var insertedPause = await _pauseRepository.InsertAsync(pause);
                PauseOutDto pauseOutDto = new PauseOutDto
                {
                    Id = insertedPause.Id,
                    StartDate = insertedPause.StartDate,
                    EndDate = insertedPause.EndDate,
                    ProfileIdQol = insertedPause.ProfileIdQol,
                    HouseholdId = insertedPause.HouseholdId
                };
                return CreatedAtRoute("GetPauseByIdAsync", new { id = pauseOutDto.Id }, pauseOutDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message + "\n" + e.InnerException);
            }
        }

        [HttpPut]
        [Route("UpdatePause/{id:Guid}", Name = "UpdatePauseAsync")]
        public async Task<IActionResult> UpdatePauseAsync([FromBody] PauseUpdateInDto pauseUpdateInDto, Guid id)
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

                if (pauseUpdateInDto.StartDate != DateTime.MinValue && pauseUpdateInDto.StartDate != null)
                {
                    //Debug.WriteLine("Vi skriver till pause.StartDate" + pauseUpdateInDto.StartDate);
                    pause.StartDate = (DateTime)pauseUpdateInDto.StartDate;
                }

                if (pauseUpdateInDto.EndDate != null)
                {
                    //Debug.WriteLine("Vi skriver till pause.EndDate" + pauseUpdateInDto.EndDate);
                    pause.EndDate = (DateTime)pauseUpdateInDto.EndDate;
                }
                
                if (pauseUpdateInDto.ProfileIdQol != null)
                {
                    //Debug.WriteLine("Vi skriver till pause.ProfileIdQol" + pauseUpdateInDto.ProfileIdQol);
                    pause.ProfileIdQol = (Guid)pauseUpdateInDto.ProfileIdQol;
                }

                pause.HouseholdId = pauseUpdateInDto.HouseholdId;

                var updatedPause = await _pauseRepository.UpdateAsync(pause);
                PauseOutDto pauseOutDto = new PauseOutDto
                {
                    Id = updatedPause.Id,
                    StartDate = updatedPause.StartDate,
                    EndDate = updatedPause.EndDate,
                    ProfileIdQol = updatedPause.ProfileIdQol,
                    HouseholdId = updatedPause.HouseholdId
                };

                return CreatedAtRoute("GetPauseByIdAsync", new { id = pauseOutDto.Id }, pauseOutDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message + "\n" + e.InnerException);
            }
        }
    }
}
