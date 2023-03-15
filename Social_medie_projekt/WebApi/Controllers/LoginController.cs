namespace WebApi.Controllers
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
        public async Task<IActionResult> Authenticate([FromBody] SignInRequest login)
        {
            try
            {
                SignInResponse? response = await _loginService.AuthenticateUser(login);

                if (response == null)
                {
                    return Unauthorized();
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
        public async Task<IActionResult> RegisterAsync([FromBody] LoginRequest register)
        {
            try
            {
                LoginResponse loginResponse = await _loginService.RegisterAsync(register);

                return Ok(loginResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllLoginAsync()
        {
            try
            {
                List<LoginResponse> logins = await _loginService.GetAllLoginAsync();

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
        public async Task<IActionResult> FindLoginByIdAsync([FromRoute] int loginId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["User"];

                if (currentUser != null && loginId != currentUser.LoginId && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                var loginResponse = await _loginService.FindLoginByIdAsync(loginId);

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
        public async Task<IActionResult> UpdateLoginByIdAsync([FromRoute] int loginId, [FromBody] LoginRequest updatedLogin)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["User"];

                if (currentUser != null && loginId != currentUser.LoginId && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                var loginResponse = await _loginService.UpdateLoginAsync(loginId, updatedLogin);

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
        public async Task<IActionResult> DeleteLoginByIdAsync([FromRoute] int loginId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["User"];

                if (currentUser != null && loginId != currentUser.LoginId && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                var loginResponse = await _loginService.DeleteLoginAsync(loginId);

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
