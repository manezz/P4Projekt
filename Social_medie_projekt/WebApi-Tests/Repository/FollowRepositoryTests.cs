namespace WebApi_Tests.Repository
{
    public class FollowRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        private readonly FollowRepository _followRepository;

        public FollowRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "FollowRepositoryTests")
                .Options;

            _context = new(_options);

            _followRepository = new FollowRepository(_context);
        }

        [Fact]
        public async void CreateAsync_ShouldFailToAddNewFollow_WhenFollowIdAlreadyExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            Follow follow = new()
            {
                UserId = 1,
                FollowingUserId = 1
            };

            var result = await _followRepository.CreateAsync(follow);

            // Act
            async Task action() => await _followRepository.CreateAsync(follow);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnFollow_WhereFollowExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;
            int followingUserId = 2;

            _context.Follow.Add(new()
            {
                UserId = userId,
                FollowingUserId = followingUserId
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _followRepository.FindByIdAsync(userId, followingUserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Follow>(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(followingUserId, result.FollowingUserId);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnNull_WhenFollowDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;
            int followingUserId = 2;

            // Act
            var result = await _followRepository.FindByIdAsync(userId, followingUserId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void FindAllByUserIdAsync_ShouldReturnListOfFollows_WhenFollowsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            User user = new()
            {
                UserId = userId,
            };
            _context.User.Add(user);

            _context.Follow.AddRange(
                new Follow
                {
                    User = user,
                    FollowingUser = new()
                    {
                        UserId = 2
                    }
                },
                new Follow
                {
                    User = user,
                    FollowingUser = new()
                    {
                        UserId = 3
                    }
                });
            await _context.SaveChangesAsync();

            // Act
            var result = await _followRepository.FindAllByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Follow>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void FindAllByUserIdAsync_ShouldReturnEmptyListOfFollows_WhenNoFollowsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            // Act
            var result = await _followRepository.FindAllByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Follow>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void FindAllFollowingUserIdAsync_ShouldReturnListOfFollows_WhenFollowsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int followingUserId = 1;

            User followingUser = new()
            {
                UserId = followingUserId
            };
            _context.User.Add(followingUser);

            _context.Follow.AddRange(
                new Follow
                {
                    User = new User()
                    {
                        UserId = 2
                    },
                    FollowingUser = followingUser
                },
                new Follow
                {
                    User = new User()
                    {
                        UserId = 3
                    },
                    FollowingUser = followingUser
                });
            await _context.SaveChangesAsync();

            // Actt
            var result = await _followRepository.FindAllByFollowingUserIdAsync(followingUserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Follow>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void FindAllByFollowUserIdAsync_ShouldReturnEmptyListOfFollows_WhenNoFollowsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int followingUserId = 1;

            // Act
            var result = await _followRepository.FindAllByFollowingUserIdAsync(followingUserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Follow>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void DeleteAsync_ShouldReturnDeletedFollow_WhenFollowIsDeleted()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;
            int followingUserId = 2;

            Follow follow = new()
            {
                UserId = userId,
                FollowingUserId = followingUserId
            };
            _context.Follow.Add(follow);

            await _context.SaveChangesAsync();

            // Act
            var result = await _followRepository.DeleteAsync(userId, followingUserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Follow>(result);
            Assert.Equal(userId, result?.UserId);
            Assert.Equal(followingUserId, result?.FollowingUserId);
        }

        [Fact]
        public async void DeleteAsync_ShouldReturnNull_WhenFollowDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;
            int followingUserId = 2;

            // Act
            var result = await _followRepository.DeleteAsync(userId, followingUserId);

            // Assert
            Assert.Null(result);
        }
    }
}
