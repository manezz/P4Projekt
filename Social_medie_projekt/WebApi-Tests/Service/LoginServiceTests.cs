namespace WebApi_Tests.Service
{
    public class LoginServiceTests
    {
        private readonly LoginService _loginService;
        private readonly Mock<ILoginRepository> _loginRepositoryMock = new();
        private readonly Mock<IJwtUtils> _jwtUtilsMock = new();

        public LoginServiceTests()
        {
            _loginService = new(
                _loginRepositoryMock.Object,
                _jwtUtilsMock.Object);
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

        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyListOfLoginResponses_WhenNoLoginsExists()
        {
            // Arrange
            List<Login> logins = new();

            _loginRepositoryMock
            .Setup(x => x.GetAllAsync())
                .ReturnsAsync(logins);

            // Act
            var result = await _loginService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<LoginResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetAllAsync_ShouldThrowNullExeption_WhenRepositoryReturnsNull()
        {
            // Arrange
            List<Login> logins = null!;

            _loginRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(logins);

            // Act
            async Task action() => await _loginService.GetAllAsync();

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(action);
            Assert.Contains("Value cannot be null", ex.Message);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnLoginResponse_WhenCreateIsSuccess()
        {
            // Arrange
            LoginRequest newLogin = new()
            {
                Email = "Test1@mail.dk",
                Password = "password",
                Role = 0,
                User = new()
                {
                    UserImage = new()
                }
            };
            int loginId = 1;

            Login login = new()
            {
                LoginId = loginId,
                Email = "Test1@mail.dk",
                Password = "password",
                Role = 0,
                User = new()
                {
                    UserImage = new()
                }
            };

            _loginRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Login>()))
                .ReturnsAsync(login);

            // Act
            var result = await _loginService.CreateAsync(newLogin);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LoginResponse>(result);
            Assert.Equal(login.LoginId, result?.LoginId);
            Assert.Equal(login.Email, result?.Email);
            Assert.Equal(login.Role, result?.Role);
        }

        [Fact]
        public async void CreateAsync_ShouldThrowNullExeption_WhenRepositioryReturnsNull()
        {
            // Arrange
            LoginRequest newLogin = new()
            {
                Email = "Test1@mail.dk",
                Password = "password",
                Role = 0,
                User = new()
                {
                    UserImage = new()
                }
            };

            _loginRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Login>()))
                .ReturnsAsync(() => null!);

            // Act
            async Task action() => await _loginService.CreateAsync(newLogin);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(action);
            Assert.Contains("Value cannot be null", ex.Message);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnLoginResponse_WhenLoginExists()
        {
            // Arrange
            int loginId = 1;

            Login login = new()
            {
                LoginId = loginId,
                Email = "Test1@mail.dk",
                Password = "password",
                Role = 0,
                User = new()
                {
                    UserImage = new()
                }
            };

            _loginRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(login);

            // Act
            var result = await _loginService.GetByIdAsync(loginId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LoginResponse>(result);
            Assert.Equal(login.LoginId, result?.LoginId);
            Assert.Equal(login.Email, result?.Email);
            Assert.Equal(login.Role, result?.Role);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnNull_WhenLoginDoesNotExist()
        {
            // Arrange
            int loginId = 1;

            _loginRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(() => null);

            // Act
            var result = await _loginService.GetByIdAsync(loginId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void AuthenticateAsync_ShouldReturnSignInResponse_WhenLoginExists()
        {
            // Arrange
            SignInRequest signInRequest = new()
            {
                Email = "Test1@mail.dk",
                Password = "password",
            };

            Login login = new()
            {
                LoginId = 1,
                Email = "Test1@mail.dk",
                Password = "password",
                Role = 0,
                User = new()
                {
                    UserImage = new()
                }
            };

            _loginRepositoryMock
                .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(login);

            // Act
            var result = await _loginService.AuthenticateAsync(signInRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<SignInResponse>(result);
            Assert.Equal(login.LoginId, result?.LoginId);
            Assert.Equal(login.Email, result?.Email);
            Assert.Equal(login.Role, result?.Role);
        }

        [Fact]
        public async void AuthenticateAsync_ShouldReturnNull_WhenLoginDoesNotExists()
        {
            // Arrange
            SignInRequest signInRequest = new()
            {
                Email = "Test1@mail.dk",
                Password = "password",
            };

            _loginRepositoryMock
                .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                    .ReturnsAsync(() => null!);

            // Act
            var result = await _loginService.AuthenticateAsync(signInRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnLoginResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            LoginRequest loginRequest = new()
            {
                Email = "Test1@mail.dk",
                Password = "password",
                User = new()
                {
                    UserImage = new()
                }
            };
            int loginId = 1;

            Login login = new()
            {
                LoginId = loginId,
                Email = "Test1@mail.dk",
                Password = "password",
                Role = 0,
                User = new()
                {
                    UserImage = new()
                }
            };

            _loginRepositoryMock
                .Setup(x => x.UpdateByIdAsync(It.IsAny<int>(), It.IsAny<Login>()))
                .ReturnsAsync(login);

            // Act
            var result = await _loginService.UpdateByIdAsync(loginId, loginRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LoginResponse>(result);
            Assert.Equal(loginId, result?.LoginId);
            Assert.Equal(loginRequest.Email, result?.Email);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnNull_WhenLoginDoesNotExists()
        {
            // Arrange
            LoginRequest loginRequest = new()
            {
                Email = "Test1@mail.dk",
                Password = "password",
                User = new()
                {
                    UserImage = new()
                }
            };
            int loginId = 1;

            _loginRepositoryMock
                .Setup(x => x.UpdateByIdAsync(It.IsAny<int>(), It.IsAny<Login>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _loginService.UpdateByIdAsync(loginId, loginRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnLoginResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int loginId = 1;

            Login login = new()
            {
                LoginId = loginId,
                Email = "Test1@mail.dk",
                Password = "password",
                Role = 0,
                User = new()
                {
                    UserImage = new()
                }
            };

            _loginRepositoryMock
                .Setup(x => x.DeleteByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(login);

            // Act
            var result = await _loginService.DeleteByIdAsync(loginId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<LoginResponse>(result);
            Assert.Equal(login.LoginId, result?.LoginId);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnNull_WhenLoginDoesNotExists()
        {
            // Arrange
            int loginId = 1;

            _loginRepositoryMock
                .Setup(x => x.DeleteByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _loginService.DeleteByIdAsync(loginId);

            // Assert
            Assert.Null(result);
        }
    }
}
