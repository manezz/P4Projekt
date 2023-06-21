using WebApi.SharedKernel.DTOs;

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
            List<UserResponse> users = new()
            {
                new UserResponse()
                {
                    UserId = 1,
                    UserName = "Tester 1",
                    FollowUserId = 1,
                    Login = new(),
                    UserImage = new()
                },
                new UserResponse()
                {
                    UserId = 2,
                    UserName = "Tester 2",
                    FollowUserId = 2,
                    Login = new(),
                    UserImage = new()
                }
            };

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

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode200_WhenUserExist()
        {
            // Arrange
            int userId = 1;

            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };
            UserResponse user = new()
            {
                UserId = userId,
                UserName = "Tester 1",
                FollowUserId = 1,
                Login = new(),
                UserImage = new()
            };

            _userServiceMock
                .Setup(x => x.FindByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(user);

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _userController.FindByIdAsync(userId);

            // Asset
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode404_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;

            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            _userServiceMock
                .Setup(x => x.FindByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(() => null);

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _userController.FindByIdAsync(userId);

            // Asset
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int userId = 1;

            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            _userServiceMock
                .Setup(x => x.FindByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _userController.FindByIdAsync(userId);

            // Asset
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void UpdateAsync_ShouldReturnStatusCode200_WhenUserIsUpdated()
        {
            // Arrange
            int userId = 1;

            UserRequest updateUser = new()
            {
                UserName = "Tester 1",
                UserImage = new()
            };
            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };
            UserResponse userResponse = new()
            {
                UserId = userId,
                UserName = "Tester 1",
                FollowUserId = 1,
                Login = new(),
                UserImage = new()
            };

            _userServiceMock
                .Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(userResponse);

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _userController.UpdateAsync(userId, updateUser);

            // Asset
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void UpdateAsync_ShouldReturnStatusCode404_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;

            UserRequest user = new()
            {
                UserName = "Tester 1",
                UserImage = new()
            };

            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            _userServiceMock
                .Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(() => null);

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _userController.UpdateAsync(userId, new UserRequest());

            // Asset
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void UpdateAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int userId = 1;

            UserRequest updateUser = new()
            {
                UserName = "Tester 1",
                UserImage = new()
            };
            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            _userServiceMock
                .Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _userController.UpdateAsync(userId, updateUser);

            // Asset
            Assert.Equal(500, result.StatusCode);
        }
    }
}
