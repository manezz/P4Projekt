namespace WebApi.Repository
{
    public interface IPostRepository
    {
        Task<Post> CreatePostAsync(Post newPost);
        Task<Post> DeletePostAsync(int id);
        Task<Post> UpdatePostAsync(int id, Post updatePost);
        Task<Post> UpdatePostLikesAsync(int id, int like);
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetPostByPostIdAsync(int PostId);
        Task<List<Post?>> GetAllPostsByUserIdAsync(int UserId);


        Task<List<Tag>> GetAllTagsAsync();
        Task<Tag?> GetTagByIdAsync(int id);
        Task<List<Tag?>> GetTagsByPostIdAsync(int postId);
        Task<Tag?> CreateTagAsync(Tag newTag);
        Task<Tag?> UpdateTagAsync(Tag updateTag);
        Task<PostsTag> CreatePostTagAsync(PostsTag newPostsTag);
        Task<PostsTag> DeletePostTagAsync(PostsTag newPostsTag);
        Task<PostsTag> UpdatePostTagAsync(PostsTag newPostsTag);
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
            return await _context.Post.Include(c => c.User)
                 .OrderByDescending(d => d.Date)
                 .ToListAsync();
        }

        public async Task<Post?> GetPostByPostIdAsync(int postId)
        {
            return await _context.Post.Include(c => c.User).FirstOrDefaultAsync(x => postId == x.PostId);
        }

        public async Task<List<Post?>> GetAllPostsByUserIdAsync(int userId)
        {
            return await _context.Post.Include(c => c.User).Where(x => userId == x.UserId).ToListAsync();
        }

        public async Task<Post> CreatePostAsync(Post newPost)
        {
            _context.Post.Add(newPost);
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
            var tagId = from tag in _context.Tags
                        where tag.Name == updateTag.Name
                        select tag.TagId;

            //var postId = from posttag in _context.PostsTags
            //             where posttag.PostId == 

            if (tagId.Any()) //If tag exists but not in post, sets id to same as found tag
            {
                updateTag.TagId = await tagId.FirstOrDefaultAsync();
                return updateTag;
            }
            _context.Tags.Add(updateTag);
            await _context.SaveChangesAsync();

            updateTag = await GetTagByIdAsync(updateTag.TagId);
            return updateTag;
        }

        public async Task<PostsTag> CreatePostTagAsync(PostsTag postsTag)
        {
            _context.PostsTags.Add(postsTag);
            await _context.SaveChangesAsync();
            return postsTag;
        }

        public async Task<PostsTag> UpdatePostTagAsync(PostsTag postsTag)
        //public async Task<List<PostsTag>> UpdatePostTagAsync(PostsTag postsTag)
        {
            var postag2 = from posttag in _context.PostsTags
                         where posttag.PostId == postsTag.PostId
                         where posttag.TagId != postsTag.TagId
                         select posttag;

            var postag = await _context.PostsTags
                .Where(x => x.PostId == postsTag.PostId)
                .Where(x => x.TagId == postsTag.TagId)
                .Select(x => x)
                .ToListAsync();

            //var postag = await _context.PostsTags.FindAsync(postsTag.PostId, postsTag.TagId);

            //Console.Write("hej");
            //if (postag2.Any())
            //{
            //    //_context.PostsTags.Remove(postag2);
            //    //return postag;

            //    //_context.
            //    //_context.PostsTags.Where(i => i.TagId == tagId).delete
            //    //_context.Remove(postsTag.)
            //    //var posttagdelete = from posttag in _context.PostsTags
            //    //                    where posttag.PostId == postsTag.PostId
            //    //                    where posttag.TagId == postsTag.TagId
            //}
            _context.PostsTags.Add(postsTag);
            await _context.SaveChangesAsync();
            return postsTag;
        }

        public async Task<PostsTag> DeletePostTagAsync(PostsTag postsTag)
        {
            _context.PostsTags.Remove(postsTag);
            await _context.SaveChangesAsync();
            return postsTag;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _context.Tag.ToListAsync();
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
            return await _context.Tag.FindAsync(id);
            //return await _context.Tags.Include(p => p.Posts).FirstOrDefaultAsync(x => x.TagId == id);
        }
    }
}
