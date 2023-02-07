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
                post.Title = updatePost.Title;
                post.Desc = updatePost.Desc;
            }

            return post;
        }

        public async Task<List<Posts>> GetAllAsync()
        {
           return await _context.Posts.Include(c => c.User)
                .OrderByDescending(d => d.Date)
                .ToListAsync();
        }

        public async Task<Posts?> GetPostByIdAsync(int id)
        {
            return await _context.Posts
                .Include(u => u.User)
                .OrderByDescending(d => d.Date)
                .FirstOrDefaultAsync(x => x.UserId == id);
        }
    }
}
