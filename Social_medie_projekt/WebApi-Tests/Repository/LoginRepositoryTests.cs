namespace WebApi_Tests.Repository
{
    public class LoginRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        private readonly LoginRepository _loginRepository;

        public LoginRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "LoginRepositoryTests")
                .Options;

            _context = new(_options);

            _loginRepository = new LoginRepository(_context);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfLogins_WhereLoginsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            _context.Login.Add(
                new Login
                {
                    LoginId = 1,
                    Email = "Test1@mail.dk",
                    Password = "password",
                    Role = 0
                });
            _context.Login.Add(
                new Login
                {
                    LoginId = 2,
                    Email = "Test2@mail.dk",
                    Password = "password",
                    Role = (Role)1
                });
            await _context.SaveChangesAsync();

            // Act
            var result = await _loginRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Login>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyListOfLogins_WhereNoLogingsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _loginRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Login>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void CreateAsync_ShouldAddNewIdToLogin_WhenSavingToDatabase()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Login login = new()
            {
                LoginId = 1,
                Email = "Test1@mail.dk",
                Password = "password",
                Role = 0
            };

            // Act
            var result = await _loginRepository.CreateAsync(login);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Login>(result);
            Assert.Equal(expectedNewId, result.LoginId);
        }

        [Fact]
        public async void CreateAsync_ShouldFailToAddNewLogin_WhenLoginIdAlreadyExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            Login login = new()
            {
                LoginId = 1,
                Email = "Test1@mail.dk",
                Password = "password",
                Role = 0
            };

            var result = await _loginRepository.CreateAsync(login);

            // Act
            async Task action() => await _loginRepository.CreateAsync(login);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnLogin_WhenLoginExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int loginId = 1;

            _context.Login.Add(new()
            {
                LoginId = 1,
                Email = "Test1@mail.dk",
                Password = "password",
                Role = 0
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _loginRepository.GetByIdAsync(loginId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Login>(result);
            Assert.Equal(loginId, result?.LoginId);
        }
    }
}
