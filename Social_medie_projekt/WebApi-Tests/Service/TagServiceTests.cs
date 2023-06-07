namespace WebApi_Tests.Service
{
    public class TagServiceTests
    {
        private readonly TagService _tagService;
        private readonly Mock<ITagRepository> _tagRepositoryMock;
        private readonly Mock<IPostTagRepository> _postTagRepositoryMock;

        public TagServiceTests()
        {
            _tagService = new(
                _tagRepositoryMock.Object,
                _postTagRepositoryMock.Object);
        }


    }
}
