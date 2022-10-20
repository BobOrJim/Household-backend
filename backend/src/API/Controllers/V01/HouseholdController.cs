using System.Diagnostics;
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
        [Route("GetHouseholdByProfileId/{profileId:Guid}", Name = "GetHouseholdObjectByProfileIdAsync")]
        public async Task<IActionResult> GetHouseholdObjectByProfileIdAsync(Guid profileId)
        {
            try
            {
                Profile? profileObject = await _profileRepository.GetByIdAsync(profileId);
                if (profileObject == null) return NotFound("Provided profileID not found");
                Household? householdObject = await _householdRepository.GetByIdAsync(profileObject.HouseholdId);
                if (householdObject == null) return NotFound("Profile found, but no household by ID is tied to it(this should be impossible :o)");

                HouseholdDto result = new HouseholdDto
                {
                    Code = householdObject.Code,
                    Name = householdObject.Name,
                };

                return Ok(householdObject);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("GetHouseholdById/(id:Guid)")]
        public async Task<IActionResult> GetHouseholdById(Guid id)
        {
            var household = await _householdRepository.GetByIdAsync(id);

            if(household == null)
            {
                return BadRequest();
            }

            return Ok(household);
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
                return Ok(household);
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
                if (household == null) return NotFound("ID could not be found");
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
