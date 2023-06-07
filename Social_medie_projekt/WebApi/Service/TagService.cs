namespace WebApi.Service
{
    public interface ITagService
    {
        Task<List<TagResponse>> GetAllAsync();
        Task<TagResponse?> FindByIdAsync(int tagId);
        Task<List<TagResponse>> FindAllByPostIdAsync(int postId);
        Task<TagResponse> CreateAsync(TagRequest newTag);
        Task<List<TagResponse>?> UpdateBatchByPostIdAsync(int postId, List<TagRequest> tagRequests);
    }

    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IPostTagRepository _postTagRepository;
        public TagService(ITagRepository tagRepository, IPostTagRepository postTagRepository)
        {
            _tagRepository = tagRepository;
            _postTagRepository = postTagRepository;
        }

        private static TagResponse MapTagToTagResponse(Tag tag)
        {
            return new TagResponse
            {
                TagId = tag.TagId,
                Name = tag.Name,
            };
        }

        public static Tag MapTagRequestToTag(TagRequest tagRequest)
        {
            return new Tag
            {
                Name = tagRequest.Name
            };
        }

        private static PostTag MapPostIdAndTagIdToPostTag(int postId, int tagId)
        {
            return new PostTag
            {
                PostId = postId,
                TagId = tagId,
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
            var mapTag = MapTagRequestToTag(newTag);

            var tagExists = await _tagRepository.FindByNameAsync(mapTag.Name);

            if (tagExists != null)
            {
                return MapTagToTagResponse(tagExists);
            }

            var tag = await _tagRepository.CreateAsync(mapTag);

            return tag == null ? throw new ArgumentNullException(null) : MapTagToTagResponse(tag);
        }

        public async Task<List<TagResponse>?> UpdateBatchByPostIdAsync(int postId, List<TagRequest> tagRequests)
        {
            var oldTags = await _tagRepository.FindAllByPostIdAsync(postId);

            if (oldTags == null)
            {
                return null!;
            }

            // Maps the tags from the request
            var updateTags = tagRequests
                .Select(tagRequests => MapTagRequestToTag(tagRequests))
                .ToList();

            // Creates list of Tags, where oldTags does not contain the name of the tag
            var tagCreate = updateTags
                .Where(x => !(oldTags
                    .Select(z => z.Name))
                .Contains(x.Name))
                .ToList();

            // Creates list of Tags, where updateTags does not contain the name of the tag
            var tagDelete = oldTags
                .Where(x => !(updateTags
                    .Select(z => z.Name))
                .Contains(x.Name))
                .ToList();

            // Deletes the tags
            var tagsDeleted = tagDelete
                .Select(x => _postTagRepository.DeleteAsync(postId, x.TagId).Result)
                .ToList();

            // Creates the tags
            var tagsCreated = tagCreate
                .Select(x => _tagRepository.FindByNameAsync(x.Name).Result ?? _tagRepository.CreateAsync(x).Result)
                .ToList();

            // Creates the posttags
            _ = tagsCreated
                .Select(x => _postTagRepository.CreateAsync(MapPostIdAndTagIdToPostTag(postId, x.TagId)).Result)
                .ToList();

            return tagsCreated
                .Select(x => MapTagToTagResponse(x))
                .ToList();
        }


    }
}

