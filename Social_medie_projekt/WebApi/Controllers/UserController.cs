using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                List<UserResponse> users = await _userService.GetAllUsers();

                if (users.Count == 0)
                {
                    return NotFound();
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> FindUserById([FromRoute] int userId)
        {
            try
            {
                LoginResponse currentUser = (LoginResponse)HttpContext.Items["User"];

                if(currentUser != null && userId != currentUser.User.UserId && currentUser.Type != Role.admin)
                {
                    return Unauthorized(new { message = "Unauthorized"});
                }

                var userResponse = await _userService.FindUserAsync(userId);

                if(userResponse == null)
                {
                    return NotFound();
                }
                return Ok(userResponse);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserRequest newUser)
        {
            try
            {
                UserResponse userResponse = await _userService.CreateUserAsync(newUser);

                return Ok(userResponse);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{userid}")]
        public async Task<IActionResult> EditUser([FromRoute]int userId, [FromBody] UserRequest updatedUser)
        {
            try
            {
                LoginResponse currentUser = (LoginResponse)HttpContext.Items["User"];

                if(currentUser != null && userId != currentUser.User.UserId && currentUser.Type != Role.admin)
                {
                    return Unauthorized(new {message = "Unauthrized"});
                }

                var userResponse = await _userService.UpdateUserAsync(userId, updatedUser);

                if(userResponse == null)
                {
                    return NotFound();
                }

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteUserById([FromRoute]int userId)
        {
            try
            {
                LoginResponse currentUser = (LoginResponse)HttpContext.Items["User"];

                if (currentUser != null && userId != currentUser.User.UserId && currentUser.Type != Role.admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                var user = await _userService.DeleteUserAsync(userId);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
                throw;
            }
        }
        
    }
}
