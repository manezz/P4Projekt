namespace WebApi.Service
{
    public interface ILoginService
    {
        Task<SignInResponse> AuthenticateUser(SignInRequest login);
        Task<LoginResponse> RegisterAsync(UserSignupRequest newUser);
        Task<List<LoginResponse>> GetAllLoginAsync();
        Task<LoginResponse> CreateLoginAsync(LoginRequest newLogin);
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

        private LoginResponse MapLoginToLoginResponse(Login login)
        {
            if (login.User == null)
            {
                return new LoginResponse
                {
                    LoginId = login.LoginId,
                    Email = login.Email,
                    Type = login.Type,
                };
            }
            else
            {
                return new LoginResponse
                {
                    LoginId = login.LoginId,
                    Email = login.Email,
                    Type = login.Type,

                    User = new LoginUserResponse
                    {
                        UserId = login.User.UserId,
                        UserName = login.User.UserName,
                        Created = login.User.Created
                    }
                };
            }

        }

        private Login MapLoginRequestToLogin(LoginRequest loginRequest)
        {
            return new Login
            {
                Email = loginRequest.Email,
                Type = loginRequest.Type,
                Password = loginRequest.Password
            };
        }

        private Login MapCustomerSignupRequestToLogin(UserSignupRequest userSignupRequest)
        {
            return new Login
            {
                Email = userSignupRequest.Email,
                Type = Role.user,
                Password = userSignupRequest.Password,
                User = new()
                {
                    UserName = userSignupRequest.User.UserName,
                }
            };
        }


        public async Task<SignInResponse> AuthenticateUser(SignInRequest login)
        {
            Login user = await _loginRepository.FindLoginByEmailAsync(login.Email);
            if (user == null)
            {
                return null;
            }

            if (user.Password == login.Password)
            {
                SignInResponse response = new()
                {
                    LoginResponse = new()
                    {
                        LoginId = user.LoginId,
                        Email = user.Email,
                        Type = user.Type,
                        User = new()
                        {
                            UserId = user.User.UserId,
                            UserName = user.User.UserName,
                            Created = user.User.Created,
                            Posts = user.User.Posts.Select(x => new UserPostLoginResponse
                            {
                                PostId = x.PostId,
                                Title = x.Title,
                                Desc = x.Desc,
                                Tags = x.Tags,
                                Likes = x.Likes,
                                Date = x.Date
                            }).ToList()
                        }

                    },
                    Token = _jwtUtils.GenerateJwtToken(user)
                };
                return response;
            }
            return null;
        }

        public async Task<LoginResponse> RegisterAsync(UserSignupRequest newUser)
        {
            var user = await _loginRepository.RegisterAsync(MapCustomerSignupRequestToLogin(newUser));

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

        public async Task<LoginResponse> CreateLoginAsync(LoginRequest newLogin)
        {
            var login = await _loginRepository.CreateLoginAsync(MapLoginRequestToLogin(newLogin));

            if (login == null)
            {

                throw new ArgumentNullException();
            }

            return MapLoginToLoginResponse(login);
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

