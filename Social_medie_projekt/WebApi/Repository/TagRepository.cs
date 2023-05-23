namespace WebApi.Repository
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllAsync();
        Task<Tag?> FindByIdAsync(int tagId);
        Task<List<Tag>?> FindAllByPostIdAsync(int postId);
        Task<Tag?> CreateAsync(Tag newTag);
        Task<Tag?> UpdateAsync(Tag updateTag);
    }

    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _context;
        public TagRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _context.Tag.ToListAsync();
        }

        public async Task<List<Tag>?> FindAllByPostIdAsync(int postId)
        {
            return await _context.PostTag
                .Include(p => p.Post)
                .Include(t => t.Tag)
                .Where(x => x.PostId == postId)
                .Select(x => x.Tag)
                .ToListAsync();

            //return await _context.Tag
            //    .Include(p => p.Posts)
        }

        public async Task<Tag?> FindByIdAsync(int id)
        {
            return await _context.Tag.FindAsync(id);
        }

        public async Task<Tag?> CreateAsync(Tag newTag)
        {
            // Gets id from Tag entity with identical name property
            var tagId = from tag in _context.Tag
                        where tag.Name == newTag.Name
                        select tag.TagId;

            // If tag exists but not in post, sets id to same as found tag
            if (tagId.Any())
            {
                newTag.TagId = await tagId.FirstOrDefaultAsync();
                return newTag;
            }

            _context.Tag.Add(newTag);
            await _context.SaveChangesAsync();

            // Returns the tag with the id that was just created
            var returnTag = await FindByIdAsync(newTag.TagId);
            return returnTag;
        }

        public async Task<Tag?> UpdateAsync(Tag updateTag)
        {
            // Gets id from Tag entity with identical name property
            var tagId = from tag in _context.Tag
                        where tag.Name == updateTag.Name
                        select tag.TagId;

            //If tag exists but not in post, sets id to same as found tag
            if (tagId.Any())
            {
                updateTag.TagId = await tagId.FirstOrDefaultAsync();
                return updateTag;
            }

            _context.Tag.Add(updateTag);
            await _context.SaveChangesAsync();

            // Returns the tag with the id that was just created
            var returnTag = await FindByIdAsync(updateTag.TagId);
            return returnTag;
        }

    }
}
