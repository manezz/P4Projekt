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
    }
}
