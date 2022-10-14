using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V01
{
    [Route("api/V01/[controller]")]
    [ApiController]
    public class PauseController : ControllerBase
    {
        private readonly IRepository<Pause> _pauseRepository;

        public PauseController(IRepository<Pause> pauseRepository)
        {
            _pauseRepository = pauseRepository;
        }

        [HttpGet]
        [Route("GetPauseById/{Guid}", Name = "GetPauseByIdAsync")]
        public async Task<IActionResult> GetPauseByIdAsync(Guid id)
        {
            Pause? pauseObject = await _pauseRepository.GetByIdAsync(id);
            if (pauseObject == null)
            {
                return NotFound();
            }
            return Ok(pauseObject);
        }

        [HttpPost]
        [Route("AddPause", Name = "AddPauseAsync")]
        public async Task<IActionResult> AddPauseAsync([FromBody] PauseDto pauseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Pause result = new Pause
                {
                    StartDate = pauseDto.StartDate,
                    EndDate = pauseDto.EndDate,
                    ProfileId = pauseDto.ProfileId,
                };

                await _pauseRepository.InsertAsync(result);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        [Route("UpdatePause/{id:Guid}", Name = "UpdatePauseAsync")]
        public async Task<IActionResult> UpdatePauseAsync([FromBody] PauseDto pauseDto, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Pause? pause = await _pauseRepository.GetByIdAsync(id);

                pause.StartDate = pauseDto.StartDate;
                pause.EndDate = pauseDto.EndDate;
                pause.ProfileId = pauseDto.ProfileId;

                await _pauseRepository.UpdateAsync(pause);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }

        /* [HttpPost] //Return Ok()
        [Route("AddUser", Name = "AddUserAsync")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                User user = new User
                {
                    Alias = userDto.Alias,
                    PhoneNr = userDto.PhoneNr,
                    IsLoggedIn = userDto.IsLoggedIn,
                    ProfilePictureUrl = userDto.ProfilePictureUrl,
                    ContactEmail = userDto.ContactEmail,
                    AuthId = Guid.Parse(userDto.AuthId),
                };
                await _iUserService.InsertAsync(user);
                return Ok();
                //return CreatedAtRoute("GetUserById", new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet] //Return User
        [Route("GetUserById/{id:Guid}", Name = "GetUserByIdAsync")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            User? user = await _iUserService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet] //Return User[]
        [Route("GetAllUsers", Name = "GetAllUsersAsync")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            IEnumerable<User> users = await _iUserService.GetAllAsync(u => true);
            return Ok(users);
        }

        [HttpPatch] //Return User Auth
        //[Authorize]
        [Route("UpdateUser/{id:Guid}", Name = "UpdateUserAsync")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                User user = new User
                {
                    Id = id,
                    Alias = userDto.Alias,
                    PhoneNr = userDto.PhoneNr,
                    IsLoggedIn = userDto.IsLoggedIn,
                    ProfilePictureUrl = userDto.ProfilePictureUrl,
                    ContactEmail = userDto.ContactEmail,
                    AuthId = Guid.Parse(userDto.AuthId),
                };
                if (await _iUserService.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                await _iUserService.UpdateAsync(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete] //Return Ok() Auth
        //[Authorize]
        [Route("DeleteUserById/{id:Guid}", Name = "DeleteUserByIdAsync")]
        public async Task<IActionResult> DeleteUserByIdAsync(Guid id)
        {
            Debug.WriteLine("1");
            try
            {
                Debug.WriteLine("2");
                var user = await _iUserService.GetByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                Debug.WriteLine("3");
                await _iUserService.DeleteAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        } */

    }
}
