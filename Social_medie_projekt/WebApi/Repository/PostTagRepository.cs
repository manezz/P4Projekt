namespace WebApi.Repository
{
    public interface IPostTagRepository
    {
        Task<List<PostTag>> FindAllByPostIdAsync(int postId);
        Task<PostTag?> FindByIdAsync(int postId, int tagId);
        Task<PostTag> CreateAsync(PostTag newPostsTag);
        Task<PostTag> UpdateAsync(PostTag newPostTag);
        Task<PostTag?> DeleteAsync(int postId, int tagId);
    }

    public class PostTagRepository : IPostTagRepository
    {
        private readonly DatabaseContext _context;

        public PostTagRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<PostTag>> FindAllByPostIdAsync(int postId)
        {
            return await _context.PostTag
                .Include(p => p.Post)
                .Include(t => t.Tag)
                .Where(p => p.PostId == postId)
                .Select(p => p)
                .ToListAsync();
        }

        public async Task<PostTag?> FindByIdAsync(int postId, int tagId)
        {
            return await _context.PostTag.FindAsync(postId, tagId);
        }

        public async Task<PostTag> CreateAsync(PostTag postTag)
        {
            _context.PostTag.Add(postTag);
            await _context.SaveChangesAsync();
            return postTag;
        }

        public async Task<PostTag> UpdateAsync(PostTag postsTag)
        {
            var postag2 = from posttag in _context.PostTag
                          where posttag.PostId == postsTag.PostId
                          where posttag.TagId != postsTag.TagId
                          select posttag;

            var postag = await _context.PostTag
                .Where(x => x.PostId == postsTag.PostId)
                .Where(x => x.TagId == postsTag.TagId)
                .Select(x => x)
            .ToListAsync();

            _context.PostTag.Add(postsTag);
            await _context.SaveChangesAsync();
            return postsTag;
        }

        public async Task<PostTag?> DeleteAsync(int postId, int tagId)
        {
            var postTag = await FindByIdAsync(postId, tagId);

            if (postTag != null)
            {
                _context.Remove(postTag);
                await _context.SaveChangesAsync();
            }

            return postTag;
        }
    }
}
