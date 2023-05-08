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
    }
}
