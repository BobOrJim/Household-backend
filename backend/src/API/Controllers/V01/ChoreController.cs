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
            ChoreDto choreDto = new ChoreDto
            {
                Id = choreObject.Id,
                Name = choreObject.Name,
                Points = choreObject.Points,
                Description = choreObject.Description,
                PictureUrl = choreObject.PictureUrl,
                AudioUrl = choreObject.AudioUrl,
                Frequency = choreObject.Frequency,
                IsArchived = choreObject.IsArchived,
                HouseholdId = choreObject.HouseholdId
            };
            return Ok(choreDto);
        }

        [HttpGet]
        [Route("GetChoresByHouseholdId/{householdId:Guid}", Name = "GetChoresByHouseholdIdAsync")]
        public async Task<IActionResult> GetChoreByHouseholdIdAsync(Guid householdId)
        {
            IEnumerable<Chore> choreList = await _choreRepository.GetListAsync(c => c.HouseholdId == householdId);
            if (choreList == null)
            {
                return NotFound();
            }
            List<ChoreDto> choreDtoList = new List<ChoreDto>();
            foreach (Chore choreObject in choreList)
            {
                ChoreDto choreDto = new ChoreDto
                {
                    Id = choreObject.Id,
                    Name = choreObject.Name,
                    Points = choreObject.Points,
                    Description = choreObject.Description,
                    PictureUrl = choreObject.PictureUrl,
                    AudioUrl = choreObject.AudioUrl,
                    Frequency = choreObject.Frequency,
                    IsArchived = choreObject.IsArchived,
                    HouseholdId = choreObject.HouseholdId
                };
                choreDtoList.Add(choreDto);
            }
            return Ok(choreList);
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
                    HouseholdId = choreDto.HouseholdId,
                };

                await _choreRepository.InsertAsync(result);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
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
                chore.HouseholdId = choreDto.HouseholdId;


                await _choreRepository.UpdateAsync(chore);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteChore/{id:Guid}")]
        public async Task<IActionResult> DeleteChore(Guid id)
        {
            var choreToDelete = await _choreRepository.GetByIdAsync(id);
            if (choreToDelete == null)
            {
                return BadRequest("Chore doesn't exist");
            }

            var result = await _choreRepository.DeleteAsync(choreToDelete);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
