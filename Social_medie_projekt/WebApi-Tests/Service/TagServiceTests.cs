using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApi_Tests.Service
{
    public class TagServiceTests
    {
        private readonly TagService _tagService;
        private readonly Mock<ITagRepository> _tagRepositoryMock = new();
        private readonly Mock<IPostTagRepository> _postTagRepositoryMock = new();

        public TagServiceTests()
        {
            _tagService = new(
                _tagRepositoryMock.Object,
                _postTagRepositoryMock.Object);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfTagResponses_WhenTagsExists()
        {
            // Arrange
            List<Tag> tags = new()
            {
                new()
                {
                    TagId = 1,
                    Name = "Tag1"
                },
                new()
                {
                    TagId = 2,
                    Name = "Tag2"
                }
            };

            _tagRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(tags);

            // Act
            var result = await _tagService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TagResponse>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnEmptyListOfTagResponses_WhenNoTagsExists()
        {
            // Arrange
            List<Tag> tags = new();

            _tagRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(tags);

            // Act
            var result = await _tagService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TagResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void GetAllAsync_ShouldThrowNullExeption_WhenRepositoryReturnsNull()
        {
            // Arrange
            List<Tag> tags = null!;

            _tagRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(tags);

            // Act
            async Task action() => await _tagService.GetAllAsync();

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(action);
            Assert.Contains("Value cannot be null", ex.Message);
        }

        [Fact]
        public async void CreateAsync_ShouldReturnTagResponse_WhenTagCreateIsSuccess()
        {
            // Arrange
            TagRequest newTag = new()
            {
                Name = "Tag1"
            };
            int tagId = 1;

            Tag tag = new()
            {
                TagId = tagId,
                Name = newTag.Name
            };

            _tagRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Tag>()))
                .ReturnsAsync(tag);

            // Act
            var result = await _tagService.CreateAsync(newTag);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TagResponse>(result);
            Assert.Equal(tag.TagId, result?.TagId);
            Assert.Equal(tag.Name, result?.Name);
        }

        [Fact]
        public async void CreateAsync_ShouldThrowNullExeption_WhenRepositoryReturnsNull()
        {
            // Arrange
            TagRequest newTag = new()
            {
                Name = "Tag1"
            };

            _tagRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Tag>()))
                .ReturnsAsync(() => null!);

            // Act
            async Task action() => await _tagService.CreateAsync(newTag);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(action);
            Assert.Contains("Value cannot be null", ex.Message);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnTagResponse_WhenTagExists()
        {
            // Arrange
            int tagId = 1;

            Tag tag = new()
            {
                TagId = tagId,
                Name = "Tag1"
            };

            _tagRepositoryMock
                .Setup(x => x.FindByIdAsync(tagId))
                .ReturnsAsync(tag);

            // Act
            var result = await _tagService.FindByIdAsync(tagId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TagResponse>(result);
            Assert.Equal(tag.TagId, result?.TagId);
            Assert.Equal(tag.Name, result?.Name);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnNull_WhenTagDoesNotExist()
        {
            // Arrange
            int tagId = 1;

            _tagRepositoryMock
                    .Setup(x => x.FindByIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(() => null!);

            // Act
            var result = await _tagService.FindByIdAsync(tagId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void UpdateBatchByPostIdAsync_ShouldReturnListOfTagResponses_WhenPostUpdateIsSuccessAndTagsAreNotNull()
        {
            // Arrange
            List<TagResponse> updatedTags = new()
            {
                new TagResponse { TagId = 1, Name = "Tag1" },
                new TagResponse { TagId = 2, Name = "Tag2" }
            };

            List<TagRequest> tagRequests = new()
            {
                new TagRequest { Name = "Tag1" },
                new TagRequest { Name = "Tag2" }
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

            tagRequests.Select(y => y.Name).ToList();

            _tagRepositoryMock
                .Setup(x => tagRequests.Select(y => x.CreateAsync(y)) CreateAsync())
                .ReturnsAsync(updatedTags)

            //_tagServiceMock
            //    .Setup(x => x.UpdateBulkByPostIdAsync(It.IsAny<int>(), new List<TagRequest>()))
            //    .ReturnsAsync(updatedTags);

            //_postRepositoryMock
            //    .Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Post>()))
            //    .ReturnsAsync(post);

            //_postRepositoryMock
            //    .Setup(x => x.FindByIdAsync(It.IsAny<int>()))
            //    .ReturnsAsync(postAfterTags);

            // Act
            var result = await _tagService.UpdateBatchByPostIdAsync(postId, tagRequests);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TagResponse>>(result);
            Assert.Equal(tagRequests.Select(x => x.Name), result?.Select(x => x.Name)!);
        }
    }
}
