using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V01
{
    [Route("api/V01/[controller]")]
    [ApiController]
    public class ChoreController : ControllerBase
    {
        private readonly IRepository<Chore> _choreRepository;

        public ChoreController(IRepository<Chore> choreRepository)
        {
            _choreRepository = choreRepository;
        }

        [HttpGet]
        [Route("GetChoreById/{id:Guid}", Name = "GetChoreByIdAsync")]
        public async Task<IActionResult> GetChoreByIdAsync(Guid id)
        {
            Chore? choreObject = await _choreRepository.GetByIdAsync(id);
            if (choreObject == null)
            {
                return NotFound();
            }
            return Ok(choreObject);
        }

        [HttpPost]
        [Route("AddChore", Name = "AddChoreAsync")]
        public async Task<IActionResult> AddChoreAsync([FromBody] ChoreDto choreDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Chore result = new Chore
                {
                    Name = choreDto.Name,
                    Points = choreDto.Points,
                    Description = choreDto.Description,
                    PictureUrl = choreDto.PictureUrl,
                    AudioUrl = choreDto.AudioUrl,
                    Frequency = choreDto.Frequency,
                    IsArchived = choreDto.IsArchived,
                    Household = choreDto.Household,
                    HouseholdId = choreDto.HouseholdId,
                    choresCompleted = choreDto.choresCompleted,
                };

                await _choreRepository.InsertAsync(result);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("UpdateChore/{id:Guid}", Name = "UpdateChoreAsync")]
        public async Task<IActionResult> UpdateChoreAsync([FromBody] ChoreDto choreDto, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Chore? chore = await _choreRepository.GetByIdAsync(id);

                chore.Name = choreDto.Name;
                chore.Points = choreDto.Points;
                chore.Description = choreDto.Description;
                chore.PictureUrl = choreDto.PictureUrl;
                chore.AudioUrl = choreDto.AudioUrl;
                chore.Frequency = choreDto.Frequency;
                chore.IsArchived = choreDto.IsArchived;
                chore.Household = choreDto.Household;
                chore.HouseholdId = choreDto.HouseholdId;
                chore.choresCompleted = choreDto.choresCompleted;


                await _choreRepository.UpdateAsync(chore);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}
