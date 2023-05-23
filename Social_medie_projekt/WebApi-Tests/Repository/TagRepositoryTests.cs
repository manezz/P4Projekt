namespace WebApi_Tests.Repository
{
    public class TagRepositoryTests
    {
        public readonly DbContextOptions<DatabaseContext> _options;
        public readonly DatabaseContext _context;
        public readonly TagRepository _tagRepository;

        public TagRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TagRepositoryTests")
                .Options;

            _context = new(_options);

            _tagRepository = new TagRepository(_context);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfTags_WhereTagsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            _context.Tag.Add(
                new Tag
                {
                    TagId = 1,
                    Name = "Test1",
                });
            _context.Tag.Add(
                new Tag
                {
                    TagId = 2,
                    Name = "Test2",
                });
            await _context.SaveChangesAsync();

            // Act
            var result = await _tagRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Tag>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyListOfTags_WhereNoTagsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _tagRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Tag>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void FindAllByPostIdAsync_ShouldReturnListOfTags_WhereTagsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;

            _context.Post.Add(
                new Post
                {
                    PostId = postId,
                    User = new()
                });

            _context.PostTag.Add(
                new PostTag
                {
                    PostId = postId,
                    TagId = 1
                });
            _context.PostTag.Add(
                new PostTag
                {
                    PostId = postId,
                    TagId = 2
                });

            _context.Tag.Add(
                new Tag
                {
                    TagId = 1,
                    Name = "Test1",
                });
            _context.Tag.Add(
                new Tag
                {
                    TagId = 2,
                    Name = "Test2",
                });
            await _context.SaveChangesAsync();

            // Act
            var result = await _tagRepository.FindAllByPostIdAsync(postId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Tag>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void FindAllByPostIdAsync_ShouldReturnEmptyListOfTags_WhereNoTagsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;

            // Act
            var result = await _tagRepository.FindAllByPostIdAsync(postId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Tag>>(result);
            Assert.Empty(result);
        }
    }
}
