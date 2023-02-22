namespace WebApi.Repository
{
    public interface ILikeRepository
    {
        Task<Like?> CheckLike(int KeyId);
        Task<List<Like>?> GetAllLikesFromUser(int userId);
        Task<Like> CreateLikeAsync(Like newLike);
        Task<Like> DeleteLikeAsync(int keyId);
    }

    public class LikeRepository : ILikeRepository
    {
        private readonly DatabaseContext _context;

        public LikeRepository(DatabaseContext context)
        {
            _context = context;
        }


        public async Task<Like?> CheckLike(int KeyId)
        {
            return await _context.Like.FirstOrDefaultAsync(x => KeyId == x.KeyId);
        }

        public async Task<List<Like>?> GetAllLikesFromUser(int userId)
        {
            return await _context.Like.Include(c => c.User).Where(x => userId == x.UserId).ToListAsync();
        }

        public async Task<Like> CreateLikeAsync(Like like)
        {

            if(CheckLike(like.KeyId) != null)
            {
                _context.Like.Add(like);
                await _context.SaveChangesAsync();
            }

            return like;
        }

        public async Task<Like> DeleteLikeAsync(int keyId)
        {
            var like = await CheckLike(keyId);

            if (like != null)
            {
                _context.Remove(like);
                await _context.SaveChangesAsync();
            }

            return like;
        }
    }
}
