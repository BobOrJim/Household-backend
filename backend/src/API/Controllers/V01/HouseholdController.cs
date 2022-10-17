using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V01
{
    [Route("api/V01/[controller]")]
    [ApiController]
    public class HouseholdController : ControllerBase
    {
        private readonly IRepository<Household> _householdRepository;
        private readonly IRepository<Profile> _profileRepository;

        public HouseholdController(IRepository<Household> householdRepository, IRepository<Profile> profileRepository)
        {
            _householdRepository = householdRepository;
            _profileRepository = profileRepository;
        }

        [HttpGet]
        [Route("GethouseholdByUserId/{id:Guid}", Name = "GetHouseholdObjectByUserIdAsync")]
        public async Task<IActionResult> GetHouseholdObjectByUserIdAsync(Guid userId)
        {
            Profile? profileObject = await _profileRepository.GetByIdAsync(userId);
            if (profileObject == null) return NotFound();
            Household? householdObject = await _householdRepository.GetByIdAsync(profileObject.HouseholdId);
            if (householdObject == null) return NotFound();

            return Ok(householdObject);
        }

        [HttpPost]
        [Route("AddHousehold", Name = "AddHouseholdAsync")]
        public async Task<IActionResult> AddHouseholdAsync([FromBody] HouseholdDto HouseholdDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Household household = new Household
                {
                    Name = HouseholdDto.Name,
                    Code = HouseholdDto.Code,
                };

                await _householdRepository.InsertAsync(household);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteHouseholdById/{id:Guid}", Name = "DeleteHouseholdByIdAsync")]
        public async Task<IActionResult> DeleteHouseholdByIdAsync(Guid id)
        {
            try
            {
                Household? household = await _householdRepository.GetByIdAsync(id);
                await _householdRepository.DeleteAsync(household);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }
    }
}
