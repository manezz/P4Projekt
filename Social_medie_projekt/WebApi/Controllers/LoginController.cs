namespace WebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] SignInRequest login)
        {
            try
            {
                SignInResponse? response = await _loginService.AuthenticateAsync(login);

                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateAsync([FromBody] LoginRequest register)
        {
            try
            {
                LoginResponse loginResponse = await _loginService.CreateAsync(register);

                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<LoginResponse> logins = await _loginService.GetAllAsync();

                if (logins.Count == 0)
                {
                    return NoContent();
                }

                return Ok(logins);

            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        [Route("{loginId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int loginId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                if (currentUser == null || loginId != currentUser.LoginId && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                var loginResponse = await _loginService.FindByIdAsync(loginId);

                if (loginResponse == null)
                {
                    return NotFound();
                }

                return Ok(loginResponse);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpPut]
        [Route("{loginId}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int loginId, [FromBody] LoginRequest updatedLogin)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                if (currentUser == null || loginId != currentUser.LoginId && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                var loginResponse = await _loginService.UpdateAsync(loginId, updatedLogin);

                if (loginResponse == null)
                {
                    return NotFound();
                }

                return Ok(loginResponse);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpDelete]
        [Route("{loginId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int loginId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                if (currentUser == null || loginId != currentUser.LoginId && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                var loginResponse = await _loginService.DeleteAsync(loginId);

                if (loginResponse == null)
                {
                    return NotFound();
                }

                return Ok(loginResponse);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}
