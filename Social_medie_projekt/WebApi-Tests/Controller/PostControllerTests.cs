using WebApi.Database.Entities;

namespace WebApi_Tests.Controller
{
    public class PostControllerTests
    {
        private readonly PostController _postController;
        private readonly Mock<IPostService> _postServiceMock = new();
        private readonly HttpContext httpContext = new DefaultHttpContext();

        public PostControllerTests()
        {
            _postController = new(_postServiceMock.Object)
            {
                ControllerContext = new()
                {
                    HttpContext = httpContext
                }
            };
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnStatusCode200_WhenPostsExists()
        {
            // Arrange
            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            List<PostResponse> posts = new()
            {
                new PostResponse()
                {
                    PostId = 1,
                    Title = "Test 1",
                    Desc = "Test 1",
                    LikeUserId = 1,
                    PostLikes = new(),
                    User = new(),
                    Tags = new()
                },
                new PostResponse()
                {
                    PostId = 2,
                    Title = "Test 2",
                    Desc = "Test 2",
                    LikeUserId = 2,
                    PostLikes = new(),
                    User = new(),
                    Tags = new()
                }
            };

            _postServiceMock
                .Setup(x => x.GetAllAsync(It.IsAny<int>()))
                .ReturnsAsync(posts);

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _postController.GetAllAsync();
            // Asset
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnStatusCode204_WhenNoPostsExist()
        {
            // Arrange
            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            List<PostResponse> posts = new();

            _postServiceMock
                .Setup(x => x.GetAllAsync(It.IsAny<int>()))
                .ReturnsAsync(posts);

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _postController.GetAllAsync();

            // Asset
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            List<PostResponse> posts = new();

            _postServiceMock
                .Setup(x => x.GetAllAsync(It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _postController.GetAllAsync();

            // Asset
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void FindAllByUserIdAsync_ShouldReturnStatusCode200_WhenPostsExist()
        {
            // Arrange
            int userId = 1;

            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            List<PostResponse> posts = new()
            {
                new PostResponse()
                {
                    PostId = 1,
                    Title = "Test 1",
                    Desc = "Test 1",
                    LikeUserId = 1,
                    PostLikes = new(),
                    User = new(),
                    Tags = new()
                },
                new PostResponse()
                {
                    PostId = 2,
                    Title = "Test 2",
                    Desc = "Test 2",
                    LikeUserId = 2,
                    PostLikes = new(),
                    User = new(),
                    Tags = new()
                }
            };

            _postServiceMock
                .Setup(x => x.FindAllByUserIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(posts);

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _postController.FindAllByUserIdAsync(userId);

            // Asset
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void FindAllByUserIdAsyncShouldReturnStatusCode404_WhenPostsDoesNotExist()
        {
            // Arrange
            int userId = 1;

            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            _postServiceMock
                .Setup(x => x.FindAllByUserIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(() => null);

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _postController.FindAllByUserIdAsync(userId);

            // Asset
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode200_WhenPostExist()
        {
            // Arrange
            int postId = 1;

            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            PostResponse post = new()
            {
                PostId = 1,
                Title = "Test 1",
                Desc = "Test 1",
                LikeUserId = 1,
                PostLikes = new(),
                User = new(),
                Tags = new()
            };

            _postServiceMock
                .Setup(x => x.FindByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(post);

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _postController.FindByIdAsync(postId);

            // Asset
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode404_WhenPostDoesNotExist()
        {
            // Arrange
            int postId = 1;

            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            _postServiceMock
                .Setup(x => x.FindByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(() => null);

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _postController.FindByIdAsync(postId);

            // Asset
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int postId = 1;

            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            _postServiceMock
                .Setup(x => x.FindByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _postController.FindByIdAsync(postId);

            // Asset
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnStatusCode200_WhenPostIsSuccessfullyCreated()
        {
            // Arrange
            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            PostRequest postRequest = new()
            {
                Title = "Test 1",
                Desc = "Test 1",
                Tags = new()
            };

            PostResponse postResponse = new()
            {
                PostId = 1,
                Title = "Test 1",
                Desc = "Test 1",
                LikeUserId = 1,
                PostLikes = new(),
                User = new(),
                Tags = new()
            };

            _postServiceMock
                .Setup(x => x.CreateAsync(It.IsAny<PostRequest>()))
                .ReturnsAsync(postResponse);

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _postController.CreateAsync(postRequest);

            // Asset
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            LoginResponse currentUser = new()
            {
                User = new()
                {
                    UserId = 1
                }
            };

            PostRequest newPost = new()
            {
                Title = "Test 1",
                Desc = "Test 1",
                Tags = new()
            };

            _postServiceMock
                .Setup(x => x.CreateAsync(It.IsAny<PostRequest>()))
                .ReturnsAsync(() => throw new Exception("This is an exception"));

            httpContext.Items["Login"] = currentUser;

            // Act
            var result = (IStatusCodeActionResult)await _postController.CreateAsync(newPost);

            // Asset
            Assert.Equal(500, result.StatusCode);
        }
    }
}
