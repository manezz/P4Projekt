namespace WebApi.Service
{
    public interface IFollowService
    {
        Task<FollowResponse> CreateAsync(FollowRequest newFollow);
        Task<FollowResponse> DeleteAsync(int userId, int followingId);
        Task<FollowResponse?> FindByIdAsync(int userId, int followingId);
        Task<List<FollowResponse>> FindAllByUserIdAsync(int userId);
        Task<List<FollowResponse>> FindAllByFollowingIdAsync(int followerId);
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
        public async Task<FollowResponse> CreateAsync(FollowRequest newFollow)
        {
            var follow = await _followRepository.CreateAsync(MapFollowRequestToFollow(newFollow));

            if (follow == null)
            {
                throw new ArgumentNullException();
            }

            return MapFollowToFollowResponse(follow);
        }

        // Deletes an existing follow relationship
        public async Task<FollowResponse> DeleteAsync(int userId, int followingId)
        {
            var follow = await _followRepository.DeleteAsync(userId, followingId);

            if (follow == null)
            {
                throw new ArgumentNullException();
            }

            return MapFollowToFollowResponse(follow);
        }

        // Finds a specific follow relationship
        public async Task<FollowResponse?> FindByIdAsync(int userId, int followingId)
        {
            var follow = await _followRepository.FindByIdAsync(userId, followingId);

            if (follow == null)
            {
                return null;
            }

            return MapFollowToFollowResponse(follow);
        }

        // Find all who follows a user
        public async Task<List<FollowResponse>> FindAllByFollowingIdAsync(int followingId)
        {
            List<Follow> follow = await _followRepository.FindAllByFollowingIdAsync(followingId);

            if (follow == null)
            {
                throw new ArgumentNullException();
            }

            return follow.Select(follow => MapFollowToFollowResponse(follow)).ToList();
        }

        // Find all follows from a user
        public async Task<List<FollowResponse>> FindAllByUserIdAsync(int userId)
        {
            List<Follow> follow = await _followRepository.FindAllByUserIdAsync(userId);

            if (follow == null)
            {
                throw new ArgumentNullException();
            }

            return follow.Select(follow => MapFollowToFollowResponse(follow)).ToList();
        }
    }
}
