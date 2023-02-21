namespace WebApi.Service
{
    public interface IPostService
    {
        Task<List<PostResponse>> GetAllPostsAsync();
        Task<PostResponse?> GetPostByPostIdAsync(int Id);
        Task<List<PostResponse?>> GetAllPostsByUserIdAsync(int Id);
        Task<PostResponse> CreatePostAsync(PostRequest newPost);
        Task<PostResponse> UpdatePostAsync(int postId, PostUpdateRequest updatePost);
        Task<PostResponse> DeletePostAsync(int postId);
    }
 
    public class PostService : IPostService
    {

        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }



        private PostResponse MapPostToPostResponse(Posts post)
        {
            return new PostResponse
            {
                PostId = post.PostId,
                Title = post.Title,
                Desc = post.Desc,
                Tags = post.Tags,
                Date = post.Date,
                Likes = post.Likes,

                User = new PostUserResponse
                {
                    UserId = post.UserId,
                    UserName = post.User.UserName,
                }
            };
        }

        private Posts MapPostRequestToPost(PostRequest postRequest)
        {
            return new Posts
            {
                UserId = postRequest.UserId,
                Title = postRequest.Title,
                Desc = postRequest.Desc,
                Tags = postRequest.Tags,
            };
        }



        private Posts MapPostUpdateRequestToPost(PostUpdateRequest postUpdateRequest)
        {
            return new Posts
            {
                Title = postUpdateRequest.Title,
                Desc = postUpdateRequest.Desc,
                Tags = postUpdateRequest.Tags,
            };
        }



        private Liked MapLikeRequestToLike(LikeRequest likedRequest)
        {
            return new Liked
            {
                UserId = likedRequest.UserId,
                PostId = likedRequest.PostId,
            };
        }

        private LikeResponse MapLikeToLikeResponse(Liked like)
        {
            return new LikeResponse
            {
                UserId = like.UserId,
                PostId = like.PostId,
                IsLiked = like.IsLiked,
            };
        }



        public async Task<List<PostResponse>> GetAllPostsAsync()
        {
            List<Posts> posts = await _postRepository.GetAllAsync();

            if (posts == null)
            {
                throw new ArgumentNullException();
            }

            return posts.Select(post => MapPostToPostResponse(post)).ToList();
        }

        public async Task<PostResponse?> GetPostByPostIdAsync(int postId)
        {
            var posts = await _postRepository.GetPostByPostIdAsync(postId);

            if (posts == null)
            {
                return null;
            }
            return MapPostToPostResponse(posts);
        }

        public async Task<List<PostResponse?>> GetAllPostsByUserIdAsync(int userId)
        {
            var posts = await _postRepository.GetAllPostsByUserIdAsync(userId);

            if (posts == null)
            {
                return null;
            }
            return posts.Select(post => MapPostToPostResponse(post)).ToList();
        }



        public async Task<PostResponse> CreatePostAsync(PostRequest newPost)
        {
            var post = await _postRepository.CreatePostAsync(MapPostRequestToPost(newPost));

            if(post == null)
            {
                throw new ArgumentNullException();
            }

            return MapPostToPostResponse(post);

        }

        public async Task<PostResponse> UpdatePostAsync(int postId, PostUpdateRequest updatePost)
        {
            var post = await _postRepository.UpdatePostAsync(postId, MapPostUpdateRequestToPost(updatePost));

            if(post != null)
            {
                return MapPostToPostResponse(post);
            }
            return null;
        }

        public async Task<PostResponse> DeletePostAsync(int postId)
        {
            var post = await _postRepository.DeletePostAsync(postId);

            if (post == null)
            {
                return null;
            }

            return MapPostToPostResponse(post);
        }
    }
}
