namespace WebApi.Service
{
    public interface IPostService
    {
        Task<List<PostResponse>> GetAllPostsAsync();
        Task<PostResponse?> GetPostById(int Id);
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
                Date = post.Date,
                Likes = post.Likes,
                User = new PostUserResponse
                {
                    UserId = post.UserId,
                    FirstName = post.User.FirstName,
                    LastName = post.User.LastName,
                    Address = post.User.Address,
                    Created = post.User.Created,
                },
                Tags = post.Tags.Select(x => new PostTagResponse
                {
                    TagId = x.TagId,
                    tag = x.tag
                }).ToList()
            };
        }

        private Posts MapPostRequestToPost(PostRequest postRequest)
        {
            return new Posts
            {
                UserId = postRequest.UserId,
                Title = postRequest.Title,
                Desc = postRequest.Desc,
                Tags = postRequest.Tags.Select(x => new Tag 
                {
                    tag = x.Tag,
                }).ToList()
            };
        }

        private Posts MapPostUpdateRequestToPost(PostUpdateRequest postUpdateRequest)
        {
            return new Posts
            {
                Title = postUpdateRequest.Title,
                Desc = postUpdateRequest.Desc,
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
            //if(newPost.Tags.IndexOf())

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

        public async Task<PostResponse?> GetPostById(int Id)
        {
           var posts = await _postRepository.GetPostByIdAsync(Id);

            if(posts == null)
            {
                return null;
            }
            return MapPostToPostResponse(posts);
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
