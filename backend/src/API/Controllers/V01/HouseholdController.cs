using System;
using System.Diagnostics;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V01
{
    [Route("api/V01/[controller]")]
    [ApiController]
    public class HouseholdController : ControllerBase
    {
        private readonly IRepository<Household> _householdRepository;
        private readonly IRepository<Profile> _profileRepository;
        private static Random random = new Random();

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

                HouseholdOutDto householdOutDto = new HouseholdOutDto
                {
                    Id = householdObject.Id,
                    Code = householdObject.Code,
                    Name = householdObject.Name,
                };

                return Ok(householdOutDto);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        [Route("GetHouseholdById/{id:Guid}")]
        public async Task<IActionResult> GetHouseholdById(Guid id)
        {
            Household? household = await _householdRepository.GetByIdAsync(id);

            if (household == null)
            {
                return NotFound("ID could not be found");
            }
            HouseholdOutDto householdOutDto = new HouseholdOutDto
            {
                Id = household.Id,
                Code = household.Code,
                Name = household.Name,
            };

            return Ok(householdOutDto);
        }

        [HttpGet]
        [Route("GetHouseholdByHouseholdCode/{code}", Name = "GetHouseholdByHouseholdCodeAsync")]
        public async Task<IActionResult> GetHouseholdByHouseholdCodeAsync(string code)
        {
            try
            {
                Household? household = (await _householdRepository.GetListAsync(x => x.Code == code)).FirstOrDefault();
                if (household == null)
                {
                    return NotFound("Code could not be found");
                }

                HouseholdOutDto householdOutDto = new HouseholdOutDto
                {
                    Id = household.Id,
                    Code = household.Code,
                    Name = household.Name
                };

                return Ok(householdOutDto);
            }
            catch (Exception e)
            {
                return NotFound("Something went wrong\n\n" + e.InnerException);
            }
        }

        [HttpPost]
        [Route("AddHousehold", Name = "AddHouseholdAsync")]
        [Authorize]
        public async Task<IActionResult> AddHouseholdAsync([FromBody] HouseholdInDto HouseholdInDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Household household = new Household
                {
                    Name = HouseholdInDto.Name,
                    Code = getRandomHouseholdCode(5),
                };

                Household insertedHousehold = await _householdRepository.InsertAsync(household);

                if (insertedHousehold != null)
                {
                    HouseholdOutDto insertedHouseholdOutDto = new HouseholdOutDto
                    {
                        Id = insertedHousehold.Id,
                        Name = insertedHousehold.Name,
                        Code = insertedHousehold.Code
                    };
                    return CreatedAtAction(nameof(GetHouseholdById), new { Id = insertedHouseholdOutDto.Id }, insertedHouseholdOutDto);
                }
            }

            catch (Exception e)
            {
                return StatusCode(500, e.Message + "\n\n" + e.InnerException);
            }
            return StatusCode(500, "Hi fellow teammate, if you see this something is fuckedup in the BE, and its not your fault. Blame Jimmy");
        }

        [HttpPatch]
        [Route("EditHousehold/{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateHousehold(Guid id, [FromBody] HouseholdInDto householdEditDto)
        {
            var existingHousehold = await _householdRepository.GetByIdAsync(id);

            if (existingHousehold == null)
            {
                return BadRequest("Household does not exist");
            }

            var updatedHousehold = await _householdRepository.UpdateAsync(new Household()
            {
                Id = existingHousehold.Id,
                Name = householdEditDto.Name ?? existingHousehold.Name,
                Code = existingHousehold.Code,
            });

            if (updatedHousehold == null)
            {
                return BadRequest();
            }

            return Ok(updatedHousehold);
        }

        [HttpDelete]
        [Route("DeleteHouseholdById/{id:Guid}", Name = "DeleteHouseholdByIdAsync")]
        [Authorize]
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

        private string getRandomHouseholdCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
    }
}
