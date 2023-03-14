namespace WebApi.Service
{
    public interface ILoginService
    {
        Task<LoginResponse> AuthenticateUser(LoginRequest login);
        Task<LoginResponse> RegisterAsync(LoginRequest newUser);
        Task<List<LoginResponse>> GetAllLoginAsync();
        Task<LoginResponse> FindLoginByIdAsync(int loginId);
        Task<LoginResponse?> UpdateLoginAsync(int loginId, LoginRequest updatedLogin);
        Task<LoginResponse?> DeleteLoginAsync(int loginId);
    }
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IJwtUtils _jwtUtils;

        public LoginService(ILoginRepository loginRepository, IJwtUtils jwtUtils)
        {
            _loginRepository = loginRepository;
            _jwtUtils = jwtUtils;
        }

        private static LoginResponse MapLoginToLoginResponse(Login login)
        {
            return new LoginResponse
            {
                LoginId = login.LoginId,
                Email = login.Email,
                Role = login.Role,

                User = new LoginUserResponse
                {
                    UserId = login.User.UserId,
                    UserName = login.User.UserName,
                    Created = login.User.Created,
                    Posts = login.User.Posts.Select(x => new UserPostLoginResponse
                    {
                        PostId = x.PostId,
                        Title = x.Title,
                        Desc = x.Desc,

                        Date = x.Date
                    }).ToList()
                }
            };
        }

        private static Login MapLoginRequestToLogin(LoginRequest loginRequest)
        {
            return new Login
            {
                Email = loginRequest.Email,
                Role = loginRequest.Role,
                Password = loginRequest.Password,
                User = new()
                {
                    UserName = loginRequest.User.UserName
                }
            };
        }

        public async Task<LoginResponse> AuthenticateUser(LoginRequest login)
        {
            Login? foundLogin = await _loginRepository.FindLoginByEmailAsync(login.Email);

            if (foundLogin == null)
            {
                throw new ArgumentNullException();
            }

            if (foundLogin.Password == login.Password)
            {
                LoginResponse response = new()
                {
                    LoginId = foundLogin.LoginId,
                    Email = foundLogin.Email,
                    Role = foundLogin.Role,
                    User = new()
                    {
                        UserId = foundLogin.User.UserId,
                        UserName = foundLogin.User.UserName,
                        Created = foundLogin.User.Created,
                    },
                    Token = _jwtUtils.GenerateJwtToken(foundLogin)
                };
                return response;
            }
            return null;
        }

        public async Task<LoginResponse> RegisterAsync(LoginRequest newUser)
        {
            var user = await _loginRepository.RegisterAsync(MapLoginRequestToLogin(newUser));

            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return MapLoginToLoginResponse(user);
        }

        public async Task<List<LoginResponse>> GetAllLoginAsync()
        {
            List<Login> logins = await _loginRepository.GetAllLoginAsync();

            if (logins == null)
            {
                throw new ArgumentNullException();
            }

            return logins.Select(login => MapLoginToLoginResponse(login)).ToList();
        }

        public async Task<LoginResponse> FindLoginByIdAsync(int loginId)
        {
            var login = await _loginRepository.FindLoginByIdAsync(loginId);

            if (login != null)
            {
                return MapLoginToLoginResponse(login);
            }

            return null;
        }

        public async Task<LoginResponse?> UpdateLoginAsync(int loginId, LoginRequest updatedLogin)
        {
            var login = await _loginRepository.UpdateLoginById(loginId, MapLoginRequestToLogin(updatedLogin));

            if (login != null)
            {
                return MapLoginToLoginResponse(login);
            }

            return null;
        }

        public async Task<LoginResponse?> DeleteLoginAsync(int loginId)
        {
            var login = await _loginRepository.DeleteLoginByIdAsync(loginId);

            if (login != null)
            {
                return MapLoginToLoginResponse(login);
            }

            return null;
        }
    }
}

