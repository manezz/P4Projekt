namespace WebApi.Service
{
    public interface IPostTagService
    {
        Task<List<PostTagResponse>> FindAllByPostIdAsync(int postId);
        Task<PostTagResponse> CreateAsync(int postId, int tagId);
        Task<PostTagResponse> UpdateAsync(int postId, int tagId);
        Task<PostTagResponse> DeleteAsync(int postId, int tagId);
    }
    public class PostTagService : IPostTagService
    {
        public readonly IPostTagRepository _postTagRepository;

        public PostTagService(IPostTagRepository postTagRepository)
        {
            _postTagRepository = postTagRepository;
        }

        private static PostTagResponse MapPostTagToPostTagResponse(PostTag postsTag)
        {
            return new PostTagResponse
            {
                PostId = postsTag.PostId,
                TagId = postsTag.TagId,
            };
        }

        //New mapping
        private static PostTag MapPostTagResponseToPostTag(PostTagResponse postTagResponse)
        {
            return new PostTag
            {
                PostId = postTagResponse.PostId,
                TagId = postTagResponse.TagId,
            };
        }

        // change name
        private static PostTag MapPostTagRequestToPostTag(int postId, int tagId)
        {
            return new PostTag
            {
                PostId = postId,
                TagId = tagId,
            };
        }

        public async Task<List<PostTagResponse>> FindAllByPostIdAsync(int postId)
        {
            var postTags = await _postTagRepository.FindAllByPostIdAsync(postId);

            if (postTags == null)
            {
                throw new ArgumentNullException();
            }

            return postTags.Select(posttag => MapPostTagToPostTagResponse(posttag)).ToList();
        }

        public async Task<PostTagResponse> CreateAsync(int postId, int tagId)
        {
            var postTag = await _postTagRepository.CreateAsync(MapPostTagRequestToPostTag(postId, tagId));

            if (postTag == null)
            {
                throw new ArgumentNullException();
            }

            return MapPostTagToPostTagResponse(postTag);
        }

        public async Task<PostTagResponse> UpdateAsync(int postId, int tagId)
        {

            var postTag = await _postTagRepository.CreateAsync(MapPostTagRequestToPostTag(postId, tagId));

            if (postTag == null)
            {
                throw new ArgumentNullException();
            }


            return MapPostTagToPostTagResponse(postTag);
        }

        public async Task<PostTagResponse> DeleteAsync(int postId, int tagId)
        {
            var postTag = await _postTagRepository.DeleteAsync(postId, tagId);

            if (postTag == null)
            {
                throw new ArgumentNullException();
            }

            return MapPostTagToPostTagResponse(postTag);
        }
    }
}
