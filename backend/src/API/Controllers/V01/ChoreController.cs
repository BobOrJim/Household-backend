using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Route("GetChoreById/{id:Guid}", Name = "GetChoreById")]
        public async Task<IActionResult> GetChoreByIdAsync(Guid id)
        {
            Chore? choreObject = await _choreRepository.GetByIdAsync(id);
            if (choreObject == null)
            {
                return NotFound();
            }
            ChoreOutDto choreDto = new ChoreOutDto
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
            List<ChoreOutDto> choreDtoList = new List<ChoreOutDto>();
            foreach (Chore choreObject in choreList)
            {
                ChoreOutDto choreDto = new ChoreOutDto
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
            return Ok(choreDtoList);
        }

        [HttpPost]
        [Route("AddChore", Name = "AddChoreAsync")]
        [Authorize]
        public async Task<IActionResult> AddChoreAsync([FromBody] ChoreInDto choreInDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Chore chore = new Chore
                {
                    Name = choreInDto.Name,
                    Points = choreInDto.Points,
                    Description = choreInDto.Description,
                    PictureUrl = choreInDto.PictureUrl,
                    AudioUrl = choreInDto.AudioUrl,
                    Frequency = choreInDto.Frequency,
                    IsArchived = choreInDto.IsArchived,
                    HouseholdId = choreInDto.HouseholdId,
                };

                var insertedChore = await _choreRepository.InsertAsync(chore);

                return CreatedAtAction("GetChoreById", new { id = insertedChore.Id }, new ChoreOutDto()
                {
                    Id = insertedChore.Id,
                    Name = insertedChore.Name,
                    Points = insertedChore.Points,
                    Description = insertedChore.Description,
                    PictureUrl = insertedChore.PictureUrl,
                    AudioUrl = insertedChore.AudioUrl,
                    Frequency = insertedChore.Frequency,
                    IsArchived = insertedChore.IsArchived,
                    HouseholdId = insertedChore.HouseholdId,
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("UpdateChore/{id:Guid}", Name = "UpdateChoreAsync")]
        [Authorize]
        public async Task<IActionResult> UpdateChoreAsync([FromBody] ChoreInDto choreInDto, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Chore? chore = await _choreRepository.GetByIdAsync(id);

                chore.Name = choreInDto.Name;
                chore.Points = choreInDto.Points;
                chore.Description = choreInDto.Description;
                chore.PictureUrl = choreInDto.PictureUrl;
                chore.AudioUrl = choreInDto.AudioUrl;
                chore.Frequency = choreInDto.Frequency;
                chore.IsArchived = choreInDto.IsArchived;
                chore.HouseholdId = choreInDto.HouseholdId;

                var updatedChore = await _choreRepository.UpdateAsync(chore);
                return Ok(new ChoreOutDto()
                {
                    Id = updatedChore.Id,
                    Name = updatedChore.Name,
                    Points = updatedChore.Points,
                    Description = updatedChore.Description,
                    PictureUrl = updatedChore.PictureUrl,
                    AudioUrl = updatedChore.AudioUrl,
                    Frequency = updatedChore.Frequency,
                    IsArchived = updatedChore.IsArchived,
                    HouseholdId = updatedChore.HouseholdId,
                });
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteChore/{id:Guid}")]
        [Authorize]
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
