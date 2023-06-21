namespace WebApi.Service
{
    public interface ILikeService
    {
        Task<LikeResponse?> FindByIdAsync(int userId, int postId);
        Task<List<LikeResponse>?> FindAllByUserIdAsync(int userId);
        Task<LikeResponse> CreateAsync(LikeRequest newLike);
        Task<LikeResponse?> DeleteAsync(int userId, int postId);
    }

    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IPostRepository _postRepository;

        public LikeService(ILikeRepository likeRepository, IPostRepository postRepository)
        {
            _likeRepository = likeRepository;
            _postRepository = postRepository;
        }

        private Like MapLikeRequestToLike(LikeRequest likedRequest)
        {
            return new Like
            {
                UserId = likedRequest.UserId,
                PostId = likedRequest.PostId,
            };
        }

        private LikeResponse MapLikeToLikeResponse(Like like)
        {
            return new LikeResponse
            {

                User = new LikeUserResponse
                {
                    UserId = like.UserId
                },

                Post = new LikePostResponse
                {
                    PostId = like.PostId
                },

            };
        }

        public async Task<LikeResponse?> FindByIdAsync(int userId, int postId)
        {
            var like = await _likeRepository.FindByIdAsync(userId, postId);

            if (like == null)
            {
                return null;
            }
            return MapLikeToLikeResponse(like);
        }

        public async Task<List<LikeResponse>?> FindAllByUserIdAsync(int userId)
        {
            List<Like>? likes = await _likeRepository.FindAllByUserIdAsync(userId);

            if (likes == null)
            {
                return null;
            }
            return likes.Select(like => MapLikeToLikeResponse(like)).ToList();
        }

        public async Task<LikeResponse> CreateAsync(LikeRequest newLike)
        {
            // Updates post to one more like
            var post = await _postRepository.UpdatePostLikesAsync(newLike.PostId, 1);

            if (post == null)
            {
                throw new ArgumentNullException("Post doesn't exist");
            }


            var like = await _likeRepository.CreateAsync(MapLikeRequestToLike(newLike));
            return MapLikeToLikeResponse(like);

        }

        public async Task<LikeResponse?> DeleteAsync(int userId, int postId)
        {
            // Deletes like
            var like = await _likeRepository.DeleteAsync(userId, postId);

            if (like == null)
            {
                throw new ArgumentNullException("like is null");
            }

            // Updates post to one less like
            var post = await _postRepository.UpdatePostLikesAsync(like.PostId, -1);

            if (post == null)
            {
                throw new ArgumentNullException("Post doesn't exist");
            }

            return MapLikeToLikeResponse(like);
        }
    }
}