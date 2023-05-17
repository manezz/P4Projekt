namespace WebApi.Service
{
    public interface ITagService
    {
        // Tag
        Task<List<TagResponse>> GetAllAsync();
        Task<TagResponse?> FindByIdAsync(int tagId);
        Task<List<TagResponse>> FindAllByPostIdAsync(int postId);
        Task<TagResponse> CreateAsync(TagRequest newTag);
        Task<TagResponse> UpdateAsync(TagRequest newTag);
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
            List<Tag> tags = await _tagRepository.GetAllAsync();

            if (tags == null)
            {
                throw new ArgumentNullException();
            }

            return tags.Select(Tag => MapTagToTagResponse(Tag)).ToList();
        }

        public async Task<TagResponse?> FindByIdAsync(int tagId)
        {
            var tags = await _tagRepository.FindByIdAsync(tagId);

            if (tags == null)
            {
                return null;
            }
            return MapTagToTagResponse(tags);
        }

        public async Task<List<TagResponse>> FindAllByPostIdAsync(int postId)
        {
            var tags = await _tagRepository.FindAllByPostIdAsync(postId);

            if (tags == null)
            {
                throw new ArgumentNullException();
            }

            return tags.Select(Tag => MapTagToTagResponse(Tag)).ToList();
        }

        public async Task<TagResponse> CreateAsync(TagRequest newTag)
        {
            var tag = await _tagRepository.CreateAsync(MapTagRequestToTag(newTag));

            if (tag == null)
            {
                throw new ArgumentNullException();
            }
            return MapTagToTagResponse(tag);
        }

        private async Task<Tag> CreateTagAsync(Tag newTag)
        {
            var tag = await _tagRepository.CreateAsync(newTag);

            if (tag == null)
            {
                throw new ArgumentNullException();
            }

            return tag;
        }

        public async Task<TagResponse> UpdateAsync(TagRequest newTag)
        {
            var tag = await _tagRepository.UpdateAsync(MapTagRequestToTag(newTag));

            if (tag == null)
            {
                throw new ArgumentNullException();
            }
            return MapTagToTagResponse(tag);
        }
    }
}
