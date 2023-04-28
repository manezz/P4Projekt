namespace WebApi_Tests.Service
{
    public class LoginServiceTests
    {
        private readonly LoginService _loginService;
        private readonly Mock<ILoginRepository> _loginRepositoryMock = new();
        private readonly Mock<IJwtUtils> _jwtUtils = new();

        public LoginServiceTests()
        {
            _loginService = new(_loginRepositoryMock.Object, _jwtUtils.Object);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfLoginResponses_WhenLoginsExists()
        {
            // Arrange
            List<Login> logins = new()
            {
                new()
                {
                    LoginId = 1,
                    Email = "Test1@mail.dk",
                    Password = "password",
                    Role = 0,
                    User = new()
                    {
                        UserImage = new()
                    }
                },
                new()
                {
                    LoginId = 2,
                    Email = "Test2@mail.dk",
                    Password = "password",
                    Role = (Role)1,
                    User = new()
                    {
                        UserImage = new()
                    }
                },
            };

            _loginRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(logins);

            // Act
            var result = await _loginService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<LoginResponse>>(result);
            Assert.Equal(2, result?.Count);
        }
    }
}
