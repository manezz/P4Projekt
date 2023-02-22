namespace WebApi.Service
{
    public interface IPostService
    {
        Task<List<PostResponse>> GetAllPostsAsync();
        Task<PostResponse?> GetPostByPostIdAsync(int Id);
        Task<List<PostResponse?>> GetPostByUserIdAsync(int Id);
        Task<PostResponse> CreatePostAsync(PostRequest newPost);
        Task<PostResponse?> UpdatePostAsync(int postId, PostUpdateRequest updatePost);
        Task<PostResponse?> DeletePostAsync(int postId);
        Task<LikedResponse> CreateLikeAsync(LikedRequest newLike);
        Task<LikedResponse?> DeleteLikeAsync(LikedRequest deleteLike);
        Task<List<TagResponse>> GetAllTagsAsync();
        Task<TagResponse?> GetTagById(int Id);
        Task<TagResponse> CreateTagAsync(TagRequest newTag);
        Task<TagResponse> UpdateTagAsync(TagRequest newTag);
        Task<PostTagResponse> CreatePostTagAsync(int postId, int tagId);
        Task<PostTagResponse> UpdatePostTagByPostIdAsync(int postId, int tagId);
        Task<PostTagResponse> DeletePostTagByPostIdAsync(int postId, int tagId);
    }

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostResponse>> GetAllPostsAsync()
        {
            List<Posts> posts = await _postRepository.GetAllPostsAsync();

            if (posts == null)
            {
                throw new ArgumentNullException();
            }

            return posts.Select(post => MapPostToPostResponse(post)).ToList();
        }

        public async Task<PostResponse?> GetPostByPostIdAsync(int postId)
        {
            var posts = await _postRepository.GetPostByPostIdAsync(postId);

            if (posts == null)
            {
                return null;
            }
            return MapPostToPostResponse(posts);
        }

        public async Task<List<PostResponse?>> GetPostByUserIdAsync(int userId)
        {
            var posts = await _postRepository.GetPostByUserIdAsync(userId);

            if (posts == null)
            {
                return null;
            }
            return posts.Select(post => MapPostToPostResponse(post)).ToList();
        }

        public async Task<PostResponse> CreatePostAsync(PostRequest newPost)
        {
            var post = await _postRepository.CreatePostAsync(MapPostRequestToPost(newPost));

            if (post == null)
            {
                throw new ArgumentNullException();
            }

            var tags = newPost.Tags.Select(tag => CreateTagAsync(tag).Result).ToList();

            if (post == null)
            {
                throw new ArgumentNullException();
            }

            _ = tags.Select(async tagResponse => await CreatePostTagAsync(post.PostId, tagResponse.TagId)).ToList();

            return MapPostToPostResponse(post, tags);
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

        public async Task<PostResponse?> UpdatePostAsync(int postId, PostUpdateRequest updatePost)
        {
            var post = await _postRepository.UpdatePostAsync(postId, MapPostUpdateRequestToPost(updatePost));

            if (post == null)
            {
                throw new ArgumentNullException();
                //return MapPostToPostResponse(post);
            }
            //Re-using CreateTagAsync since it already checks tag and does the necessary processes
            var tags = updatePost.Tags.Select(tag => UpdateTagAsync(tag).Result).ToList();
            if (post == null)
            {
                throw new ArgumentNullException();
            }

            _ = tags.Select(async tagResponse => await UpdatePostTagByPostIdAsync(post.PostId, tagResponse.TagId)).ToList();

            return MapPostToPostResponse(post, tags);
            //return null;
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
            var postTag = await _postRepository.UpdatePostTagAsync(MapPostTagRequestToPostTag(postId, tagId));

            if (postTag == null)
            {
                throw new ArgumentNullException();
            }

            return MapPostTagToPostTagResponse(postTag);
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

        public async Task<TagResponse?> GetTagById(int Id)
        {
            var tags = await _postRepository.GetTagByIdAsync(Id);

            if (tags == null)
            {
                return null;
            }
            return MapTagToTagResponse(tags);
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

        public async Task<TagResponse> UpdateTagAsync(TagRequest newTag)
        {
            var tag = await _postRepository.UpdateTagAsync(MapTagRequestToTag(newTag));

            if (tag == null)
            {
                throw new ArgumentNullException();
            }
            return MapTagToTagResponse(tag);
        }

        public async Task<LikedResponse> CreateLikeAsync(LikedRequest newLike)
        {
            var like = await _postRepository.CreateLikeAsync(MapLikeRequestToLike(newLike));

            var post = await _postRepository.UpdatePostLikesAsync(newLike.PostId, 1);

            if (like == null || post == null)
            {
                throw new ArgumentNullException();
            }

            return MapLikeToLikeResponse(like);
        }

        public async Task<LikedResponse?> DeleteLikeAsync(LikedRequest deleteLike)
        {
            var like = await _postRepository.DeleteLikeAsync(MapLikeRequestToLike(deleteLike));

            var post = await _postRepository.UpdatePostLikesAsync(deleteLike.PostId, -1);

            if (like == null || post == null)
            {
                throw new ArgumentNullException();
            }

            return MapLikeToLikeResponse(like);
        }

        private static PostResponse MapPostToPostResponse(Posts post, List<TagResponse> tags)
        {
            return new PostResponse
            {
                PostId = post.PostId,
                UserId = post.UserId,
                Title = post.Title,
                Desc = post.Desc,
                Date = post.Date,
                Likes = post.Likes,
                User = new PostUserResponse
                {
                    UserName = post.PostUser.UserName,
                },
                Tags = tags
            };
        }

        private static PostResponse MapPostToPostResponse(Posts post)
        {
            return new PostResponse
            {
                PostId = post.PostId,
                UserId = post.UserId,
                Title = post.Title,
                Desc = post.Desc,
                Date = post.Date,
                Likes = post.Likes,
                User = new PostUserResponse
                {
                    UserName = post.PostUser.UserName,
                },//Hvor kommer tags fra
                //Tags = post.Tags.Select(x => new PostTagResponse
                //{
                //    TagId = x.TagId,
                //    Name = x.Name
                //}).ToList()
            };
        }

        private static Posts MapPostRequestToPost(PostRequest postRequest)
        {
            return new Posts
            {
                UserId = postRequest.UserId,
                Title = postRequest.Title,
                Desc = postRequest.Desc,
            };
        }

        private static Posts MapPostUpdateRequestToPost(PostUpdateRequest postUpdateRequest)
        {
            return new Posts
            {
                Title = postUpdateRequest.Title,
                Desc = postUpdateRequest.Desc,
            };
        }

        private static Liked MapLikeRequestToLike(LikedRequest likedRequest)
        {
            return new Liked
            {
                LikeUserId = likedRequest.UserId,
                PostId = likedRequest.PostId,
            };
        }

        private static LikedResponse MapLikeToLikeResponse(Liked like)
        {
            return new LikedResponse
            {
                UserId = like.LikeUserId,
                PostId = like.PostId,
                LikedTime = like.LikedTime,
            };
        }

        private PostTagResponse MapPostTagToPostTagResponse(PostsTag postsTag)
        {
            return new PostTagResponse
            {
                PostId = postsTag.PostId,
                TagId = postsTag.TagId,
            };
        }

        // change name
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
    }
}
