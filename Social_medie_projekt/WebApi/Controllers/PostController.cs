namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                List<PostResponse> posts = await _postService.GetAllAsync(currentUser.User.UserId);

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

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        [Route("{postId}")]
        public async Task<IActionResult> GetPostByPostIdAsync([FromRoute] int postId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                var postResponse = await _postService.GetPostByPostIdAsync(postId, currentUser.User.UserId);

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

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetAllPostsByUserIdAsync([FromRoute] int userId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                var postResponse = await _postService.GetAllPostsByUserIdAsync(userId, currentUser.User.UserId);

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

        [Authorize(Role.User, Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody] PostRequest newPost)
        {
            try
            {
                var postResponse = await _postService.CreatePostAsync(newPost);

                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [Authorize(Role.User, Role.Admin)]
        [HttpPut]
        [Route("{postId}")]
        public async Task<IActionResult> UpdatePostAsync([FromRoute] int postId, [FromBody] PostUpdateRequest updatedPost)
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

        [Authorize(Role.User, Role.Admin)]
        [HttpDelete]
        [Route("{postId}")]
        public async Task<IActionResult> DeletePostAsync([FromRoute] int postId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                if (currentUser != null && !currentUser.User.Posts.Exists(x => x.PostId == postId) && currentUser.Role != Role.Admin)
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
    }

}
