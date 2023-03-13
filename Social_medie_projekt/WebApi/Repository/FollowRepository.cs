namespace WebApi.Repository
{
    public interface IFollowRepository
    {
        Task<Follow> Follow(Follow newFollow);
        Task<Follow> Unfollow(int followerId, int followingId);
        Task<Follow> FindFollow(int followerId, int followingId);
        Task<List<Follow>> FindUsersFollowing(int followingId);
        Task<List<Follow>> FindUsersFollowers(int followerId);

    }

    public class FollowRepository : IFollowRepository
    {
        private readonly DatabaseContext _context;


        public FollowRepository(DatabaseContext context)
        {
            _context = context;
        }

        // Creates a new follow relationship
        public async Task<Follow> Follow(Follow newFollow)
        {
            if (await FindFollow(newFollow.UserId, newFollow.FollowingId) != null)
            {
                throw new Exception("User already followed");
            }

            _context.Follow.Add(newFollow);
            await _context.SaveChangesAsync();

            return newFollow;
        }

        // Deletes an existing follow relationship
        public async Task<Follow> Unfollow(int followerId, int followingId)
        {
            var follow = await FindFollow(followerId, followingId);

            if (follow != null)
            {
                _context.Remove(follow);
                await _context.SaveChangesAsync();
            }

            return follow;
        }



        // Finds a specific follow relationship
        public async Task<Follow> FindFollow(int followerId, int followingId)
        {
            return await _context.Follow.Where(x => followerId == x.UserId).Where(y => followingId == y.FollowingId).FirstOrDefaultAsync();
        }





        // Find all users that userId follows
        public async Task<List<Follow>> FindUsersFollowing(int userId)
        {
            return await _context.Follow.Where(x => userId == x.UserId).ToListAsync();
        }



        // Find all FollowingId who follows user
        public async Task<List<Follow>> FindUsersFollowers(int followingId)
        {
            return await _context.Follow.Where(x => followingId == x.FollowingId).ToListAsync();
        }

    }
}
