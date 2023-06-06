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

        [Fact]
        public async void FindAllByPostIdAsync_ShouldReturnEmptyListOfPostTags_WhenNoPostTagsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;

            // Act
            var result = await _postTagRepository.FindAllByPostIdAsync(postId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<PostTag>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnPostTag_WhenPostTagExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;
            int tagId = 1;

            _context.PostTag.Add(new()
            {
                PostId = postId,
                TagId = tagId
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _postTagRepository.FindByIdAsync(postId, tagId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PostTag>(result);
            Assert.Equal(postId, result?.PostId);
            Assert.Equal(tagId, result?.TagId);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnNull_WhenPostTagDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            var postId = 1;
            var tagId = 1;

            // Act
            var result = await _postTagRepository.FindByIdAsync(postId, tagId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateAsync_ShouldFailToAddNewPostTag_WhenPostTagIdAlreadyExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            PostTag postTag = new()
            {
                PostId = 1,
                TagId = 1
            };

            var result = await _postTagRepository.CreateAsync(postTag);

            // Act
            async Task action() => await _postTagRepository.CreateAsync(postTag);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void DeleteAsync_ShouldReturnDeletedPostTag_WhenPostTagIsDeleted()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;
            int tagId = 1;

            PostTag postTag = new()
            {
                PostId = postId,
                TagId = tagId
            };
            _context.PostTag.Add(postTag);

            await _context.SaveChangesAsync();

            // Act
            var result = await _postTagRepository.DeleteAsync(postId, tagId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PostTag>(result);
            Assert.Equal(postId, result?.PostId);
            Assert.Equal(tagId, result?.TagId);
        }

        [Fact]
        public async void DeleteAsync_ShouldReturnNull_WhenPostTagDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;
            int tagId = 1;

            // Act
            var result = await _postTagRepository.DeleteAsync(postId, tagId);

            // Assert
            Assert.Null(result);
        }
    }
}
