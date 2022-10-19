using Core.Entities;
using Core.Interfaces.Services;
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
        [Route("GetAllChoreCompleted", Name = "GetAllChoreCompletedAsync")]
        public async Task<IActionResult> GetAllChoreCompletedAsync()
        {
            List<ChoreCompleted>? result = (List<ChoreCompleted>?)await _choreCompletedRepository.GetListAsync(o => true);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetChoreCompletedByRange", Name = "GetChoreCompletedByRangeAsync")]
        public async Task<IActionResult> GetChoreCompletedByRangeAsync(DateTime start, DateTime end)
        {
            List<ChoreCompleted> result = (List<ChoreCompleted>)await _choreCompletedRepository.GetListAsync(o => o.CompletedAt >= start && o.CompletedAt <= end);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("AddChoreCompleted", Name = "AddChoreCompletedAsync")]
        public async Task<IActionResult> AddChoreCompletedAsync([FromBody] ChoreCompletedDto choreCompletedDto)
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
