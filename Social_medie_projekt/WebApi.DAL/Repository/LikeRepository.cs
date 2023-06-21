namespace WebApi.Repository
{
    public interface ILikeRepository
    {
        Task<Like?> FindByIdAsync(int userId, int postId);
        Task<List<Like>?> FindAllByUserIdAsync(int userId);
        Task<Like> CreateAsync(Like newLike);
        Task<Like?> DeleteAsync(int userId, int postId);
    }

    public class LikeRepository : ILikeRepository
    {
        private readonly DatabaseContext _context;

        public LikeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Like?> FindByIdAsync(int userId, int postId)
        {
            return await _context.Like
                .FindAsync(userId, postId);
        }

        public async Task<List<Like>?> FindAllByUserIdAsync(int userId)
        {
            return await _context.Like
                .Include(c => c.User)
                .Where(x => userId == x.UserId)
                .ToListAsync();
        }

        public async Task<Like> CreateAsync(Like like)
        {
            _context.Like.Add(like);
            await _context.SaveChangesAsync();
            return like;
        }

        public async Task<Like?> DeleteAsync(int userId, int postId)
        {
            var like = await FindByIdAsync(userId, postId);

            if (like != null)
            {
                _context.Remove(like);
                await _context.SaveChangesAsync();
            }

            return like;
        }
    }
}
