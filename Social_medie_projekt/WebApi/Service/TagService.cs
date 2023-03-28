namespace WebApi.Service
{
    public interface ITagService
    {
        // Tag
        Task<List<TagResponse>> GetAllTagsAsync();
        Task<TagResponse?> GetTagById(int Id);
        Task<List<TagResponse>> GetTagsByPostIdAsync(int postId);
        Task<TagResponse> CreateTagAsync(TagRequest newTag);
        Task<TagResponse> UpdateTagAsync(TagRequest newTag);
    }

    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }


        // ----------------------------------------------
        // ------------- TAG RES/REQ --------------------
        // ----------------------------------------------


        private static TagResponse MapTagToTagResponse(Tag tag)
        {
            return new TagResponse
            {
                TagId = tag.TagId,
                Name = tag.Name,
            };
        }


        // public bc using in PostService
        public static Tag MapTagRequestToTag(TagRequest tagRequest)
        {
            return new Tag
            {
                Name = tagRequest.Name
            };
        }



        // ---------------------- TAGS ----------------------
        public async Task<List<TagResponse>> GetAllTagsAsync()
        {
            List<Tag> tags = await _tagRepository.GetAllTagsAsync();

            if (tags == null)
            {
                throw new ArgumentNullException();
            }

            return tags.Select(Tag => MapTagToTagResponse(Tag)).ToList();
        }

        public async Task<TagResponse?> GetTagById(int tagId)
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

        public async Task<TagResponse> CreateTagAsync(TagRequest newTag)
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
