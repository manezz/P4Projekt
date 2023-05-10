﻿namespace WebApi_Tests.Repository
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
        public async void GetByIdAsync_ShouldReturnPost_WherePostExists()
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
            var result = await _postRepository.GetByIdAsync(postId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Post>(result);
            Assert.Equal(postId, result.PostId);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnNull_WhenPostDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int postId = 1;

            // Act
            var result = await _postRepository.GetByIdAsync(postId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetAllByUserIdAsync_ShouldReturnListOfPosts_WhenPostsExists()
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
            var result = await _postRepository.GetAllByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Post>>(result);
            Assert.Equal(2, result.Count);
        }
    }
}
