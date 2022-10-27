using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace API.Controllers.V01
{
    [Route("api/V01/[controller]")]
    [ApiController]
    public class ChoreCompletedController : ControllerBase
    {
        private readonly IRepository<ChoreCompleted> _choreCompletedRepository;

        public ChoreCompletedController(IRepository<ChoreCompleted> choreCompletedRepository)
        {
            _choreCompletedRepository = choreCompletedRepository;
        }

        [HttpGet]
        //[Route("GetAllChoreCompletedByHouseholdId/{householdId:Guid}", Name = "GetAllChoreCompletedAsync")]
        [Route("GetAllChoreCompletedByHouseholdId/{householdId:Guid}")]
        public async Task<IActionResult> GetAllChoreCompletedByHouseholdId(Guid householdId)
        {
            List<ChoreCompleted>? result = (List<ChoreCompleted>?)await _choreCompletedRepository.GetListAsync(o => o.HouseholdId == householdId);
            if (result == null)
            {
                return NotFound();
            }
            List<ChoreCompletedOutDto> choreCompletedOutDtos = new();
            foreach (ChoreCompleted choreCompleted in result)
            {
                choreCompletedOutDtos.Add(new ChoreCompletedOutDto
                {
                    Id = choreCompleted.Id,
                    CompletedAt = choreCompleted.CompletedAt,
                    ProfileIdQol = choreCompleted.ProfileIdQol ?? Guid.Empty,
                    ChoreId = choreCompleted.ChoreId ?? Guid.Empty,
                    HouseholdId = choreCompleted.HouseholdId ?? Guid.Empty
                });
            }
            return Ok(choreCompletedOutDtos);
        }

        [HttpGet]
        [Route("GetChoreCompletedByHouseholdIdAndByRange/{householdId:Guid}", Name = "GetChoreCompletedByRangeAsync")]
        public async Task<IActionResult> GetChoreCompletedByRangeAsync(Guid householdId, DateTime start, DateTime end)
        {
            List<ChoreCompleted> result = (List<ChoreCompleted>)await _choreCompletedRepository.GetListAsync(o => o.HouseholdId == householdId && o.CompletedAt >= start && o.CompletedAt <= end);
            if (result == null)
            {
                return NotFound();
            }
            List<ChoreCompletedOutDto> choreCompletedOutDtos = new();
            foreach (ChoreCompleted choreCompleted in result)
            {
                choreCompletedOutDtos.Add(new ChoreCompletedOutDto
                {
                    Id = choreCompleted.Id,
                    CompletedAt = choreCompleted.CompletedAt,
                    ProfileIdQol = choreCompleted.ProfileIdQol ?? Guid.Empty,
                    ChoreId = choreCompleted.ChoreId ?? Guid.Empty,
                    HouseholdId = choreCompleted.HouseholdId ?? Guid.Empty
                });
            }
            return Ok(choreCompletedOutDtos);
        }



        [HttpGet]
        [Route("GetChoreCompletedById/{Id:Guid}")]
        public async Task<IActionResult> GetChoreCompletedById(Guid Id)
        {
            ChoreCompleted? choreCompleted = await _choreCompletedRepository.GetByIdAsync(Id);
            if (choreCompleted == null)
            {
                return NotFound();
            }
            ChoreCompletedOutDto choreCompletedOutDto = new ChoreCompletedOutDto()
            {
                Id = choreCompleted.Id,
                CompletedAt = choreCompleted.CompletedAt,
                ProfileIdQol = choreCompleted.ProfileIdQol ?? Guid.Empty,
                ChoreId = choreCompleted.ChoreId ?? Guid.Empty,
                HouseholdId = choreCompleted.HouseholdId ?? Guid.Empty,
            };
            return Ok(choreCompletedOutDto);
        }


        [HttpPost]
        [Route("AddChoreCompleted", Name = "AddChoreCompletedAsync")]
        public async Task<IActionResult> AddChoreCompletedAsync([FromBody] ChoreCompletedInDto choreCompletedInDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ChoreCompleted choreCompleted = new ChoreCompleted
                {
                    CompletedAt = choreCompletedInDto.CompletedAt,
                    ProfileIdQol = choreCompletedInDto.ProfileIdQol,
                    ChoreId = choreCompletedInDto.ChoreId,
                    HouseholdId = choreCompletedInDto.HouseholdId,
                };
        
                ChoreCompleted? insertedChoreCompleted = await _choreCompletedRepository.InsertAsync(choreCompleted);
                if (insertedChoreCompleted != null)
                {
                    ChoreCompletedOutDto insertedChoreCompletedOutDto = new ChoreCompletedOutDto
                    {
                        Id = insertedChoreCompleted.Id,
                        CompletedAt = insertedChoreCompleted.CompletedAt,
                        ProfileIdQol = insertedChoreCompleted.ProfileIdQol ?? Guid.Empty,
                        ChoreId = insertedChoreCompleted.ChoreId ?? Guid.Empty,
                        HouseholdId = insertedChoreCompleted.HouseholdId ?? Guid.Empty,
                    };
                    return CreatedAtAction(nameof(GetChoreCompletedById), new { Id = insertedChoreCompletedOutDto.Id }, insertedChoreCompletedOutDto);
                }

                return StatusCode(500, "Hi fellow teammate, if you see this something is fuckedup in the BE, and its not your fault. Blame Jimmy");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message + "\n\n" + e.InnerException);
            }
        }
    }
}
