namespace WebApi.Repository
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllAsync();
        Task<Tag?> GetTagByIdAsync(int id);
        Task<Tag> CreateTagAsync(Tag newTags);

    }

    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _context;

        public TagRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Tag> CreateTagAsync(Tag newTags)
        {
            _context.Tags.Add(newTags);
            await _context.SaveChangesAsync();
            newTags = await GetTagByIdAsync(newTags.TagId);
            return newTags;
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _context.Tags.Include(p => p.posts).ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _context.Tags.Include(p => p.posts).FirstOrDefaultAsync(x => x.TagId == id);
        }
    }
}
