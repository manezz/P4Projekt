namespace WebApi_Tests.Repository
{
    public class PostRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        private readonly PostRepository _postRepository;

        public PostRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "PostRepositoryTests")
                .Options;

            _context = new(_options);

            _postRepository = new PostRepository(_context);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfPosts_WherePostsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            _context.Post.Add(
                new Post
                {
                    PostId = 1,
                    PostLikes = new(),
                    User = new()
                });
            _context.Post.Add(
                new Post
                {
                    PostId = 2,
                    PostLikes = new(),
                    User = new()
                });
            await _context.SaveChangesAsync();

            // Act
            var result = await _postRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Post>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyListOfPosts_WhereNoPostsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _postRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Post>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void CreateAsync_ShouldAddNewIdToPost_WhenSavingToDatabase()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Post post = new()
            {
                PostLikes = new(),
                User = new()
            };

            // Act
            var result = await _postRepository.CreateAsync(post);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Post>(result);
            Assert.Equal(expectedNewId, result.PostId);
        }

        [Fact]
        public async void CreateAsync_ShouldFailToAddNewPost_WhenPostIdAlreadyExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            Post post = new()
            {
                PostId = 1,
                PostLikes = new(),
                User = new()
            };

            var result = await _postRepository.CreateAsync(post);

            // Act
            async Task action() => await _postRepository.CreateAsync(post);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnPost_WherePostExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;

            _context.Post.Add(new()
            {
                PostId = postId,
                PostLikes = new(),
                User = new()
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _postRepository.FindByIdAsync(postId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Post>(result);
            Assert.Equal(postId, result.PostId);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnNull_WhenPostDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;

            // Act
            var result = await _postRepository.FindByIdAsync(postId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void FindAllByUserIdAsync_ShouldReturnListOfPosts_WhenPostsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            User user = new()
            {
                UserId = userId,
            };
            _context.Post.Add(
                new Post
                {
                    PostId = 1,
                    PostLikes = new(),
                    User = user
                });
            _context.Post.Add(
                new Post
                {
                    PostId = 2,
                    PostLikes = new(),
                    User = user
                });
            await _context.SaveChangesAsync();

            // Act
            var result = await _postRepository.FindAllByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Post>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void FindAllByUserIdAsync_ShouldReturnEmptyListOfPosts_WhenNoPostsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            // Act
            var result = await _postRepository.FindAllByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Post>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void UpdateAsync_ShouldChangeValuesOnPosts_WhenPostsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;

            Post newPost = new()
            {
                PostId = postId,
                Title = "Title 1",
                Desc = "Desc 1",
                PostLikes = new(),
                User = new()
            };
            _context.Post.Add(newPost);
            await _context.SaveChangesAsync();

            Post updatePost = new()
            {
                PostId = postId,
                Title = "New Title 1",
                Desc = "New Desc 1",
            };

            // Act
            var result = await _postRepository.UpdateAsync(postId, updatePost);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Post>(result);
            Assert.Equal(postId, result?.PostId);
            Assert.Equal(updatePost.Title, result?.Title);
            Assert.Equal(updatePost.Desc, result?.Desc);
        }

        [Fact]
        public async void UpdateAsync_ShouldReturnNull_WhenPostDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;

            Post updatePost = new()
            {
                PostId = postId,
                Title = "New Title 1",
                Desc = "New Desc 1",
            };

            // Act
            var result = await _postRepository.UpdateAsync(postId, updatePost);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteAsync_ShouldReturnDeletedPost_WhenPostIsDeleted()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;

            Post post = new()
            {
                PostId = postId,
                PostLikes = new(),
                User = new()
            };
            _context.Post.Add(post);

            await _context.SaveChangesAsync();

            // Act
            var result = await _postRepository.DeleteAsync(postId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Post>(result);
            Assert.Equal(postId, result?.PostId);
        }

        [Fact]
        public async void DeleteAsync_ShouldReturnNull_WhenPostDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;

            // Act
            var result = await _postRepository.DeleteAsync(postId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdatePostLikesAsync_ShouldChangeValuesOnPost_WhenPostExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;
            int oldLikes = 2;
            int like = 1;

            Post post = new()
            {
                PostId = postId,
                PostLikes = new()
                {
                    Likes = oldLikes
                },
                User = new()
            };
            _context.Post.Add(post);

            await _context.SaveChangesAsync();

            // Act
            var result = await _postRepository.UpdatePostLikesAsync(postId, like);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Post>(result);
            Assert.Equal(postId, result?.PostId);
            Assert.Equal(oldLikes + like, result?.PostLikes.Likes);
        }
    }
}
