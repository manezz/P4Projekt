namespace WebApi.Service
{
    public interface ILoginService
    {
        Task<SignInResponse> AuthenticateAsync(SignInRequest sign);
        Task<LoginResponse> CreateAsync(LoginRequest newUser);
        Task<List<LoginResponse>> GetAllAsync();
        Task<LoginResponse> FindByIdAsync(int loginId);
        Task<LoginResponse?> UpdateAsync(int loginId, LoginRequest updatedLogin);
        Task<LoginResponse?> DeleteByIdAsync(int loginId);
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
                    UserImage = new LoginUserUserImageResponse
                    {
                        Image = Convert.ToBase64String(login.User.UserImage.Image)
                    },
                    Posts = login.User.Posts.Select(post => new UserPostLoginResponse
                    {
                        PostId = post.PostId,
                        Title = post.Title,
                        Desc = post.Desc,
                        Date = post.Date,
                        PostLikes = new UserPostLoginPostLikesResponse
                        {
                            Likes = post.PostLikes.Likes
                        }
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
                    UserName = loginRequest.User.UserName,
                    UserImage = new()
                }
            };
        }

        public async Task<SignInResponse> AuthenticateAsync(SignInRequest signIn)
        {
            var login = await _loginRepository.GetByEmailAsync(signIn.Email);

            if (login != null)
            {
                if (login.Password == signIn.Password)
                {
                    SignInResponse response = new()
                    {
                        LoginId = login.LoginId,
                        Email = login.Email,
                        Role = login.Role,
                        User = new()
                        {
                            UserId = login.User.UserId,
                            UserName = login.User.UserName,
                            Created = login.User.Created,
                            UserImage = new()
                            {
                                Image = Convert.ToBase64String(login.User.UserImage.Image)
                            }
                        },
                        Token = _jwtUtils.GenerateJwtToken(login)
                    };
                    return response;
                }
            }
            return null!;
        }

        public async Task<LoginResponse> CreateAsync(LoginRequest newUser)
        {
            var user = await _loginRepository.CreateAsync(MapLoginRequestToLogin(newUser));

            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return MapLoginToLoginResponse(user);
        }

        public async Task<List<LoginResponse>> GetAllAsync()
        {
            List<Login> logins = await _loginRepository.GetAllAsync();

            if (logins == null)
            {
                throw new ArgumentNullException();
            }

            return logins.Select(login => MapLoginToLoginResponse(login)).ToList();
        }

        public async Task<LoginResponse> FindByIdAsync(int loginId)
        {
            var login = await _loginRepository.GetByIdAsync(loginId);

            if (login != null)
            {
                return MapLoginToLoginResponse(login);
            }

            return null!;
        }

        public async Task<LoginResponse?> UpdateAsync(int loginId, LoginRequest updatedLogin)
        {
            var login = await _loginRepository.UpdateByIdAsync(loginId, MapLoginRequestToLogin(updatedLogin));

            if (login != null)
            {
                return MapLoginToLoginResponse(login);
            }

            return null;
        }

        public async Task<LoginResponse?> DeleteByIdAsync(int loginId)
        {
            var login = await _loginRepository.DeleteByIdAsync(loginId);

            if (login != null)
            {
                return MapLoginToLoginResponse(login);
            }

            return null;
        }
    }
}

