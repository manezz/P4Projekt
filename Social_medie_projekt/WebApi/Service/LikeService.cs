namespace WebApi.Service
{
    public interface ILikeService
    {
        Task<LikeResponse?> CheckLike(int KeyId);
        Task<LikeResponse> CreateLikeAsync(LikeRequest newLike);
        Task<LikeResponse?> DeleteLikeAsync(int keyId);
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
                //IsLiked = likedRequest.IsLiked,
            };
        }

        private LikeResponse MapLikeToLikeResponse(Like like)
        {
            return new LikeResponse
            {
                UserId = like.UserId,
                PostId = like.PostId,
                //IsLiked = like.IsLiked,
            };
        }



        public async Task<LikeResponse?> CheckLike(int KeyId)
        {
            var like = await _likeRepository.CheckLike(KeyId);

            if (like == null)
            {
                return null;
            }
            return MapLikeToLikeResponse(like);
        }

        public async Task<List<LikeResponse?>> GetAllUsersLikes(int userId)
        {
            var likes = await _likeRepository.GetAllUsersLikes(userId);

            if (likes == null)
            {
                return null;
            }
            return likes.Select(like => MapLikeToLikeResponse(like)).ToList();
        }

        public async Task<LikeResponse> CreateLikeAsync(LikeRequest newLike)
        {
            // Updates post to one more like
            var post = await _postRepository.UpdatePostLikesAsync(newLike.PostId, 1);

            if (post == null){
                throw new ArgumentNullException("Post doesn't exist");
            }
            else{
                var like = await _likeRepository.CreateLikeAsync(MapLikeRequestToLike(newLike));
                return MapLikeToLikeResponse(like);
            }
        }

        public async Task<LikeResponse?> DeleteLikeAsync(int keyId)
        {
            // Deletes like
            var like = await _likeRepository.DeleteLikeAsync(keyId);
    
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