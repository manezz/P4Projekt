namespace WebApi.Repository
{
    public interface ITagRepository
    {
        // TAGS
        Task<List<Tag>> GetAllTagsAsync();
        Task<Tag?> GetTagByIdAsync(int id);
        Task<List<Tag?>> GetTagsByPostIdAsync(int postId);
        Task<Tag?> CreateTagAsync(Tag newTag);
        Task<Tag?> UpdateTagAsync(Tag updateTag);
    }

    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _context;
        public TagRepository(DatabaseContext context)
        {
            _context = context;
        }


        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _context.Tag.ToListAsync();
            //return await _context.Tags.Include(p => p.Posts).ToListAsync();
        }

        public async Task<List<Tag?>> GetTagsByPostIdAsync(int postId)
        {
            return await _context.PostTag
                .Include(p => p.Post)
                .Include(t => t.Tag)
                .Where(x => x.PostId == postId)
                .Select(x => x.Tag)
                .ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _context.Tag.FindAsync(id);
            //return await _context.Tags.Include(p => p.Posts).FirstOrDefaultAsync(x => x.TagId == id);
        }

        public async Task<Tag?> CreateTagAsync(Tag newTag)
        {
            //gets id from Tag entity with identical name property
            var tagId = from tag in _context.Tag
                        where tag.Name == newTag.Name
                        select tag.TagId;

            if (tagId.Any())//If tag exists but not in post, sets id to same as found tag
            {
                newTag.TagId = await tagId.FirstOrDefaultAsync();
                return newTag;
            }

            _context.Tag.Add(newTag);
            await _context.SaveChangesAsync();

            newTag = await GetTagByIdAsync(newTag.TagId);
            return newTag;
        }

        public async Task<Tag?> UpdateTagAsync(Tag updateTag)
        {
            //var tagl = await GetAllTagsAsync();

            //gets id from Tag entity with identical name property
            var tagId = from tag in _context.Tag
                        where tag.Name == updateTag.Name
                        select tag.TagId;

            //var postId = from posttag in _context.PostsTags
            //             where posttag.PostId == 

            if (tagId.Any()) //If tag exists but not in post, sets id to same as found tag
            {
                updateTag.TagId = await tagId.FirstOrDefaultAsync();
                return updateTag;
            }
            _context.Tag.Add(updateTag);
            await _context.SaveChangesAsync();

            updateTag = await GetTagByIdAsync(updateTag.TagId);
            return updateTag;
        }

    }
}
