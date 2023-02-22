namespace WebApi.Service
{
    public interface IPostService
    {
        Task<List<PostResponse>> GetAllPostsAsync();
        Task<PostResponse?> GetPostByPostIdAsync(int Id);
        Task<List<PostResponse?>> GetAllPostsByUserIdAsync(int Id);
        Task<PostResponse> CreatePostAsync(PostRequest newPost);
        Task<PostResponse?> UpdatePostAsync(int postId, PostUpdateRequest updatePost);
        Task<PostResponse?> DeletePostAsync(int postId);
        Task<List<TagResponse>> GetAllTagsAsync();
        Task<TagResponse?> GetTagById(int Id);
        Task<TagResponse> CreateTagAsync(TagRequest newTag);
        Task<PostTagResponse> CreatePostTagAsync(int postId, int tagId);
        Task<List<TagResponse>> GetTagsByPostIdAsync(int postId);
    }

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
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

        private PostResponse MapPostToPostResponse(Post post)
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
                    UserName = post.User.UserName,
                },
                //Tags = tags
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
              

        




        public async Task<List<PostResponse>> GetAllPostsAsync()
        {
            List<Post> posts = await _postRepository.GetAllAsync();

            if (posts == null)
            {
                throw new ArgumentNullException();
            }

            return posts.Select(post => MapPostToPostResponse(post/*, GetTagsByPostIdAsync(post.PostId).Result*/)).ToList();
        }

        public async Task<PostResponse?> GetPostByPostIdAsync(int postId)
        {
            var post = await _postRepository.GetPostByPostIdAsync(postId);

            if (post == null)
            {
                return null;
            }

            return MapPostToPostResponse(post);
        }        

        public async Task<List<PostResponse?>> GetAllPostsByUserIdAsync(int userId)
        {
            List<Post?> posts = await _postRepository.GetAllPostsByUserIdAsync(userId);

            if (posts == null)
            {
                throw new ArgumentNullException();
            }

            return posts.Select(post => MapPostToPostResponse(post/*, GetTagsByPostIdAsync(post.PostId).Result*/)).ToList();
        }

        public async Task<PostResponse> CreatePostAsync(PostRequest newPost)
        {
            var post = await _postRepository.CreatePostAsync(MapPostRequestToPost(newPost));

            if(post == null)
            {
                throw new ArgumentNullException();
            }

            return MapPostToPostResponse(post);

        }

        public async Task<PostResponse?> UpdatePostAsync(int postId, PostUpdateRequest updatePost)
        {
            var post = await _postRepository.UpdatePostAsync(postId, MapPostUpdateRequestToPost(updatePost));

            if(post != null)
            {
                return MapPostToPostResponse(post);
            }
            return null;
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




        // --------- TAGS --------- //

        public async Task<PostTagResponse> CreatePostTagAsync(int postId, int tagId)
        {
            var postTag = await _postRepository.CreatePostTagAsync(MapPostTagRequestToPostTag(postId, tagId));

            if (postTag == null)
            {
                throw new ArgumentNullException();
            }

            return MapPostTagToPostTagResponse(postTag);
        }

        private PostTagResponse MapPostTagToPostTagResponse(PostsTag postsTag)
        {
            return new PostTagResponse
            {
                PostId = postsTag.PostId,
                TagId = postsTag.TagId,
            };
        }

        private static PostsTag MapPostTagRequestToPostTag(int postId, int tagId)
        {
            return new PostsTag
            {
                PostId = postId,
                TagId = tagId,
            };
        }



        private static TagResponse MapTagToTagResponse(Tag tag)
        {
            return new TagResponse
            {
                TagId = tag.TagId,
                Name = tag.Name,
            };
        }

        private static Tag MapTagRequestToTag(TagRequest tagRequest)
        {
            return new Tag
            {
                Name = tagRequest.Name
            };
        }

        public async Task<TagResponse> CreateTagAsync(TagRequest newTag)
        {
            var tag = await _postRepository.CreateTagAsync(MapTagRequestToTag(newTag));

            if (tag == null)
            {
                throw new ArgumentNullException();
            }
            return MapTagToTagResponse(tag);
        }

        public async Task<List<TagResponse>> GetAllTagsAsync()
        {
            List<Tag> tags = await _postRepository.GetAllTagsAsync();

            if (tags == null)
            {
                throw new ArgumentNullException();
            }

            return tags.Select(Tag => MapTagToTagResponse(Tag)).ToList();
        }

        public async Task<List<TagResponse>> GetTagsByPostIdAsync(int postId)
        {
            var tags = await _postRepository.GetTagsByPostIdAsync(postId);

            if (tags == null)
            {
                throw new ArgumentNullException();
            }

            return tags.Select(Tag => MapTagToTagResponse(Tag)).ToList();
        }

        public async Task<TagResponse?> GetTagById(int Id)
        {
            var tags = await _postRepository.GetTagByIdAsync(Id);

            if (tags == null)
            {
                return null;
            }
            return MapTagToTagResponse(tags);
        }
    }
}
