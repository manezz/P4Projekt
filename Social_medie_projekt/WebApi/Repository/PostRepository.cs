namespace WebApi.Repository
{
     public interface IPostRepository
    {
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetPostByPostIdAsync(int PostId);
        Task<List<Post?>> GetAllPostsByUserIdAsync(int UserId);
        Task<Post> CreatePostAsync(Post newPost);
        Task<Post> UpdatePostAsync(int id, Post updatePost);
        Task<Post> DeletePostAsync(int id);
        Task<Post> UpdatePostLikesAsync(int id, int like);        
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
            return await _context.Posts.Include(c => c.User)
                 .OrderByDescending(d => d.Date)
                 .ToListAsync();
        }

        public async Task<Post?> GetPostByPostIdAsync(int postId)
        {
            return await _context.Posts.Include(c => c.User).FirstOrDefaultAsync(x => postId == x.PostId);
        }

        public async Task<List<Post?>> GetAllPostsByUserIdAsync(int userId)
        {
            return await _context.Posts.Include(c => c.User).Where(x => userId == x.UserId).ToListAsync();
        }

        public async Task<Post> CreatePostAsync(Post newPost)
        {
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            return newPost;
        }

        public async Task<Post> UpdatePostAsync(int id, Post updatePost)
        {
            var post = await GetPostByPostIdAsync(id);

            if(post != null)
            {
                post.Title = updatePost.Title;
                post.Desc = updatePost.Desc;
                post.Tags = updatePost.Tags;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            return post;
        }       

        public async Task<Post> DeletePostAsync(int id)
        {
            var post = await GetPostByPostIdAsync(id);

            if(post != null)
            {
                _context.Remove(post);
                await _context.SaveChangesAsync();
            }
            return post;
        }



        // For updating each post that has/needs a like from likeService
        public async Task<Post> UpdatePostLikesAsync(int id, int like)
        {
            var post = await GetPostByPostIdAsync(id);

            if (post != null)
            {
                post.Likes += like;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            return post;
        }
    }
}
