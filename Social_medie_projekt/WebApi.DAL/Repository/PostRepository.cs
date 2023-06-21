namespace WebApi.Repository
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllAsync();
        Task<Post?> FindByIdAsync(int PostId);
        Task<List<Post>> FindAllByUserIdAsync(int UserId);
        Task<Post> CreateAsync(Post newPost);
        Task<Post?> DeleteAsync(int id);
        Task<Post?> UpdateAsync(int id, Post updatePost);
        Task<Post?> UpdatePostLikesAsync(int id, int like);
    }

    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext _context;

        public PostRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _context.Post
                .Include(c => c.User)
                .ThenInclude(user => user.UserImage)
                .Include(x => x.PostLikes)
                .Include(x => x.Tags)
                .OrderByDescending(d => d.Date)
                .ToListAsync();
        }

        public async Task<Post?> FindByIdAsync(int postId)
        {
            return await _context.Post
                .Include(c => c.User)
                .ThenInclude(user => user.UserImage)
                .Include(x => x.PostLikes)
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => postId == x.PostId);
        }

        public async Task<List<Post>> FindAllByUserIdAsync(int userId)
        {
            return await _context.Post
                .Include(c => c.User)
                .ThenInclude(user => user.UserImage)
                .Include(x => x.PostLikes)
                .Include(x => x.Tags)
                .Where(x => userId == x.UserId)
                .ToListAsync();
        }

        public async Task<Post> CreateAsync(Post newPost)
        {
            _context.Post.Add(newPost);
            await _context.SaveChangesAsync();
            return newPost;
        }

        public async Task<Post?> UpdateAsync(int id, Post updatePost)
        {
            var post = await FindByIdAsync(id);

            if (post != null)
            {
                post.Title = updatePost.Title;
                post.Desc = updatePost.Desc;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            return post;
        }

        public async Task<Post?> DeleteAsync(int id)
        {
            var post = await FindByIdAsync(id);

            if (post != null)
            {
                _context.Remove(post);
                await _context.SaveChangesAsync();
            }
            return post;
        }

        // For adding removing a like from the PostLikes count
        public async Task<Post?> UpdatePostLikesAsync(int id, int like)
        {
            var post = await FindByIdAsync(id);

            if (post != null)
            {
                post.PostLikes.Likes += like;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            return post;
        }
    }
}
