namespace WebApi_Tests.Service
{
    public class PostServiceTests
    {
        private readonly PostService _postService;
        private readonly Mock<IPostRepository> _postRepositoryMock = new();
        private readonly Mock<ITagRepository> _tagRepositoryMock = new();
        private readonly Mock<ITagService> _tagServiceMock = new();
        private readonly Mock<ILikeRepository> _likeRepositoryMock = new();
        private readonly Mock<IPostTagService> _postTagServiceMock = new();

        public PostServiceTests()
        {
            _postService = new(
                _postRepositoryMock.Object,
                _tagRepositoryMock.Object,
                _tagServiceMock.Object,
                _likeRepositoryMock.Object,
                _postTagServiceMock.Object);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfPostResponses_WhenPostsExists()
        {
            // Arrange
            int likeUserId = 1;

            List<Post> posts = new()
            {
                new()
                {
                    PostId = 1,
                    Title = "Title1",
                    Desc = "Desc1",
                    PostLikes = new(),
                    User = new()
                    {
                        UserImage = new()
                    }
                },
                new()
                {
                    PostId = 2,
                    Title = "Title2",
                    Desc = "Desc2",
                    PostLikes = new(),
                    User = new()
                    {
                        UserImage = new()
                    }
                }
            };

            _postRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(posts);

            // Act
            var result = await _postService.GetAllAsync(likeUserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<PostResponse>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyListOfPostResponses_WhenNoPostsExists()
        {
            // Arrange
            int likeUserId = 1;

            List<Post> posts = new();

            _postRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(posts);

            // Act
            var result = await _postService.GetAllAsync(likeUserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<PostResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetAllAsync_ShouldThrowNullExeption_WhenRepositoryReturnsNull()
        {
            // Arrange
            List<Post> posts = null!;

            int likeUserId = 1;

            _postRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(posts);

            // Act
            async Task action() => await _postService.GetAllAsync(likeUserId);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(action);
            Assert.Contains("Value cannot be null", ex.Message);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnPostResponse_WhenPostCreateIsSuccess()
        {
            // Arrange
            PostRequest newPost = new()
            {
                Title = "Title1",
                Desc = "Desc1",
            };
            int postId = 1;

            Post post = new()
            {
                PostId = postId,
                Title = "Title1",
                Desc = "Desc1",
                PostLikes = new(),
                User = new()
                {
                    UserImage = new()
                }
            };

            _postRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Post>()))
                .ReturnsAsync(post);

            // Act
            var result = await _postService.CreateAsync(newPost);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PostResponse>(result);
            Assert.Equal(post.PostId, result?.PostId);
            Assert.Equal(post.Title, result?.Title);
            Assert.Equal(post.Desc, result?.Desc);
        }

        [Fact]
        public async void CreateAsync_ShouldThrowNullExeption_WhenRepositoryReturnsNull()
        {
            // Arrange
            PostRequest newPost = new()
            {
                Title = "Title1",
                Desc = "Desc1",
            };

            _postRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Post>()))
                .ReturnsAsync(() => null!);

            // Act
            async Task action() => await _postService.CreateAsync(newPost);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(action);
            Assert.Contains("Value cannot be null", ex.Message);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnPostResponse_WhenPostExists()
        {
            // Arrange
            int postId = 1;
            int likeUserId = 1;

            Post post = new()
            {
                PostId = postId,
                Title = "Title1",
                Desc = "Desc1",
                PostLikes = new(),
                User = new()
                {
                    UserImage = new()
                }
            };

            _postRepositoryMock
                .Setup(x => x.GetByIdAsync(postId))
                .ReturnsAsync(post);

            // Act
            var result = await _postService.GetByIdAsync(postId, likeUserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PostResponse>(result);
            Assert.Equal(post.PostId, result?.PostId);
            Assert.Equal(post.Title, result?.Title);
            Assert.Equal(post.Desc, result?.Desc);
        }

        [Fact]
        public async void GetByIdAsync_ShouldReturnNull_WhenPostDoesNotExist()
        {
            // Arrange
            int postId = 1;
            int likeUserId = 1;

            _postRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(() => null!);

            // Act
            var result = await _postService.GetByIdAsync(postId, likeUserId);

            // Assert
            Assert.Null(result);
        }

        // Creat Tests for UpdateByIdAsync
        [Fact]
        public async void UpdateByIdAsync_ShouldReturnPostResponse_WhenPostUpdateIsSuccess()
        {
            // Arrange
            PostUpdateRequest postUpdateRequest = new()
            {
                Title = "Title2",
                Desc = "Desc2"
            };
            int postId = 1;

            Post post = new()
            {
                PostId = postId,
                Title = "Title1",
                Desc = "Desc1",
                PostLikes = new(),
                User = new()
                {
                    UserImage = new()
                }
            };

            _postRepositoryMock
                .Setup(x => x.UpdateByIdAsync(It.IsAny<int>(), It.IsAny<Post>()))
                .ReturnsAsync(post);

            // Act
            var result = await _postService.UpdateByIdAsync(postId, postUpdateRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PostResponse>(result);
            Assert.Equal(postId, result?.PostId);
            Assert.Equal(postUpdateRequest.Title, result?.Title);
            Assert.Equal(postUpdateRequest.Desc, result?.Desc);
        }
    }
}
