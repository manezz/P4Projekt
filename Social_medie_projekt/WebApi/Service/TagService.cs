namespace WebApi.Service
{
    public interface ITagService
    {
        Task<List<TagResponse>> GetAllTagsAsync();
        Task<TagResponse?> GetTagById(int Id);
        Task<TagResponse> CreateTagAsync(TagRequest newTag);
    }
    public class TagService : ITagService
    {

        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        private TagResponse MapTagToTagResponse(Tag tag)
        {
            return new TagResponse
            {
                TagId = tag.TagId,
                tag = tag.tag,
                post = tag.Posts.Select(x => new TagPostResponse
                {
                    PostId = x.PostId,
                    UserId = x.UserId,
                    Title = x.Title,
                    Desc = x.Desc,
                    Date = x.Date,
                    Likes = x.Likes,
                    User = new PostUserResponse
                    {
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                    }
                }).ToList()
            };
        }

        private Tag MapTagRequestToTag(TagRequest tagRequest)
        {
            return new Tag
            {
                TagId = tagRequest.TagId,
                tag = tagRequest.tag
            };
        }

        public async Task<TagResponse> CreateTagAsync(TagRequest newTag)
        {
            var tag = await _tagRepository.CreateTagAsync(MapTagRequestToTag(newTag));
            if(tag == null)
            {
                throw new ArgumentNullException();
            }
            return MapTagToTagResponse(tag);
        }
        public async Task<List<TagResponse>> GetAllTagsAsync()
        {
            List<Tag> tags = await _tagRepository.GetAllAsync();

            if (tags == null)
            {
                throw new ArgumentNullException();
            }

            return tags.Select(Tag => MapTagToTagResponse(Tag)).ToList();
        }

        public async Task<TagResponse?> GetTagById(int Id)
        {
            var tags = await _tagRepository.GetTagByIdAsync(Id);

            if (tags == null)
            {
                return null;
            }
            return MapTagToTagResponse(tags);
        }
    }
}
