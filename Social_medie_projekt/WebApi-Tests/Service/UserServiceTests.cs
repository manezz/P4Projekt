namespace WebApi_Tests.Service
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IFollowRepository> _followRepositoryMock = new();

        public UserServiceTests()
        {
            _userService = new(_userRepositoryMock.Object, _followRepositoryMock.Object);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfUserResponses_WhenUsersExists()
        {
            // Arrange
            List<User> users = new()
            {
                new()
                {
                    UserId = 1,
                    LoginId = 1,
                    UserName = "Tester 1",
                    UserImage = new(),
                    Login = new()

                },
                new()
                {
                    UserId = 1,
                    LoginId = 1,
                    UserName = "Tester 1",
                    UserImage = new(),
                    Login = new()
                },
            };

            _userRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserResponse>>(result);
            Assert.Equal(2, result?.Count);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyListOfUserResponses_WhenNoUsersExists()
        {
            // Arrange
            List<User> users = new();

            _userRepositoryMock
            .Setup(x => x.GetAllAsync())
                .ReturnsAsync(users);

            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetAllAsync_ShouldThrowNullExeption_WhenRepositioryReturnsNull()
        {
            // Arrange
            List<User> users = new();

            _userRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => throw new ArgumentNullException());

            // Act
            async Task action() => await _userService.GetAllAsync();

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(action);
            Assert.Contains("Value cannot be null", ex.Message);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnUserResponse_WhenUserExists()
        {
            // Arrange
            int userId = 1;
            int followUserId = 1;

            User user = new()
            {
                UserId = userId,
                UserName = "Tester 1",
                UserImage = new(),
                Login = new()
            };

            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.GetByIdAsync(userId, followUserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(user.UserId, result.UserId);
            Assert.Equal(user.UserName, result?.UserName);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnNull_WhenUserDoesNotExists()
        {
            // Arrange
            int userId = 1;
            int followUserId = 1;

            _userRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(() => null);

            // Act
            var result = await _userService.GetByIdAsync(userId, followUserId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnUserResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            UserRequest userRequest = new()
            {
                UserName = "Tester 1",
                UserImage = new()
                {
                    Image = ""
                }
            };
            int userId = 1;

            User user = new()
            {
                UserId = userId,
                UserName = "Tester 1",
                UserImage = new(),
                Login = new()
            };

            _userRepositoryMock
                .Setup(x => x.UpdateByIdAsync(It.IsAny<int>(), It.IsAny<User>()))
                .ReturnsAsync(user);

            // Act
            var result = await _userService.UpdateByIdAsync(userId, userRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            Assert.Equal(userId, result?.UserId);
            Assert.Equal(userRequest.UserName, result?.UserName);
            Assert.Equal(userRequest.UserImage.Image, result?.UserImage.Image);
        }
    }
}
