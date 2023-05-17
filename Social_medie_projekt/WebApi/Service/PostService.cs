using Microsoft.IdentityModel.Tokens;

namespace WebApi.Service
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

        // With Like
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

        // Without Like
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
            // Throws ArgumentNullException if posts is null
            List<Post> posts = await _postRepository.GetAllAsync()
                ?? throw new ArgumentNullException(null);

            return posts.Select(post =>
                MapPostToPostResponse(post,
                // Gets like for each post 
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
            // Throws ArgumentNullException if posts is null
            List<Post> post = await _postRepository.FindAllByUserIdAsync(userId)
                ?? throw new ArgumentNullException(null);

            return post.Select(post =>
                MapPostToPostResponse(post,
                _likeRepository.FindByIdAsync(likeUserId, post.PostId).Result ?? new Like())).ToList();
        }

        public async Task<PostResponse> CreateAsync(PostRequest newPost)
        {
            // Throws ArgumentNullException if post is null
            var post = await _postRepository.CreateAsync(MapPostRequestToPost(newPost))
                ?? throw new ArgumentNullException(null);

            // Maps without tags if tags is null
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

            // Maps with tags if tags is not null
            return MapPostToPostResponse(postAfterTags);
        }

        public async Task<PostResponse?> UpdateAsync(int postId, PostUpdateRequest updatePost)
        {
            // Gets the old tags
            var oldtags = await _tagRepository.FindAllByPostIdAsync(postId);

            if (oldtags == null)
            {
                return null;
            }

            // Maps the tags from the request
            var updateTags = updatePost.Tags
                .Select(x => TagService.MapTagRequestToTag(x))
                .ToList();

            // Updates the post
            var post = await _postRepository.UpdateAsync(postId, MapPostUpdateRequestToPost(updatePost));
            // Updates the tags
            var tags = updatePost.Tags.Select(tag => _tagService.UpdateAsync(tag).Result).ToList();

            if (post == null)
            {
                return null;
            }

            // Creates list of Tags, where oldtags does not contain the name of the tag
            var tagCreate = updateTags
                .Where(x => !(oldtags.Select(z => z.Name))
                .Contains(x.Name))
                .ToList();

            // Creates list of Tags, where updateTags does not contain the name of the tag
            var tagDelete = oldtags
                .Where(x => !(updateTags.Select(z => z.Name))
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

            var postAfterTags = await _postRepository.FindByIdAsync(post.PostId)
                ?? throw new ArgumentNullException(null);

            // Maps the post with the current tags
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
