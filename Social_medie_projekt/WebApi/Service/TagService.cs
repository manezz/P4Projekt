using WebApi.Database.Entities;

namespace WebApi.Service
{
    public interface ITagService
    {
        Task<List<TagResponse>> GetAllAsync();
        Task<TagResponse?> FindByIdAsync(int tagId);
        Task<List<TagResponse>> FindAllByPostIdAsync(int postId);
        Task<TagResponse> CreateAsync(TagRequest newTag);
        Task<List<TagResponse>?> UpdateBulkByPostIdAsync(int postId, List<TagRequest> tagRequests);
    }

    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IPostTagService _postTagService;
        public TagService(ITagRepository tagRepository, IPostTagService postTagService)
        {
            _tagRepository = tagRepository;
            _postTagService = postTagService;
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

        public async Task<List<TagResponse>?> UpdateBulkByPostIdAsync(int postId, List<TagRequest> tagRequests)
        {
            // Gets the old tags
            var oldTags = await FindAllByPostIdAsync(postId);

            // Maps the tags from the request
            var updateTags = tagRequests
                .Select(tagRequests => MapTagRequestToTag(tagRequests))
                .ToList();

            // Checks if the tags already exists
            var tagsExists = updateTags
                .Select(x => _tagRepository.FindByNameAsync(x.Name).Result)
                .ToList();

            // Creates the Tags
            var tags = updateTags
                .Where(x => !(tagsExists
                    .Select(z => z.Name))
                .Contains(x.Name))
                .Select(tag => _tagRepository.CreateAsync(tag).Result)
                .ToList();

            var tagCreateNew = updateTags

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
                .Select(x => _postTagService.DeleteAsync(postId, x.TagId).Result)
                .ToList();

            // Creates the tags
            var tagsCreated = tagCreate
                .Select(x => _tagRepository.CreateAsync(x).Result)
                .ToList();

            // Creates the posttags
            _ = tagsCreated
                .Select(x => _postTagService.CreateAsync(post.PostId, x.TagId).Result)
                .ToList();

            return null;
        }


    }
}

