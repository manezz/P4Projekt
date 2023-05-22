using System.Linq;

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
        public async void FindByIdAsync_ShouldReturnPostResponse_WhenPostExists()
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
                .Setup(x => x.FindByIdAsync(postId))
                .ReturnsAsync(post);

            // Act
            var result = await _postService.FindByIdAsync(postId, likeUserId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PostResponse>(result);
            Assert.Equal(post.PostId, result?.PostId);
            Assert.Equal(post.Title, result?.Title);
            Assert.Equal(post.Desc, result?.Desc);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnNull_WhenPostDoesNotExist()
        {
            // Arrange
            int postId = 1;
            int likeUserId = 1;

            _postRepositoryMock
                .Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(() => null!);

            // Act
            var result = await _postService.FindByIdAsync(postId, likeUserId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateAsync_ShouldReturnPostResponse_WhenPostUpdateIsSuccess()
        {
            // Arrange
            PostUpdateRequest postUpdateRequest = new()
            {
                Title = "Title1",
                Desc = "Desc1"
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
                .Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Post>()))
                .ReturnsAsync(post);

            // Act
            var result = await _postService.UpdateAsync(postId, postUpdateRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PostResponse>(result);
            Assert.Equal(postId, result?.PostId);
            Assert.Equal(postUpdateRequest.Title, result?.Title);
            Assert.Equal(postUpdateRequest.Desc, result?.Desc);
        }

        [Fact]
        public async void UpdateAsync_ShouldReturnPostResponseWithTags_WhenPostUpdateIsSuccessAndTagsAreNotNull()
        {
            // Arrange
            List<Tag> oldTags = new()
            {
                new Tag { TagId = 1, Name = "Tag1" },
                new Tag { TagId = 2, Name = "Tag2" }
            };

            PostUpdateRequest postUpdateRequest = new()
            {
                Title = "Title1",
                Desc = "Desc1",
                Tags = new()
                {
                    new TagRequest { Name = "Tag1" },
                    new TagRequest { Name = "Tag2" }
                }
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

            Post postAfterTags = new()
            {
                PostId = postId,
                Title = "Title1",
                Desc = "Desc1",
                PostLikes = new(),
                User = new()
                {
                    UserImage = new()
                },
                Tags =
                {
                    new Tag { TagId = 1, Name = "Tag1" },
                    new Tag { TagId = 2, Name = "Tag2" }
                }
            };

            _tagRepositoryMock
                .Setup(x => x.FindAllByPostIdAsync(It.IsAny<int>()))
                .ReturnsAsync(oldTags);

            _postRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Post>()))
                .ReturnsAsync(post);

            _postRepositoryMock
                .Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(postAfterTags);

            // Act
            var result = await _postService.UpdateAsync(postId, postUpdateRequest);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PostResponse>(result);
            Assert.Equal(postId, result?.PostId);
            Assert.Equal(postUpdateRequest.Title, result?.Title);
            Assert.Equal(postUpdateRequest.Desc, result?.Desc);
            Assert.Equal(postUpdateRequest.Tags.Select(x => x.Name), result?.Tags?.Select(x => x.Name)!);
        }

        [Fact]
        public async void UpdateAsync_ShouldReturnNull_WhenPostDoesNotExist()
        {
            // Arange
            PostUpdateRequest postUpdateRequest = new()
            {
                Title = "Title1",
                Desc = "Desc1"
            };
            int postId = 1;

            _postRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Post>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _postService.UpdateAsync(postId, postUpdateRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void DeleteAsync_ShuldReturnPostReponse_WhenDeleteSuccess()
        {
            // Arrange
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
                .Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(post);

            // Act
            var result = await _postService.DeleteAsync(postId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PostResponse>(result);
            Assert.Equal(postId, result?.PostId);
        }

        [Fact]
        public async void DeleteAsync_ShuldReturnNull_WhenPostDoesNotExist()
        {
            // Arrange
            int postId = 1;

            _postRepositoryMock
                .Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _postService.DeleteAsync(postId);

            // Assert
            Assert.Null(result);
        }
    }
}
