namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPostService _postService;
        private readonly ILikeService _likeService;

        public PostController(IPostService postService, ILikeService likeService)
        {
            _postService = postService;
            _likeService = likeService;
        }

        //[Authorize(Role.user, Role.admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            try
            {
                List<PostResponse> posts = await _postService.GetAllPostsAsync();

                if (posts.Count == 0)
                {
                    return NoContent();

                }
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[Authorize(Role.user, Role.admin)]
        [HttpGet]
        [Route("{postId}")]
        public async Task<IActionResult> GetPostByPostId([FromRoute] int postId)
        {
            try
            {
                var postResponse = await _postService.GetPostByPostIdAsync(postId);

                if (postResponse == null)
                {
                    return NotFound();
                }
                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[Authorize(Role.user, Role.admin)]
        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetAllPostsByUserId([FromRoute] int userId)
        {
            try
            {
                var postResponse = await _postService.GetAllPostsByUserIdAsync(userId);

                if (postResponse == null)
                {
                    return NotFound();
                }
                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[Authorize(Role.user, Role.admin)]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostRequest newPost)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["User"];

                    // unødvendigt? der er allerede authorization på selve HttpPost
                //if (currentUser != null && newPost.UserId != currentUser.User.UserId)
                //{
                //    return Unauthorized(new { message = "Unauthorized" });
                //}

                var postResponse = await _postService.CreatePostAsync(newPost);

                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        //[Authorize(Role.user)]
        [HttpPut]
        [Route("{postId}")]
        public async Task<IActionResult> UpdatePost([FromRoute] int postId, [FromBody] PostUpdateRequest updatedPost)
        {
            try
            {
                var postResponse = await _postService.UpdatePostAsync(postId, updatedPost);

                if (postResponse == null)
                {
                    return NotFound();
                }

                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[Authorize(Role.user, Role.admin)]
        [HttpDelete]
        [Route("{postId}")]
        public async Task<IActionResult> DeletePost([FromRoute] int postId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["User"];

                if (currentUser != null && !currentUser.User.Posts.Exists(x => x.PostId == postId) && currentUser.Type != Role.admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                var postResponse = await _postService.DeletePostAsync(postId);

                if (postResponse == null)
                {
                    return NotFound();
                }

                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.user, Role.admin)]
        [HttpPost]
        [Route("Like")]
        public async Task<IActionResult> CreateLikeAsync([FromBody] LikedRequest newLike)
        {
            try
            {
                var likedResponse = await _postService.CreateLikeAsync(newLike);

                return Ok(likedResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.user, Role.admin)]
        [HttpDelete]
        [Route("Like")]
        public async Task<IActionResult> DeleteLikeAsync([FromBody] LikedRequest deleteLike)
        {
            try
            {
                var likedResponse = await _postService.DeleteLikeAsync(deleteLike);

                if (likedResponse == null)
                {
                    return NotFound();
                }

                return Ok(likedResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[Authorize(Role.user, Role.admin)]
        [HttpGet]
        [Route("Tag")]
        public async Task<IActionResult> GetAllTagsAsync()
        {
            try
            {
                List<TagResponse> tags = await _postService.GetAllTagsAsync();

                if (tags.Count == 0)
                {
                    return NoContent();
                }
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[Authorize(Role.user, Role.admin)]
        [HttpGet]
        [Route("Tag/{tagId}")]
        public async Task<IActionResult> GetTagByIdAsync([FromRoute] int tagId)
        {
            try
            {
                var tagResponse = await _postService.GetTagById(tagId);

                if (tagResponse == null)
                {
                    return NotFound();
                }
                return Ok(tagResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[Authorize(Role.user, Role.admin)]
        [HttpPost]
        [Route("Tag")]
        public async Task<IActionResult> CreateTagAsync([FromBody] TagRequest newTag)
        {
            try
            {
                TagResponse tagResponse = await _postService.CreateTagAsync(newTag);

                return Ok(tagResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }

}
