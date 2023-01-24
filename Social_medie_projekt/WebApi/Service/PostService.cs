namespace WebApi.Service
{
    public interface IPostService
    {
        Task<List<PostResponse>> GetAllPostsAsync();
        Task<PostResponse?> GetPostById(int Id);
        Task<PostResponse> CreatePostAsync(PostRequest newPost);
        Task<PostResponse?> EditPostAsync(int postId, PostRequest updatePost);
        Task<PostResponse?> DeletePost(int Id);

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
                    FirstName = post.User.FirstName,
                    LastName = post.User.LastName,
                    Address = post.User.Address,
                    Created = post.User.Created,
                },
                Tags = post.Tags.Select(x => new PostTagResponse
                {
                    Tag = x.Tag
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

        public async Task<PostResponse?> DeletePost(int Id)
        {
         
            var post = await _postRepository.DeletePostAsync(Id);

            if(post == null)
            {
                return null;
            }

            return MapPostToPostResponse(post);
        }

        public async Task<PostResponse?> EditPostAsync(int postId, PostRequest updatePost)
        {
            var post = await _postRepository.EditPost(postId, MapPostRequestToPost(updatePost));

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
    }
}
