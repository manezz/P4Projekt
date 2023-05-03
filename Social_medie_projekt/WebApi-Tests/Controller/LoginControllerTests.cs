using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApi_Tests.Controller
{
    public class LoginControllerTests
    {
        private readonly LoginController _loginController;
        private readonly Mock<ILoginService> _loginServiceMock = new();
        private readonly HttpContext httpContext = new DefaultHttpContext();

        public LoginControllerTests()
        {
            _loginController = new(_loginServiceMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnStatusCode200_WhenLoginsExists()
        {
            // Arrange
            List<LoginResponse> logins = new();

            logins.Add(new LoginResponse()
            {
                LoginId = 1,
                Email = "Test1@mail.dk",
                Role = 0
            });

            logins.Add(new LoginResponse()
            {
                LoginId = 2,
                Email = "Test2@mail.dk",
                Role = (Role)1
            });

            _loginServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(logins);

            // Act
            var result = (IStatusCodeActionResult)await _loginController.GetAllAsync();

            // Asset
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnStatusCode204_WhenNoLoginsExist()
        {
            // Arrange
            List<LoginResponse> logins = new();

            _loginServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(logins);

            // Act
            var result = (IStatusCodeActionResult)await _loginController.GetAllAsync();

            // Asset
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            List<LoginResponse> logins = new();

            _loginServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _loginController.GetAllAsync();

            // Asset
            Assert.Equal(500, result.StatusCode);

        }

        [Fact]
        public async void CreateAsync_ShouldReturnStatusCode200_WhenLoginIsSuccessfullyCreated()
        {
            // Arrange
            LoginRequest newLogin = new()
            {
                Email = "Test1@mail.dk",
                Password = "password",
                Role = 0
            };

            int loginId = 1;

            LoginResponse loginResponse = new()
            {
                LoginId = loginId,
                Email = "Test1@mail.dk",
                Role = 0
            };

            _loginServiceMock
                .Setup(x => x.CreateAsync(It.IsAny<LoginRequest>()))
                .ReturnsAsync(loginResponse);

            // Act
            var result = (IStatusCodeActionResult)await _loginController.CreateAsync(newLogin);

            // Asset
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnStatusCode500_WhenExceptionIsRasied()
        {
            // Arrange
            LoginRequest newLogin = new()
            {
                Email = "Test1@mail.dk",
                Password = "password",
                Role = 0
            };

            _loginServiceMock
                .Setup(x => x.CreateAsync(It.IsAny<LoginRequest>()))
                .ReturnsAsync(() => throw new Exception("This is an exeption"));

            // Act
            var result = (IStatusCodeActionResult)await _loginController.CreateAsync(newLogin);

            // Asset
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnStatusCode200_WhenLoginExists()
        {
            // Arrange
            int loginId = 1;

            LoginResponse login = new()
            {
                LoginId = loginId,
                Email = "Test1@mail.dk",
                Role = 0
            };

            httpContext.Items["Login"] = login;

            _loginServiceMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(login);

            // Act
            var result = (IStatusCodeActionResult)await _loginController.GetByIdAsync(loginId);

            // Asset
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnStatusCode404_WhenLoginDoesNotExist()
        {
            // Arrange
            int loginId = 1;

            LoginResponse login = new()
            {
                LoginId = loginId,
                Email = "Test1@mail.dk",
                Role = 0
            };

            httpContext.Items["Login"] = login;

            _loginServiceMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null!);

            // Act
            var result = (IStatusCodeActionResult)await _loginController.GetByIdAsync(loginId);

            // Asset
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int loginId = 1;

            LoginResponse login = new()
            {
                LoginId = loginId,
                Email = "Test1@mail.dk",
                Role = 0
            };

            httpContext.Items["Login"] = login;

            _loginServiceMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            // Act
            var result = (IStatusCodeActionResult)await _loginController.GetByIdAsync(loginId);

            // Asset
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void AuthenticateAsync_ShouldReturnStatusCode200_WhenLoginExists()
        {
            // Arrange
            int signInId = 1;

            SignInRequest newSignInRequest = new()
            {
                Email = "Test1@mail.dk",
                Password = "password",
            };

            SignInResponse signIn = new()
            {
                LoginId = signInId,
                Email = "Test1@mail.dk",
                Role = 0,
            };

            _loginServiceMock
                .Setup(x => x.AuthenticateAsync(It.IsAny<SignInRequest>()))
                .ReturnsAsync(signIn);

            // Act
            var result = (IStatusCodeActionResult)await _loginController.AuthenticateAsync(newSignInRequest);

            // Asset
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void AuthenticateAsync_ShouldReturnStatusCode404_WhenLoginDoesNotExist()
        {
            // Arrange
            SignInRequest newSignInRequest = new()
            {
                Email = "Test1@mail.dk",
                Password = "password",
            };

            _loginServiceMock
                .Setup(x => x.AuthenticateAsync(It.IsAny<SignInRequest>()))
                .ReturnsAsync(() => null!);

            // Act
            var result = (IStatusCodeActionResult)await _loginController.AuthenticateAsync(newSignInRequest);

            // Asset
            Assert.Equal(404, result.StatusCode);
        }
    }
}
