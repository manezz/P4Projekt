namespace WebApi.Service
{
    public interface ILikeService
    {
        Task<LikeResponse> CreateLikeAsync(LikeRequest newLike);
        Task<LikeResponse?> DeleteLikeAsync(LikeRequest deleteLike);
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



        private Liked MapLikeRequestToLike(LikeRequest likedRequest)
        {
            return new Liked
            {
                UserId = likedRequest.UserId,
                PostId = likedRequest.PostId,
                IsLiked = likedRequest.IsLiked,
            };
        }

        private LikeResponse MapLikeToLikeResponse(Liked like)
        {
            return new LikeResponse
            {
                UserId = like.UserId,
                PostId = like.PostId,
                IsLiked = like.IsLiked,
            };
        }





        public async Task<LikeResponse> CreateLikeAsync(LikeRequest newLike)
        {
            var like = await _likeRepository.CreateLikeAsync(MapLikeRequestToLike(newLike));

            // Updates post to one more like
            var post = await _postRepository.UpdatePostLikesAsync(newLike.PostId, 1);

            if (post == null)
            {
                throw new ArgumentNullException();
            }

            return MapLikeToLikeResponse(like);
        }

        public async Task<LikeResponse?> DeleteLikeAsync(LikeRequest deleteLike)
        {
            var like = await _likeRepository.DeleteLikeAsync(MapLikeRequestToLike(deleteLike));

            // Updates post to one less like
            var post = await _postRepository.UpdatePostLikesAsync(deleteLike.PostId, -1);

            if (like == null || post == null)
            {
                throw new ArgumentNullException();
            }

            return MapLikeToLikeResponse(like);
        }
    }
}
