namespace WebApi.Service
{
    public interface ITagService
    {
        // Tag
        Task<List<TagResponse>> GetAllAsync();
        Task<TagResponse?> GetByIdAsync(int Id);
        Task<List<TagResponse>> GetTagsByPostIdAsync(int postId);
        Task<TagResponse> CreateAsync(TagRequest newTag);
        Task<TagResponse> UpdateTagAsync(TagRequest newTag);
    }

    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        private static TagResponse MapTagToTagResponse(Tag tag)
        {
            return new TagResponse
            {
                TagId = tag.TagId,
                Name = tag.Name,
            };
        }

        // public bc using in PostService FIX
        public static Tag MapTagRequestToTag(TagRequest tagRequest)
        {
            return new Tag
            {
                Name = tagRequest.Name
            };
        }

        public async Task<List<TagResponse>> GetAllAsync()
        {
            List<Tag> tags = await _tagRepository.GetAllTagsAsync();

            if (tags == null)
            {
                throw new ArgumentNullException();
            }

            return tags.Select(Tag => MapTagToTagResponse(Tag)).ToList();
        }

        public async Task<TagResponse?> GetByIdAsync(int tagId)
        {
            var tags = await _tagRepository.GetTagByIdAsync(tagId);

            if (tags == null)
            {
                return null;
            }
            return MapTagToTagResponse(tags);
        }

        public async Task<List<TagResponse>> GetTagsByPostIdAsync(int postId)
        {
            var tags = await _tagRepository.GetTagsByPostIdAsync(postId);

            if (tags == null)
            {
                throw new ArgumentNullException();
            }

            return tags.Select(Tag => MapTagToTagResponse(Tag)).ToList();
        }

        public async Task<TagResponse> CreateAsync(TagRequest newTag)
        {
            var tag = await _tagRepository.CreateTagAsync(MapTagRequestToTag(newTag));

            if (tag == null)
            {
                throw new ArgumentNullException();
            }
            return MapTagToTagResponse(tag);
        }

        private async Task<Tag> CreateTagAsync(Tag newTag)
        {
            var tag = await _tagRepository.CreateTagAsync(newTag);

            if (tag == null)
            {
                throw new ArgumentNullException();
            }

            return tag;
        }

        public async Task<TagResponse> UpdateTagAsync(TagRequest newTag)
        {
            var tag = await _tagRepository.UpdateTagAsync(MapTagRequestToTag(newTag));

            if (tag == null)
            {
                throw new ArgumentNullException();
            }
            return MapTagToTagResponse(tag);
        }
    }
}
