namespace WebApi.BLL.Service
{
    public interface IPostService
    {
        Task<List<PostResponse>> GetAllAsync(int likeUserId);
        Task<PostResponse?> FindByIdAsync(int postId, int likeUserId);
        Task<List<PostResponse>?> FindAllByUserIdAsync(int userId, int likeUserId);
        Task<PostResponse> CreateAsync(PostRequest newPost);
        Task<PostResponse?> UpdateAsync(int postId, PostUpdateRequest updatePost);
        Task<PostResponse?> DeleteAsync(int postId);
    }

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ITagService _tagService;
        private readonly ILikeRepository _likeRepository;
        private readonly IPostTagService _postTagService;

        public PostService(
            IPostRepository postRepository,
            ITagRepository tagRepository,
            ITagService tagService,
            ILikeRepository likeRepository,
            IPostTagService postTagService)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _tagService = tagService;
            _likeRepository = likeRepository;
            _postTagService = postTagService;
        }

        private static PostResponse MapPostToPostResponse(Post post, Like like)
        {
            return new PostResponse
            {
                PostId = post.PostId,
                Title = post.Title,
                Desc = post.Desc,
                Date = post.Date,
                LikeUserId = like.UserId,
                PostLikes = new PostPostLikesResponse
                {
                    Likes = post.PostLikes.Likes
                },
                User = new PostUserResponse
                {
                    UserId = post.User.UserId,
                    UserName = post.User.UserName,
                },
                Tags = post.Tags.Select(x => new PostResponseTag
                {
                    TagId = x.TagId,
                    Name = x.Name,
                }).ToList()
            };
        }

        private static PostResponse MapPostToPostResponse(Post post)
        {
            return new PostResponse
            {
                PostId = post.PostId,
                Title = post.Title,
                Desc = post.Desc,
                Date = post.Date,
                PostLikes = new PostPostLikesResponse
                {
                    Likes = post.PostLikes.Likes
                },
                User = new PostUserResponse
                {
                    UserId = post.User.UserId,
                    UserName = post.User.UserName,
                },
                Tags = post.Tags.Select(x => new PostResponseTag
                {
                    TagId = x.TagId,
                    Name = x.Name,
                }).ToList()
            };
        }


        private static Post MapPostRequestToPost(PostRequest postRequest)
        {
            return new Post
            {
                UserId = postRequest.UserId,
                Title = postRequest.Title,
                Desc = postRequest.Desc,
                PostLikes = new PostLikes { }
            };
        }

        private static Post MapPostUpdateRequestToPost(PostUpdateRequest postUpdateRequest)
        {
            return new Post
            {
                Title = postUpdateRequest.Title,
                Desc = postUpdateRequest.Desc,
            };
        }

        public async Task<List<PostResponse>> GetAllAsync(int likeUserId)
        {
            List<Post> posts = await _postRepository.GetAllAsync()
                ?? throw new ArgumentNullException(null);

            return posts.Select(post =>
                MapPostToPostResponse(post,
                _likeRepository.FindByIdAsync(likeUserId, post.PostId).Result ?? new Like())).ToList();
        }

        public async Task<PostResponse?> FindByIdAsync(int postId, int likeUserId)
        {
            var post = await _postRepository.FindByIdAsync(postId);

            if (post != null)
            {
                return MapPostToPostResponse(post,
                    _likeRepository.FindByIdAsync(likeUserId, post.PostId).Result ?? new Like());
            }

            return null;
        }

        public async Task<List<PostResponse>?> FindAllByUserIdAsync(int userId, int likeUserId)
        {
            List<Post> post = await _postRepository.FindAllByUserIdAsync(userId)
                ?? throw new ArgumentNullException(null);

            return post.Select(post =>
                MapPostToPostResponse(post,
                _likeRepository.FindByIdAsync(likeUserId, post.PostId).Result ?? new Like())).ToList();
        }

        public async Task<PostResponse> CreateAsync(PostRequest newPost)
        {
            var post = await _postRepository.CreateAsync(MapPostRequestToPost(newPost))
                ?? throw new ArgumentNullException(null);

            if (newPost.Tags == null)
            {
                return MapPostToPostResponse(post);
            }

            var tags = newPost.Tags
                .Select(tag => _tagService.CreateAsync(tag).Result)
                .ToList();

            _ = tags
                .Select(tagResponse => _postTagService.CreateAsync(post.PostId, tagResponse.TagId).Result)
                .ToList();

            var postAfterTags = await _postRepository.FindByIdAsync(post.PostId)
                ?? throw new ArgumentNullException(null);

            return MapPostToPostResponse(postAfterTags);
        }

        public async Task<PostResponse?> UpdateAsync(int postId, PostUpdateRequest updatePost)
        {
            var post = await _postRepository.UpdateAsync(postId, MapPostUpdateRequestToPost(updatePost));

            if (post == null)
            {
                return null;
            }

            var updatedTags = await _tagService.UpdateBatchByPostIdAsync(postId, updatePost.Tags);

            if (updatedTags == null)
            {
                return MapPostToPostResponse(post);
            }

            var postAfterTags = await _postRepository.FindByIdAsync(post.PostId);

            if (postAfterTags == null)
            {
                return null;
            }

            return MapPostToPostResponse(postAfterTags);
        }

        public async Task<PostResponse?> DeleteAsync(int postId)
        {
            var post = await _postRepository.DeleteAsync(postId);
            if (post == null)
            {
                return null;
            }
            return MapPostToPostResponse(post);
        }
    }
}
