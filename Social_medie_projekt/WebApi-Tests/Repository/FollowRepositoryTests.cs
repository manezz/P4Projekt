using WebApi.Database.Entities;

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

        //[Fact]
        //public async void FindAllByUserIdAsync_ShouldReturnListOfFollows_WhenFollowsExists()
        //{
        //    // Arange
        //    await _context.Database.EnsureDeletedAsync();

        //    int userId = 1;

        //    User user = new()
        //    {
        //        UserId = userId,
        //    };
        //    _context.User.Add(user);

        //    _context.Follow.AddRange(
        //        new Follow
        //        {
        //            User = user,
        //            Following = new()
        //            {
        //                UserId = 2
        //            }
        //        },
        //        new Follow
        //        {
        //            User = user,
        //            Following = new()
        //            {
        //                UserId = 3
        //            }
        //        });
        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _followRepository.FindAllByPostIdAsync(post);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<List<PostTag>>(result);
        //    Assert.Equal(2, result.Count);
        //}
    }
}
