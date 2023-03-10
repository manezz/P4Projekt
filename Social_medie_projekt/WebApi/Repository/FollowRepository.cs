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
            if (await FindFollow(newFollow.FollowerUserId, newFollow.FollowingUserId) != null)
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
            return await _context.Follow.Where(x => followerId == x.FollowerUserId).Where(y => followingId == y.FollowingUserId).FirstOrDefaultAsync();
        }





        // Find all users that follows the user
        public async Task<List<Follow>> FindUsersFollowers(int followerId)
        {
            return await _context.Follow.Where(x => followerId == x.FollowerUserId).ToListAsync();
        }

        // Find all users that a user follows
        public async Task<List<Follow>> FindUsersFollowing(int followingId)
        {
            return await _context.Follow.Where(x => followingId == x.FollowingUserId).ToListAsync();
        }
    }
}
