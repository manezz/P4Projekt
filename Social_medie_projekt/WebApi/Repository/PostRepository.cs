namespace WebApi.Repository
{
    public interface IPostRepository
    {
        Task<Posts> CreatePostAsync(Posts newPost);
        Task<Posts?> DeletePostAsync(int id);
        Task<Posts?> UpdatePostAsync(int id, Posts updatePost);
        Task<Posts?> UpdatePostLikesAsync(int id, int like);
        Task<List<Posts>> GetAllPostsAsync();
        Task<Posts?> GetPostByPostIdAsync(int PostId);
        Task<List<Posts>> GetPostByUserIdAsync(int UserId);
        Task<Liked> CreateLikeAsync(Liked newLike);
        Task<Liked> DeleteLikeAsync(Liked like);
        Task<List<Tag>> GetAllTagsAsync();
        Task<Tag?> GetTagByIdAsync(int id);
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

        public async Task<Posts> CreatePostAsync(Posts newPost)
        {
            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();
            return newPost;
        }

        public async Task<Posts?> DeletePostAsync(int id)
        {
            var post = await GetPostByPostIdAsync(id);

            if (post != null)
            {
                _context.Remove(post);
                await _context.SaveChangesAsync();
            }
            return post;
        }

        public async Task<Posts?> UpdatePostAsync(int id, Posts updatePost)
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

        public async Task<Posts?> UpdatePostLikesAsync(int id, int like)
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

        public async Task<List<Posts>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(c => c.PostUser)
                 .OrderByDescending(d => d.Date)
                 .ToListAsync();
        }

        public async Task<Posts?> GetPostByPostIdAsync(int postId)
        {
            return await _context.Posts.Include(c => c.PostUser).FirstOrDefaultAsync(x => postId == x.PostId);
        }

        public async Task<List<Posts>> GetPostByUserIdAsync(int userId)
        {
            return await _context.Posts.Include(c => c.PostUser).Where(x => userId == x.UserId).ToListAsync();
        }

        public async Task<Liked> CreateLikeAsync(Liked newLike)
        {
            _context.Liked.Add(newLike);

            await _context.SaveChangesAsync();
            return newLike;
        }

        public async Task<Liked> DeleteLikeAsync(Liked like)
        {
            _context.Liked.Remove(like);

            await _context.SaveChangesAsync();
            return like;
        }

        public async Task<Tag?> CreateTagAsync(Tag newTag)
        {
            //gets id from Tag entity with identical name property
            var tagId = from tag in _context.Tags
                        where tag.Name == newTag.Name
                        select tag.TagId;

            if (tagId.Any())//If tag exists but not in post, sets id to same as found tag
            {
                newTag.TagId = await tagId.FirstOrDefaultAsync();
                return newTag;
            }

            _context.Tags.Add(newTag);
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
            return await _context.Tags.ToListAsync();
            //return await _context.Tags.Include(p => p.Posts).ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
            //return await _context.Tags.Include(p => p.Posts).FirstOrDefaultAsync(x => x.TagId == id);
        }
    }
}
