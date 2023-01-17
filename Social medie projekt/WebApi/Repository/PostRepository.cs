namespace WebApi.Repository
{
     public interface IPostRepository
    {
        Task<List<Posts>> GetAllAsync();
        Task<Posts?> GetPostByIdAsync(int id);

        Task<Posts> CreatePostAsync(Posts newPost);

        Task<Posts> DeletePostAsync(int id);

        Task<Posts> EditPost(int id, Posts updatePost);

    }

    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext _context;

        public PostRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Posts> CreatePostAsync(Posts newPost)
        {
           _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            newPost = await GetPostByIdAsync(newPost.PostId);
            return newPost;
        }

        public async Task<Posts> DeletePostAsync(int id)
        {
            var post = await GetPostByIdAsync(id);

            if(post != null)
            {
                _context.Remove(post);
                await _context.SaveChangesAsync();
            }
            return post;
        }

        public async Task<Posts> EditPost(int id, Posts updatePost)
        {
            var post = await GetPostByIdAsync(id);

            if(post != null)
            {
                post.PostInput = updatePost.PostInput;
            }

            return post;
        }

        public async Task<List<Posts>> GetAllAsync()
        {
           return await _context.Posts.ToListAsync();
        }

        public async Task<Posts?> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FindAsync(id);
        }
    }
}
