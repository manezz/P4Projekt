namespace WebApi.Repository
{
    public interface IPostRepository
    {
        Task<Posts> CreatePostAsync(Posts newPost);
        Task<Posts> DeletePostAsync(int id);
        Task<Posts> UpdatePostAsync(int id, Posts updatePost);
        Task<Posts> UpdatePostLikesAsync(int id, int like);
        Task<List<Posts>> GetAllAsync();
        Task<Posts?> GetPostByPostIdAsync(int PostId);
        Task<List<Posts?>> GetPostByUserIdAsync(int UserId);
        Task<Liked> CreateLikeAsync(Liked newLike);
        Task<Liked> DeleteLikeAsync(Liked like);
    }

    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext _context;

        public PostRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _context.Posts.Include(c => c.User)
                 .OrderByDescending(d => d.Date)
                 .ToListAsync();
        }

        public async Task<Post?> GetPostByPostIdAsync(int postId)
        {
            return await _context.Posts.Include(c => c.User).FirstOrDefaultAsync(x => postId == x.PostId);
        }

        public async Task<List<Post?>> GetAllPostsByUserIdAsync(int userId)
        {
            return await _context.Posts.Include(c => c.User).Where(x => userId == x.UserId).ToListAsync();
        }

        public async Task<Post> CreatePostAsync(Post newPost)
        {
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            return newPost;
        }

        public async Task<Post> UpdatePostAsync(int id, Post updatePost)
        {
            var post = await GetPostByPostIdAsync(id);

            if (post != null)
            {
                post.Title = updatePost.Title;
                post.Desc = updatePost.Desc;
                //post.Tags = updatePost.Tags;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            return post;
        }       

        public async Task<Post> DeletePostAsync(int id)
        {
            var post = await GetPostByPostIdAsync(id);

            if(post != null)
            {
                _context.Remove(post);
                await _context.SaveChangesAsync();
            }
            return post;
        }



        // For updating each post that has/needs a like from likeService
        public async Task<Post> UpdatePostLikesAsync(int id, int like)
        {
            var post = await GetPostByPostIdAsync(id);

            if (post != null)
            {
                post.Likes += like;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            return post;
        }

        public async Task<Tag?> CreateTagAsync(Tag newTag)
        {
            var tagId = from tag in _context.Tags
                        where tag.Name == newTag.Name
                        select tag.TagId;

            if (tagId.Any())
            {
                newTag.TagId = await tagId.FirstOrDefaultAsync();
                return newTag;
            }

            _context.Tags.Add(newTag);
            await _context.SaveChangesAsync();

            newTag = await GetTagByIdAsync(newTag.TagId);
            return newTag;
        }

        public async Task<PostsTag> CreatePostTagAsync(PostsTag postsTag)
        {
            _context.PostsTags.Add(postsTag);
            await _context.SaveChangesAsync();
            return postsTag;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
            //return await _context.Tags.Include(p => p.Posts).ToListAsync();
        }

        public async Task<List<Tag?>> GetTagsByPostIdAsync(int postId)
        {
            return await _context.PostsTags
                .Include(p => p.Posts)
                .Include(t => t.Tag)
                .Where(x => x.PostId == postId)
                .Select(x => x.Tag)
                .ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
            //return await _context.Tags.Include(p => p.Posts).FirstOrDefaultAsync(x => x.TagId == id);
        }
    }
}
