namespace WebApi.Repository
{
     public interface IPostRepository
    {
        Task<List<Posts>> GetAllAsync();
        Task<Posts?> GetPostByIdAsync(int id);
        Task<Posts> CreatePostAsync(Posts newPost);
        Task<Posts> DeletePostAsync(int id);
        Task<Posts> UpdatePostAsync(int id, Posts updatePost);
        Task<Posts> UpdatePostLikesAsync(int id, int like);
        Task<Liked> CreateLikeAsync(Liked newLike);
        Task<Liked> DeleteLikeAsync(Liked like);
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

        public async Task<Posts> UpdatePostAsync(int id, Posts updatePost)
        {
            var post = await GetPostByIdAsync(id);

            if(post != null)
            {
                post.Title = updatePost.Title;
                post.Desc = updatePost.Desc;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            return post;
        }

        public async Task<Posts> UpdatePostLikesAsync(int id, int like)
        {
            var post = await GetPostByIdAsync(id);

            if (post != null)
            {
                post.Likes += like;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            return post;
        }

        public async Task<List<Posts>> GetAllAsync()
        {
           return await _context.Posts.Include(c => c.User).ToListAsync();
        }

        public async Task<Posts?> GetPostByIdAsync(int id)
        {
            return await _context.Posts.Include(c => c.User).FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<Liked> CreateLikeAsync(Liked newLike)
        {
            _context.Liked.Add(newLike);

            await _context.SaveChangesAsync();
            return newLike;
        }

        public async Task<Liked> DeleteLikeAsync(Liked like)
        {
            _context.Liked.Remove(like);

            await _context.SaveChangesAsync();
            return like;
        }
    }
}
