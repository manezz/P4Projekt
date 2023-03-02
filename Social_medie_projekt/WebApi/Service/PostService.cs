namespace WebApi.Service
{
    public interface IPostService
    {
        // Post
        Task<List<PostResponse>> GetAllPostsAsync();
        Task<PostResponse?> GetPostByPostIdAsync(int postId);
        Task<List<PostResponse?>> GetAllPostsByUserIdAsync(int userId);
        Task<PostResponse> CreatePostAsync(PostRequest newPost);
        Task<PostResponse> UpdatePostAsync(int postId, PostUpdateRequest updatePost);
        Task<PostResponse?> DeletePostAsync(int postId);



        //// Tag
        //Task<List<TagResponse>> GetAllTagsAsync();
        //Task<TagResponse?> GetTagById(int Id);
        //Task<List<TagResponse>> GetTagsByPostIdAsync(int postId);
        //Task<TagResponse> CreateTagAsync(TagRequest newTag);
        //Task<TagResponse> UpdateTagAsync(TagRequest newTag);



        // PostTag
        Task<List<PostTagResponse>> GetPostTagsByPostIdAsync(int postId);
        Task<PostTagResponse> CreatePostTagAsync(int postId, int tagId);
        Task<PostTagResponse> UpdatePostTagByPostIdAsync(int postId, int tagId);
        Task<PostTagResponse> DeletePostTagByPostIdAsync(int postId, int tagId);
    }

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ITagService _tagService;
        public PostService(IPostRepository postRepository, ITagRepository tagRepository, ITagService tagService)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _tagService = tagService;
        }


        // With tag list
        private static PostResponse MapPostToPostResponse(Post post, List<TagResponse> tags)
        {
            return new PostResponse
            {
                PostId = post.PostId,
                Title = post.Title,
                Desc = post.Desc,
                Date = post.Date,
                Likes = post.Likes,
                User = new PostUserResponse
                {
                    UserId = post.User.UserId,
                    UserName = post.User.UserName,
                },
                Tags = tags
            };
        }

        private static PostResponse MapPostToPostResponse(Post post, List<Tag> tags)
        {
            return new PostResponse
            {
                PostId = post.PostId,
                Title = post.Title,
                Desc = post.Desc,
                Date = post.Date,
                Likes = post.Likes,
                User = new PostUserResponse
                {
                    UserId = post.User.UserId,
                    UserName = post.User.UserName,
                },
                Tags = tags.Select(x => new TagResponse
                {
                    TagId = x.TagId,
                    Name = x.Name,
                })
                .ToList()
            };
        }

        // Without tags
        private static PostResponse MapPostToPostResponse(Post post)
        {
            return new PostResponse
            {
                PostId = post.PostId,
                Title = post.Title,
                Desc = post.Desc,
                Date = post.Date,
                Likes = post.Likes,
                User = new PostUserResponse
                {
                    UserId = post.User.UserId,
                    UserName = post.User.UserName,
                }
            };
        }


        private Post MapPostRequestToPost(PostRequest postRequest)
        {
            return new Post
            {
                UserId = postRequest.UserId,
                Title = postRequest.Title,
                Desc = postRequest.Desc,
                //Tags = List<postRequest>.Tags,
            };
        }

        private Post MapPostUpdateRequestToPost(PostUpdateRequest postUpdateRequest)
        {
            return new Post
            {
                Title = postUpdateRequest.Title,
                Desc = postUpdateRequest.Desc,
            };
        }
              







        // ------------- POSTTAG RES/REQ --------------------

        private PostTagResponse MapPostTagToPostTagResponse(PostTag postsTag)
        {
            return new PostTagResponse
            {
                PostId = postsTag.PostId,
                TagId = postsTag.TagId,
            };
        }

        //New mapping
        private static PostTag MapPostTagResponseToPostTag(PostTagResponse postTagResponse)
        {
            return new PostTag
            {
                PostId = postTagResponse.PostId,
                TagId = postTagResponse.TagId,
            };
        }

        // change name
        private static PostTag MapPostTagRequestToPostTag(int postId, int tagId)
        {
            return new PostTag
            {
                PostId = postId,
                TagId = tagId,
            };
        }

    


        public async Task<List<PostResponse>> GetAllPostsAsync()
        {
            List<Post> posts = await _postRepository.GetAllAsync();

            if (posts == null)
            {
                throw new ArgumentNullException();
            }

            return posts.Select(post => MapPostToPostResponse(post,_tagRepository.GetTagsByPostIdAsync(post.PostId).Result)).ToList();
        }

        public async Task<PostResponse?> GetPostByPostIdAsync(int postId)
        {
            var post = await _postRepository.GetPostByPostIdAsync(postId);

            if (post != null)
            {
                return MapPostToPostResponse(post, await _tagRepository.GetTagsByPostIdAsync(postId));
            }

                return null;
        }        

        public async Task<List<PostResponse?>> GetAllPostsByUserIdAsync(int userId)
        {
            List<Post?> post = await _postRepository.GetAllPostsByUserIdAsync(userId);

            if (post == null)
            {
                throw new ArgumentNullException();
            }

            return post.Select(post => MapPostToPostResponse(post, _tagRepository.GetTagsByPostIdAsync(post.PostId).Result)).ToList();
        }

        public async Task<PostResponse> CreatePostAsync(PostRequest newPost)
        {
            var post = await _postRepository.CreatePostAsync(MapPostRequestToPost(newPost));
            if (post == null)
            {
                throw new ArgumentNullException();
            }

            var tags = newPost.Tags.Select(tag => _tagService.CreateTagAsync(tag).Result).ToList();

            _ = tags.Select(tagResponse => CreatePostTagAsync(post.PostId, tagResponse.TagId).Result).ToList();

            return MapPostToPostResponse(post, tags);

        }

        public async Task<PostResponse> UpdatePostAsync(int postId, PostUpdateRequest updatePost)
        {
            var oldtags = await _tagRepository.GetTagsByPostIdAsync(postId);

            if (oldtags == null)
            {
                throw new ArgumentNullException();
            }

            var updateTags = updatePost.Tags
                .Select(x => TagService.MapTagRequestToTag(x))
                .ToList();

            var post = await _postRepository.UpdatePostAsync(postId, MapPostUpdateRequestToPost(updatePost));
            var tags = updatePost.Tags.Select(tag => _tagService.UpdateTagAsync(tag).Result).ToList();

            if (post != null)
            {
                throw new ArgumentNullException();
            }

            var tagCreate = updateTags
                .Where(x => !(oldtags.Select(z => z.Name))
                .Contains(x.Name))
                .ToList();

            var tagDelete = oldtags
                .Where(x => !(updateTags.Select(z => z.Name))
                .Contains(x.Name))
                .ToList();

            var tagsDeleted = tagDelete
                .Select(x => DeletePostTagByPostIdAsync(postId, x.TagId).Result)
                .ToList();

            var tagsCreated = tagCreate
                .Select(x => _tagRepository.CreateTagAsync(x).Result)
                .ToList();

            _ = tagsCreated
                .Select(x => CreatePostTagAsync(post.PostId, x.TagId).Result)
                .ToList();

            var currentTags = await _tagRepository.GetTagsByPostIdAsync(postId);

            if (currentTags == null)
            {
                throw new ArgumentNullException();
            }

            return MapPostToPostResponse(post, currentTags);
        }

        public async Task<PostResponse?> DeletePostAsync(int postId)
        {
            var post = await _postRepository.DeletePostAsync(postId);
            if (post == null)
            {
                return null;
            }
            return MapPostToPostResponse(post);
        }
        



        // -------------------------    POST-TAG

        public async Task<List<PostTagResponse>> GetPostTagsByPostIdAsync(int postId)
        {
            var postTags = await _postRepository.GetPostTagsByPostId(postId);

            if (postTags == null)
            {
                throw new ArgumentNullException();
            }

            return postTags.Select(posttag => MapPostTagToPostTagResponse(posttag)).ToList();
        }

        public async Task<PostTagResponse> CreatePostTagAsync(int postId, int tagId)
        {
            var postTag = await _postRepository.CreatePostTagAsync(MapPostTagRequestToPostTag(postId, tagId));

            if (postTag == null)
            {
                throw new ArgumentNullException();
            }

            return MapPostTagToPostTagResponse(postTag);
        }

        public async Task<PostTagResponse> UpdatePostTagByPostIdAsync(int postId, int tagId)
        {

            var postTag = await _postRepository.UpdatePostTagAsync(MapPostTagRequestToPostTag(postId, tagId));

            if (postTag == null)
            {
                throw new ArgumentNullException();
            }


            return MapPostTagToPostTagResponse(postTag);
        }

        public async Task<PostTagResponse> DeletePostTagByPostIdAsync(int postId, int tagId)
        {
            var postTag = await _postRepository.DeletePostTagAsync(MapPostTagRequestToPostTag(postId, tagId));

            if (postTag == null)
            {
                throw new ArgumentNullException();
            }

            return MapPostTagToPostTagResponse(postTag);
        }

    }
}
