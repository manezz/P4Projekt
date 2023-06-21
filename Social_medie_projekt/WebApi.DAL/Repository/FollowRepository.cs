namespace WebApi.DAL.Repository
{
    public interface IFollowRepository
    {
        Task<Follow> CreateAsync(Follow newFollow);
        Task<Follow?> DeleteAsync(int userId, int followingUserId);
        Task<Follow?> FindByIdAsync(int userId, int followingUserId);
        Task<List<Follow>> FindAllByUserIdAsync(int userId);
        Task<List<Follow>> FindAllByFollowingUserIdAsync(int followerId);
    }

    public class FollowRepository : IFollowRepository
    {
        private readonly DatabaseContext _context;
        public FollowRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Follow> CreateAsync(Follow newFollow)
        {
            _context.Follow.Add(newFollow);
            await _context.SaveChangesAsync();
            return newFollow;
        }

        public async Task<Follow?> DeleteAsync(int userId, int followingId)
        {
            var follow = await FindByIdAsync(userId, followingId);

            if (follow != null)
            {
                _context.Remove(follow);
                await _context.SaveChangesAsync();
            }

            return follow;
        }

        public async Task<Follow?> FindByIdAsync(int userId, int followingUserId)
        {
            return await _context.Follow
                .FindAsync(userId, followingUserId);
        }

        public async Task<List<Follow>> FindAllByUserIdAsync(int userId)
        {
            return await _context.Follow
                .Where(x => userId == x.UserId)
                .ToListAsync();
        }

        public async Task<List<Follow>> FindAllByFollowingUserIdAsync(int followingUserId)
        {
            return await _context.Follow
                .Where(x => followingUserId == x.FollowingUserId)
                .ToListAsync();
        }
    }
}