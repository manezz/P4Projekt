using Microsoft.IdentityModel.Tokens;

namespace WebApi.Service
{
    public interface IPostService
    {
        // Post
        Task<List<PostResponse>?> GetAllAsync(int likeUserId);
        Task<PostResponse?> GetPostByPostIdAsync(int postId, int likeUserId);
        Task<List<PostResponse?>> GetAllPostsByUserIdAsync(int userId, int likeUserId);
        Task<PostResponse> CreatePostAsync(PostRequest newPost);
        Task<PostResponse?> UpdatePostAsync(int postId, PostUpdateRequest updatePost);
        Task<PostResponse?> DeletePostAsync(int postId);
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

        // With tag list
        private static PostResponse MapPostToPostResponse(Post post, List<TagResponse> tags)
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
                Tags = tags
            };
        }

        private static PostResponse MapPostToPostResponse(Post post, List<Tag> tags, Like like)
        {
            return new PostResponse
            {
                PostId = post.PostId,
                Title = post.Title,
                Desc = post.Desc,
                Date = post.Date,
                LikeUserId = like?.UserId,
                PostLikes = new PostPostLikesResponse
                {
                    Likes = post.PostLikes.Likes
                },
                User = new PostUserResponse
                {
                    UserId = post.User.UserId,
                    UserName = post.User.UserName,
                    UserImage = new PostUserUserImageResponse
                    {
                        Image = Convert.ToBase64String(post.User.UserImage.Image)
                    }
                },
                Tags = tags.Select(x => new TagResponse
                {
                    TagId = x.TagId,
                    Name = x.Name,
                })
                .ToList()
            };
        }

        private static PostResponse MapPostToPostResponse(Post post, List<Tag> tags)
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
                Tags = tags.Select(x => new TagResponse
                {
                    TagId = x.TagId,
                    Name = x.Name,
                })
                .ToList()
            };
        }

        // Without tags
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
                }
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

        private Post MapPostUpdateRequestToPost(PostUpdateRequest postUpdateRequest)
        {
            return new Post
            {
                Title = postUpdateRequest.Title,
                Desc = postUpdateRequest.Desc,
            };
        }

        public async Task<List<PostResponse>?> GetAllAsync(int likeUserId)
        {
            List<Post> posts = await _postRepository.GetAllAsync();

            if (posts != null)
            {
                //return posts.Select(post =>
                //    MapPostToPostResponse(post,
                //    _tagRepository.GetTagsByPostIdAsync(post.PostId).Result,
                //    _likeRepository.FindLikeAsync(likeUserId, post.PostId).Result)).ToList();

                return posts.Select(post =>
                    MapPostToPostResponse(post,
                    _tagRepository.GetTagsByPostIdAsync(post.PostId).Result ?? new List<Tag>(),
                    _likeRepository.FindLikeAsync(likeUserId, post.PostId).Result ?? new Like())).ToList();
            }

            return null;
        }

        public async Task<PostResponse?> GetPostByPostIdAsync(int postId, int likeUserId)
        {
            var post = await _postRepository.GetByIdAsync(postId);

            if (post != null)
            {
                return MapPostToPostResponse(post, await _tagRepository.GetTagsByPostIdAsync(postId),
                    _likeRepository.FindLikeAsync(likeUserId, post.PostId).Result);
            }

            return null;
        }

        public async Task<List<PostResponse?>> GetAllPostsByUserIdAsync(int userId, int likeUserId)
        {
            List<Post?> post = await _postRepository.GetAllByUserIdAsync(userId);

            if (post == null)
            {
                throw new ArgumentNullException();
            }

            return post.Select(post =>
            MapPostToPostResponse(post,
            _tagRepository.GetTagsByPostIdAsync(post.PostId).Result,
            _likeRepository.FindLikeAsync(likeUserId, post.PostId).Result)).ToList();
        }

        public async Task<PostResponse> CreatePostAsync(PostRequest newPost)
        {
            var post = await _postRepository.CreateAsync(MapPostRequestToPost(newPost));
            if (post == null)
            {
                throw new ArgumentNullException();
            }

            var tags = newPost.Tags.Select(tag => _tagService.CreateTagAsync(tag).Result).ToList();

            _ = tags.Select(tagResponse => _postTagService.CreatePostTagAsync(post.PostId, tagResponse.TagId).Result).ToList();

            return MapPostToPostResponse(post, tags);
        }

        public async Task<PostResponse?> UpdatePostAsync(int postId, PostUpdateRequest updatePost)
        {
            var oldtags = await _tagRepository.GetTagsByPostIdAsync(postId);

            if (oldtags == null)
            {
                return null;
            }

            var updateTags = updatePost.Tags
                .Select(x => TagService.MapTagRequestToTag(x))
                .ToList();

            var post = await _postRepository.UpdateByIdAsync(postId, MapPostUpdateRequestToPost(updatePost));
            var tags = updatePost.Tags.Select(tag => _tagService.UpdateTagAsync(tag).Result).ToList();

            if (post == null)
            {
                return null;
            }

            var tagCreate = updateTags
                .Where(x => !(oldtags.Select(z => z.Name))
                .Contains(x.Name))
                .ToList();

            var tagDelete = oldtags
                .Where(x => !(updateTags.Select(z => z.Name))
                .Contains(x.Name))
                .ToList();

            var tagsDeleted = tagDelete
                .Select(x => _postTagService.DeletePostTagByPostIdAsync(postId, x.TagId).Result)
                .ToList();

            var tagsCreated = tagCreate
                .Select(x => _tagRepository.CreateTagAsync(x).Result)
                .ToList();

            _ = tagsCreated
                .Select(x => _postTagService.CreatePostTagAsync(post.PostId, x.TagId).Result)
                .ToList();

            var currentTags = await _tagRepository.GetTagsByPostIdAsync(postId);

            if (currentTags == null)
            {
                return null;
            }

            return MapPostToPostResponse(post, currentTags);
        }

        public async Task<PostResponse?> DeletePostAsync(int postId)
        {
            var post = await _postRepository.DeleteByIdAsync(postId);
            if (post == null)
            {
                return null;
            }
            return MapPostToPostResponse(post);
        }
    }
}
