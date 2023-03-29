namespace WebApi.Service
{
    public interface IFollowService
    {
        Task<FollowResponse> Follow(FollowRequest newFollow);
        Task<FollowResponse> Unfollow(int userId, int followingId);
        Task<FollowResponse> FindFollow(int userId, int followingId);
        Task<List<FollowResponse>> FindUsersFollowing(int followingId);
        Task<List<FollowResponse>> FindUsersFollowers(int followerId);
    }

    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;

        public FollowService(IFollowRepository followRepository)
        {
            _followRepository = followRepository;
        }

        private static Follow MapFollowRequestToFollow(FollowRequest followRequest)
        {
            return new Follow
            {
                UserId = followRequest.UserId,
                FollowingId = followRequest.FollowingId

            };
        }

        private static FollowResponse MapFollowToFollowResponse(Follow follow)
        {
            return new FollowResponse
            {
                UserId = follow.UserId,
                FollowingId = follow.FollowingId,
            };
        }

        // Creates a new follow relationship
        public async Task<FollowResponse> Follow(FollowRequest newFollow)
        {
            var follow = await _followRepository.Follow(MapFollowRequestToFollow(newFollow));

            if (follow == null)
            {
                throw new ArgumentNullException();
            }

            return MapFollowToFollowResponse(follow);
        }

        // Deletes an existing follow relationship
        public async Task<FollowResponse> Unfollow(int userId, int followingId)
        {
            var follow = await _followRepository.Unfollow(userId, followingId);

            if (follow == null)
            {
                throw new ArgumentNullException();
            }

            return MapFollowToFollowResponse(follow);
        }

        // Finds a specific follow relationship
        public async Task<FollowResponse> FindFollow(int userId, int followingId)
        {
            var follow = await _followRepository.FindFollow(userId, followingId);

            if (follow == null)
            {
                throw new ArgumentNullException();
            }

            return MapFollowToFollowResponse(follow);
        }

        // Find all who follows a user
        public async Task<List<FollowResponse>> FindUsersFollowers(int followingId)
        {
            List<Follow> follow = await _followRepository.FindUsersFollowers(followingId);

            if (follow == null)
            {
                throw new ArgumentNullException();
            }

            return follow.Select(follow => MapFollowToFollowResponse(follow)).ToList();
        }

        // Find all follows from a user
        public async Task<List<FollowResponse>> FindUsersFollowing(int userId)
        {
            List<Follow> follow = await _followRepository.FindUsersFollowing(userId);

            if (follow == null)
            {
                throw new ArgumentNullException();
            }

            return follow.Select(follow => MapFollowToFollowResponse(follow)).ToList();
        }
    }
}
