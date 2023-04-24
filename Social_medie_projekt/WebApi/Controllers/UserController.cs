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

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                List<UserResponse> users = await _userService.GetAllUsersAsync();

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

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> FindUserById([FromRoute] int userId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                var userResponse = await _userService.FindUserAsync(userId, currentUser.User.UserId);

                if (userResponse == null)
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

        [Authorize(Role.User, Role.Admin)]
        [HttpPut]
        [Route("{userid}")]
        public async Task<IActionResult> EditUser([FromRoute] int userId, [FromBody] UserRequest updatedUser)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                if (currentUser != null && userId != currentUser.User.UserId && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthrized" });
                }

                var userResponse = await _userService.UpdateUserAsync(userId, updatedUser);

                if (userResponse == null)
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
    }
}
