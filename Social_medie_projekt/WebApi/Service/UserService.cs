namespace WebApi.Service
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsers();
        Task<UserResponse?> FindUserAsync(int id);
        Task<UserResponse> CreateUserAsync(UserRequest newUser);
        Task<UserResponse?> UpdateUserAsync(int id, UserRequest updatedUser);
        Task<UserResponse?> DeleteUserAsync(int id);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private static UserResponse MapUserToUserResponse(User user)
        {
            return new UserResponse
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Created = user.Created,
                Login = new UserLoginResponse
                {
                    LoginId = user.Login.LoginId,
                    Email = user.Login.Email,
                    Role = user.Login.Role
                },
                Posts = user.Posts.Select(post => new UserPostResponse
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Desc = post.Desc,
                    PostLikes = new UserPostPostLikesResponse
                    {
                        Likes = post.PostLikes.Likes
                    },
                    Date = post.Date
                }).ToList()
            };
        }

        private static User MapUserRequestToUser(UserRequest userRequest)
        {
            return new User
            {
                UserName = userRequest.UserName,
                Login = new()
                {
                    Email = userRequest.Login.Email,
                    Password = userRequest.Login.Password,
                    Role = userRequest.Login.Role
                },
            };
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest newUser)
        {
            var user = await _userRepository.CreateUserAsync(MapUserRequestToUser(newUser));

            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return MapUserToUserResponse(user);
        }

        public async Task<UserResponse?> DeleteUserAsync(int id)
        {
            var user = await _userRepository.DeleteUserAsync(id);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }
            return null;
        }

        public async Task<UserResponse?> FindUserAsync(int id)
        {
            var user = await _userRepository.FindUserByIdAsync(id);

            if (user != null)
            {
                return MapUserToUserResponse(user);
            }
            return null;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            List<User> user = await _userRepository.GetAllUsersAsync();

            if (user == null)
            {
                throw new ArgumentNullException();
            }
            return user.Select(user => MapUserToUserResponse(user)).ToList();
        }

        public async Task<UserResponse?> UpdateUserAsync(int id, UserRequest updatedUser)
        {
            var user = await _userRepository.UpdateUserAsync(id, MapUserRequestToUser(updatedUser));


            if (user != null)
            {
                return MapUserToUserResponse(user);
            }
            return null;
        }
    }
}
