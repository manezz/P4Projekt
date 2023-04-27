namespace WebApi.Service
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsersAsync();
        Task<UserResponse> FindUserAsync(int userId, int followUserId);
        Task<UserResponse> UpdateUserAsync(int userId, UserRequest updatedUser);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFollowRepository _followRepository;

        public UserService(IUserRepository userRepository, IFollowRepository followRepository)
        {
            _userRepository = userRepository;
            _followRepository = followRepository;
        }

        private static User MapUserRequestToUser(UserRequest userRequest)
        {
            return new User
            {
                UserName = userRequest.UserName,
                UserImage = new UserImage
                {
                    Image = Convert.FromBase64String(userRequest.UserImage.Image)
                }
            };
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
                UserImage = new UserUserImageResponse
                {
                    Image = Convert.ToBase64String(user.UserImage.Image),
                },
                Posts = user.Posts.Select(x => new UserPostResponse
                {
                    PostId = x.PostId,
                    Title = x.Title,
                    Desc = x.Desc,
                    PostLikes = new UserPostPostLikesResponse
                    {
                        Likes = x.PostLikes.Likes
                    },
                    Date = x.Date
                }).ToList(),
                Follow = user.Follow.Select(x => new UserFollowResponse
                {
                    UserId = x.UserId,
                    FollowingId = x.FollowingId,
                }).ToList()
            };
        }

        private static UserResponse MapUserToUserResponse(User user, Follow follow)
        {
            return new UserResponse
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Created = user.Created,
                FollowUserId = follow?.FollowingId,
                Login = new UserLoginResponse
                {
                    LoginId = user.Login.LoginId,
                    Email = user.Login.Email,
                    Role = user.Login.Role
                },
                UserImage = new UserUserImageResponse
                {
                    Image = Convert.ToBase64String(user.UserImage.Image),
                },
                Posts = user.Posts.Select(x => new UserPostResponse
                {
                    PostId = x.PostId,
                    Title = x.Title,
                    Desc = x.Desc,
                    PostLikes = new UserPostPostLikesResponse
                    {
                        Likes = x.PostLikes.Likes
                    },
                    Date = x.Date
                }).ToList(),
                Follow = user.Follow.Select(x => new UserFollowResponse
                {
                    UserId = x.UserId,
                    FollowingId = x.FollowingId,
                }).ToList()
            };
        }

        public async Task<UserResponse> FindUserAsync(int userId, int followUserId)
        {
            var user = await _userRepository.FindUserByIdAsync(userId);

            if (user != null)
            {
                return MapUserToUserResponse(user, _followRepository.FindFollow(userId, followUserId).Result);
            }
            return null;
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            List<User> user = await _userRepository.GetAllUsersAsync();

            if (user == null)
            {
                throw new ArgumentNullException();
            }
            return user.Select(user => MapUserToUserResponse(user)).ToList();
        }

        public async Task<UserResponse> UpdateUserAsync(int userId, UserRequest updatedUser)
        {
            var user = await _userRepository.UpdateUserAsync(userId, MapUserRequestToUser(updatedUser));

            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return MapUserToUserResponse(user);
        }
    }
}
