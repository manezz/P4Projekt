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
    }
}
