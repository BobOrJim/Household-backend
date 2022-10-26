using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        [Route("GetAllChoreCompletedByHouseholdId/{householdId:Guid}", Name = "GetAllChoreCompletedAsync")]
        public async Task<IActionResult> GetAllChoreCompletedAsync(Guid householdId)
        {
            List<ChoreCompleted>? result = (List<ChoreCompleted>?)await _choreCompletedRepository.GetListAsync(o => o.HouseholdId == householdId);
            if (result == null)
            {
                return NotFound();
            }
            List<ChoreCompletedInOutDto> choreCompletedDtos = new();
            foreach (ChoreCompleted choreCompleted in result)
            {
                choreCompletedDtos.Add(new ChoreCompletedInOutDto
                {
                    Id = choreCompleted.Id,
                    CompletedAt = choreCompleted.CompletedAt,
                    ProfileIdQol = choreCompleted.ProfileIdQol ?? Guid.Empty,
                    ChoreId = choreCompleted.ChoreId ?? Guid.Empty,
                    HouseholdId = choreCompleted.HouseholdId ?? Guid.Empty
                });
            }
            return Ok(choreCompletedDtos);
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
            List<ChoreCompletedInOutDto> choreCompletedDtos = new();
            foreach (ChoreCompleted choreCompleted in result)
            {
                choreCompletedDtos.Add(new ChoreCompletedInOutDto
                {
                    Id = choreCompleted.Id,
                    CompletedAt = choreCompleted.CompletedAt,
                    ProfileIdQol = choreCompleted.ProfileIdQol ?? Guid.Empty,
                    ChoreId = choreCompleted.ChoreId ?? Guid.Empty,
                    HouseholdId = choreCompleted.HouseholdId ?? Guid.Empty
                });
            }
            return Ok(choreCompletedDtos);
        }

        [HttpPost]
        [Route("AddChoreCompleted", Name = "AddChoreCompletedAsync")]
        public async Task<IActionResult> AddChoreCompletedAsync([FromBody] ChoreCompletedInOutDto choreCompletedDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ChoreCompleted result = new ChoreCompleted
                {
                    CompletedAt = choreCompletedDto.CompletedAt,
                    ProfileIdQol = choreCompletedDto.ProfileIdQol,
                    ChoreId = choreCompletedDto.ChoreId,
                    HouseholdId = choreCompletedDto.HouseholdId,
                };
        
        await _choreCompletedRepository.InsertAsync(result);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message + "\n\n" + e.InnerException);
            }
        }
    }
}
