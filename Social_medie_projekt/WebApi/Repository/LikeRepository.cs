namespace WebApi.Repository
{
    public interface ILikeRepository
    {
        Task<Liked> CreateLikeAsync(Liked newLike);
        Task<Liked> DeleteLikeAsync(Liked like);
    }

    public class LikeRepository : ILikeRepository
    {
        private readonly DatabaseContext _context;

        public LikeRepository(DatabaseContext context)
        {
            _context = context;
        }


        public async Task<Liked> CreateLikeAsync(Liked like)
        {
            _context.Liked.Add(like);

            await _context.SaveChangesAsync();
            return like;
        }

        public async Task<Liked> DeleteLikeAsync(Liked unLike)
        {
            _context.Liked.Remove(unLike);

            await _context.SaveChangesAsync();
            return unLike;
        }
    }
}
