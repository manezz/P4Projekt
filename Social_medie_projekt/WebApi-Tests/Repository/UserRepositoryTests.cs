namespace WebApi_Tests.Repository
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "UserRepositoryTests")
                .Options;

            _context = new(_options);

            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfUsers_WhereUsersExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            _context.User.Add(
                new User
                {
                    UserId = 1,
                    LoginId = 1,
                    UserName = "tester 1",
                });
            _context.User.Add(
                new User
                {
                    UserId = 2,
                    LoginId = 2,
                    UserName = "222test222",
                });
            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyListOfUsers_WhereNoUsersExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _userRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void CreateAsync_ShouldAddNewIdToUser_WhenSavingToDatabase()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            User user = new()
            {
                LoginId = 1,
                UserName = "tester 1",
                Login = new()
            };

            // Act
            var result = await _userRepository.CreateAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(expectedNewId, result.UserId);
        }

        [Fact]
        public async void CreateAsync_ShouldFailToAddNewUser_WhenUserIdAlreadyExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            User user = new()
            {
                LoginId = 1,
                UserName = "tester 1",
                Login = new()
            };

            var result = await _userRepository.CreateAsync(user);

            // Act
            async Task action() => await _userRepository.CreateAsync(user);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            _context.User.Add(new()
            {
                UserId = userId,
                LoginId = 1,
                UserName = "tester 1",
                Login = new()
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result?.UserId);
        }

        [Fact]
        public async void GeByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _userRepository.GetByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldChangeValuesOnUsers_WhenUsersExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            User newUser = new()
            {
                UserId = userId,
                LoginId = 1,
                UserName = "tester 1",
                Login = new(),
                UserImage = new()
                {
                    Image = Array.Empty<byte>()
                }
            };
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            User updateUser = new()
            {
                UserId = userId,
                LoginId = 1,
                UserName = "tester 11",
                UserImage = new()
                {
                    // change to value
                    Image = Array.Empty<byte>(),
                }
            };

            // Act
            var result = await _userRepository.UpdateByIdAsync(userId, updateUser);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result?.UserId);
            Assert.Equal(updateUser.LoginId, result?.LoginId);
            Assert.Equal(updateUser.UserName, result?.UserName);
            Assert.Equal(updateUser.UserImage.Image, result?.UserImage.Image);
        }

        [Fact]
        public async void UpdateByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            User updateUser = new()
            {
                UserId = userId,
                LoginId = 1,
                UserName = "tester 1",
            };

            // Act
            var result = await _userRepository.UpdateByIdAsync(userId, updateUser);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnDeletedUser_WhenUserIsDeleted()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            User newUser = new()
            {
                UserId = userId,
                LoginId = 1,
                UserName = "tester 1",
            };
            _context.User.Add(newUser);

            await _context.SaveChangesAsync();

            // Act
            var result = await _userRepository.DeleteByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<User>(result);
            Assert.Equal(userId, result?.UserId);
        }

        [Fact]
        public async void DeleteByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _userRepository.DeleteByIdAsync(1);

            // Assert
            Assert.Null(result);
        }
    }
}
