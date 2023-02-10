namespace WebApi.Service
{
    public interface IPostService
    {
        Task<List<PostResponse>> GetAllPostsAsync();
        Task<PostResponse?> GetPostByPostIdAsync(int Id);
        Task<List<PostResponse?>> GetPostByUserIdAsync(int Id);
        Task<PostResponse> CreatePostAsync(PostRequest newPost);
        Task<PostResponse?> UpdatePostAsync(int postId, PostUpdateRequest updatePost);
        Task<PostResponse?> DeletePostAsync(int postId);
        Task<LikedResponse> CreateLikeAsync(LikedRequest newLike);
        Task<LikedResponse?> DeleteLikeAsync(LikedRequest deleteLike);
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
                UserId = post.UserId,
                Title = post.Title,
                Desc = post.Desc,
                Tags = post.Tags,
                Date = post.Date,
                Likes = post.Likes,
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

        private Liked MapLikeRequestToLike(LikedRequest likedRequest)
        {
            return new Liked
            {
                UserId = likedRequest.UserId,
                PostId = likedRequest.PostId,
            };
        }

        private LikedResponse MapLikeToLikeResponse(Liked like)
        {
            return new LikedResponse
            {
                UserId = like.UserId,
                PostId = like.PostId,
                LikedTime = like.LikedTime,
            };
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

        public async Task<PostResponse?> DeletePostAsync(int postId)
        {
            var post = await _postRepository.DeletePostAsync(postId);

            if(post == null)
            {
                return null;
            }

            return MapPostToPostResponse(post);
        }

        public async Task<PostResponse?> UpdatePostAsync(int postId, PostUpdateRequest updatePost)
        {
            var post = await _postRepository.UpdatePostAsync(postId, MapPostUpdateRequestToPost(updatePost));

            if(post != null)
            {
                return MapPostToPostResponse(post);
            }
            return null;
        }

        public async Task<List<PostResponse>> GetAllPostsAsync()
        {
            List<Posts> posts = await _postRepository.GetAllAsync();

            if(posts == null)
            {
                throw new ArgumentNullException();
            }

            return posts.Select(post => MapPostToPostResponse(post)).ToList();
        }

        public async Task<PostResponse?> GetPostByPostIdAsync(int postId)
        {
           var posts = await _postRepository.GetPostByPostIdAsync(postId);

            if(posts == null)
            {
                return null;
            }
            return MapPostToPostResponse(posts);
        }

        public async Task<List<PostResponse?>> GetPostByUserIdAsync(int userId)
        {
            var posts = await _postRepository.GetPostByUserIdAsync(userId);

            if (posts == null)
            {
                return null;
            }
            return posts.Select(post => MapPostToPostResponse(post)).ToList();
        }

        public async Task<LikedResponse> CreateLikeAsync(LikedRequest newLike)
        {
            var like = await _postRepository.CreateLikeAsync(MapLikeRequestToLike(newLike));

            var post = await _postRepository.UpdatePostLikesAsync(newLike.PostId, 1);

            if (like == null || post == null)
            {
                throw new ArgumentNullException();
            }

            return MapLikeToLikeResponse(like);
        }

        public async Task<LikedResponse?> DeleteLikeAsync(LikedRequest deleteLike)
        {
            var like = await _postRepository.DeleteLikeAsync(MapLikeRequestToLike(deleteLike));

            var post = await _postRepository.UpdatePostLikesAsync(deleteLike.PostId, -1);

            if (like == null || post == null)
            {
                throw new ArgumentNullException();
            }

            return MapLikeToLikeResponse(like);
        }
    }
}
