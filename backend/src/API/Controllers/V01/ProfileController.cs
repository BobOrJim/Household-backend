using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace API.Controllers.V01
{
    [Authorize]
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
        public async Task<IActionResult> GetByUserId(Guid id)
        {
            var profiles = await _profileRepository.GetListAsync(p => p.AuthUserId == id);
            Console.WriteLine(profiles);
            if (profiles == null)
            {
                return NotFound();
            }

            return Ok(profiles);
        }

        [HttpGet]
        [Route("GetByHouseholdId/{id:Guid}")]
        public async Task<IActionResult> GetByHouseholdId(Guid id)
        {
            var profiles = await _profileRepository.GetListAsync(p => p.HouseholdId == id);
            if (profiles == null)
            {
                return NotFound();
            }

            return Ok(profiles);
        }


        [HttpPost]
        [Route("CreateProfile")]
        public async Task<IActionResult> AddProfile([FromBody] ProfileCreateDto profileDto)
        {

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var authUserId = identity?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/anonymous")?.Value; // detta är inte det snyggaste lol
            if (authUserId == null)
            {
                return Unauthorized();
            }

            var insertedUser = await _profileRepository.InsertAsync(new Profile()
            {
                Alias = profileDto.Alias,
                Avatar = "pending",
                Color = "#262626",
                IsAdmin = profileDto.IsAdmin,
                PendingRequest = profileDto.IsAdmin ? false : true,
                HouseholdId = profileDto.HouseholdId ?? new Guid(),
                AuthUserId = new Guid(authUserId),
            });

            if (insertedUser == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetByProfileId), new { id = insertedUser.Id }, insertedUser);
        }

        [HttpPatch]
        [Route("EditProfile/{id:Guid}")]
        public async Task<IActionResult> UpdateProfile(Guid id, [FromBody] ProfileUpdateDto profileDto)
        {
            var existingProfile = await _profileRepository.GetByIdAsync(id);

            if (existingProfile == null)
            {
                return BadRequest("profile doesnt exist");
            }

            var updatedProfile = await _profileRepository.UpdateAsync(new Profile()
            {
                Alias = profileDto.Alias ?? existingProfile.Alias,
                IsAdmin = profileDto.IsAdmin ?? existingProfile.IsAdmin,
                Avatar = profileDto.Avatar ?? existingProfile.Avatar,
                Color = profileDto.Color ?? existingProfile.Color,
                PendingRequest = profileDto.PendingRequest ?? existingProfile.PendingRequest,
                AuthUserId = existingProfile.AuthUserId,
                Household = existingProfile.Household,
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

        private async Task<IActionResult> GetByProfileId(Guid id)
        {
            var profiles = await _profileRepository.GetListAsync(p => p.Id == id);
            if (profiles == null)
            {
                return NotFound();
            }

            return Ok(profiles);
        }
    }
}