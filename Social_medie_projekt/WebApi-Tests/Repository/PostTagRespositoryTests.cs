namespace WebApi_Tests.Repository
{
    public class PostTagRespositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        private readonly PostTagRepository _postTagRepository;

        public PostTagRespositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "PostTagRepositoryTests")
                .Options;

            _context = new(_options);

            _postTagRepository = new PostTagRepository(_context);
        }

        [Fact]
        public async void FindAllByPostIdAsync_ShouldReturnListOfPostTags_WhenPostTagsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;

            Post post = new()
            {
                PostId = postId
            };
            _context.Post.Add(post);

            _context.PostTag.AddRange(
                new PostTag
                {
                    Post = post,
                    Tag = new()
                    {
                        TagId = 1
                    }
                },
                new PostTag
                {
                    Post = post,
                    Tag = new()
                    {
                        TagId = 2
                    }
                });
            await _context.SaveChangesAsync();

            // Act
            var result = await _postTagRepository.FindAllByPostIdAsync(postId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<PostTag>>(result);
            Assert.Equal(2, result.Count);
        }
    }
}
