namespace WebApi.Service
{
    public interface ITagService
    {
        Task<List<TagResponse>> GetAllAsync();
        Task<TagResponse?> GetTagById(int Id);
        Task<TagResponse> CreateTagAsync(TagRequest newTag);
    }
    public class TagService : ITagService
    {

        public readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public TagResponse MapTagToTagResponse(Tag tag)
        {
            return new TagResponse
            {
                TagId = tag.TagId,
                Name = tag.Name,
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
                        UserId = x.UserId,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Address = x.User.Address,
                        Created = x.User.Created,
                    }
                }).ToList()
            };
        }

        public Tag MapTagRequestToTag(TagRequest tagRequest)
        {
            return new Tag
            {
                TagId = tagRequest.TagId,
                Name = tagRequest.Name
            };
        }

        public async Task<TagResponse> CreateTagAsync(TagRequest newTag)
        {
            var tag = await _tagRepository.CreateTagAsync(MapTagRequestToTag(newTag));
            if (tag == null)
            {
                throw new ArgumentNullException();
            }
            return MapTagToTagResponse(tag);
        }
        public async Task<List<TagResponse>> GetAllAsync()
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
