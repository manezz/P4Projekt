namespace WebApi_Tests.Controller
{
    public class UserControllerTests
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _userServiceMock = new();
        private readonly HttpContext httpContext = new DefaultHttpContext();

        public UserControllerTests()
        {
            _userController = new(_userServiceMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnStatusCode200_WhenUsersExists()
        {
            // Arrange
            List<UserResponse> users = new();

            users.Add(new UserResponse()
            {
                UserId = 1,
                UserName = "Tester 1",
                FollowUserId = 1,
                Login = new(),
                UserImage = new()
            });

            users.Add(new UserResponse()
            {
                UserId = 2,
                UserName = "Tester 2",
                FollowUserId = 2,
                Login = new(),
                UserImage = new()
            });

            _userServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(users);

            // Act
            var result = (IStatusCodeActionResult)await _userController.GetAllAsync();

            // Asset
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnStatusCode204_WhenNoUsersExist()
        {
            // Arrange
            List<UserResponse> users = new();

            _userServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(users);

            // Act
            var result = (IStatusCodeActionResult)await _userController.GetAllAsync();

            // Asset
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            List<UserResponse> users = new();

            _userServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _userController.GetAllAsync();

            // Asset
            Assert.Equal(500, result.StatusCode);
        }
    }
}
