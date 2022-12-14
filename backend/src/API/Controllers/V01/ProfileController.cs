using API.Dto;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace API.Controllers.V01
{
    // [Authorize]
    [Route("api/V01/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IRepository<Profile> _profileRepository;

        public ProfileController(IRepository<Profile> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        [HttpGet]
        [Route("GetByUserId/{id:Guid}")]
        public async Task<IActionResult> GetByAuthUserId(Guid id)
        {
            IEnumerable<Profile> profiles = await _profileRepository.GetListAsync(p => p.AuthUserId == id);
            if (profiles == null)
            {
                return NotFound();
            }
            IEnumerable<ProfileOutDto> profileDtos = profiles.Select(p => new ProfileOutDto
            {
                Id = p.Id,
                Alias = p.Alias,
                Avatar = p.Avatar,
                IsAdmin = p.IsAdmin,
                PendingRequest = p.PendingRequest,
                AuthUserId = p.AuthUserId,
                HouseholdId = p.HouseholdId
            });
            return Ok(profileDtos);
        }

        [HttpGet]
        [Route("GetByHouseholdId/{id:Guid}")]
        public async Task<IActionResult> GetByHouseholdId(Guid id)
        {
            IEnumerable<Profile> profiles = await _profileRepository.GetListAsync(p => p.HouseholdId == id);
            if (profiles == null)
            {
                return NotFound();
            }
            IEnumerable<ProfileOutDto> profileDtos = profiles.Select(p => new ProfileOutDto
            {
                Id = p.Id,
                Alias = p.Alias,
                Avatar = p.Avatar,
                IsAdmin = p.IsAdmin,
                PendingRequest = p.PendingRequest,
                AuthUserId = p.AuthUserId,
                HouseholdId = p.HouseholdId
            });

            return Ok(profileDtos);
        }

        [HttpGet]
        [Route("GetByProfileId/{id:Guid}")]
        public async Task<IActionResult> GetByProfileId(Guid id)
        {
            IEnumerable<Profile> profiles = await _profileRepository.GetListAsync(p => p.Id == id);
            if (profiles == null)
            {
                return NotFound();
            }
            IEnumerable<ProfileOutDto> profileDtos = profiles.Select(p => new ProfileOutDto
            {
                Id = p.Id,
                Alias = p.Alias,
                Avatar = p.Avatar,
                IsAdmin = p.IsAdmin,
                PendingRequest = p.PendingRequest,
                AuthUserId = p.AuthUserId,
                HouseholdId = p.HouseholdId
            });

            return Ok(profileDtos);
        }

        [HttpPost]
        [Route("CreateProfile")]
        [Authorize]
        public async Task<IActionResult> AddProfile([FromBody] ProfileCreateInDto profileCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var insertedUser = await _profileRepository.InsertAsync(new Profile()
                {
                    Alias = profileCreateDto.Alias,
                    Avatar = "pending",
                    IsAdmin = profileCreateDto.IsAdmin,
                    PendingRequest = !profileCreateDto.IsAdmin,
                    HouseholdId = profileCreateDto.HouseholdId,
                    AuthUserId = profileCreateDto.AuthUserId,
                });

                ProfileOutDto profileOutDto = new ProfileOutDto
                {
                    Id = insertedUser.Id,
                    Alias = insertedUser.Alias,
                    Avatar = insertedUser.Avatar,
                    IsAdmin = insertedUser.IsAdmin,
                    PendingRequest = insertedUser.PendingRequest,
                    HouseholdId = insertedUser.HouseholdId,
                    AuthUserId = insertedUser.AuthUserId
                };
                return CreatedAtAction(nameof(GetByProfileId), new { id = insertedUser.Id }, profileOutDto);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPatch]
        [Route("EditProfile/{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] ProfileUpdateInDto profileEditDto)
        {
            var existingProfile = await _profileRepository.GetByIdAsync(id);

            if (existingProfile == null)
            {
                return BadRequest("profile doesnt exist");
            }

            var updatedProfile = await _profileRepository.UpdateAsync(new Profile()
            {
                Id = existingProfile.Id,
                Alias = profileEditDto.Alias ?? existingProfile.Alias,
                IsAdmin = profileEditDto.IsAdmin ?? existingProfile.IsAdmin,
                Avatar = profileEditDto.Avatar ?? existingProfile.Avatar,
                PendingRequest = profileEditDto.PendingRequest ?? existingProfile.PendingRequest,
                AuthUserId = existingProfile.AuthUserId,
                HouseholdId = existingProfile.HouseholdId
            });

            if (updatedProfile == null)
            {
                return BadRequest();
            }

            return Ok(updatedProfile);
        }

        [HttpDelete]
        [Route("DeleteProfile/{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteProfile(Guid id)
        {
            var profileToDelete = await _profileRepository.GetByIdAsync(id);
            if (profileToDelete == null)
            {
                return BadRequest("Profile doesn't exist");
            }

            var result = await _profileRepository.DeleteAsync(profileToDelete);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}