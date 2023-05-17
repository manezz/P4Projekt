namespace WebApi.Repository
{
    public interface IFollowRepository
    {
        Task<Follow> CreateAsync(Follow newFollow);
        Task<Follow> DeleteAsync(int userId, int followingId);
        Task<Follow?> FindByIdAsync(int userId, int followingId);
        Task<List<Follow>> FindAllByUserIdAsync(int userId);
        Task<List<Follow>> FindByFollowingIdAsync(int followerId);
    }

    public class FollowRepository : IFollowRepository
    {
        private readonly DatabaseContext _context;
        public FollowRepository(DatabaseContext context)
        {
            _context = context;
        }

        // Creates a new follow relationship
        public async Task<Follow> CreateAsync(Follow newFollow)
        {
            if (await FindByIdAsync(newFollow.UserId, newFollow.FollowingId) != null)
            {
                throw new Exception("User already followed");
            }

            _context.Follow.Add(newFollow);
            await _context.SaveChangesAsync();

            return newFollow;
        }

        // Deletes an existing follow relationship
        public async Task<Follow> DeleteAsync(int userId, int followingId)
        {
            var follow = await FindByIdAsync(userId, followingId);

            if (follow != null)
            {
                _context.Remove(follow);
                await _context.SaveChangesAsync();
            }

            return follow;
        }

        // Finds a specific follow relationship
        public async Task<Follow?> FindByIdAsync(int userId, int followingId)
        {
            return await _context.Follow.Where(x => userId == x.UserId).Where(y => followingId == y.FollowingId).FirstOrDefaultAsync();
        }

        // Find all users that userId follows
        public async Task<List<Follow>> FindAllByUserIdAsync(int userId)
        {
            return await _context.Follow.Where(x => userId == x.UserId).ToListAsync();
        }

        // Find all FollowingId who follows user
        public async Task<List<Follow>> FindByFollowingIdAsync(int followingId)
        {
            return await _context.Follow.Where(x => followingId == x.FollowingId).ToListAsync();
        }
    }
}