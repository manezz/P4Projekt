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
        public async Task<IActionResult> GetAllAsync()
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
        public async Task<IActionResult> FindByIdAsync([FromRoute] int postId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                var postResponse = await _postService.FindByIdAsync(postId, currentUser.User.UserId);

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
        public async Task<IActionResult> FindAllByUserIdAsync([FromRoute] int userId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                var postResponse = await _postService.FindAllByUserIdAsync(userId, currentUser.User.UserId);

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
        public async Task<IActionResult> CreateAsync([FromBody] PostRequest newPost)
        {
            try
            {
                var postResponse = await _postService.CreateAsync(newPost);

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
        public async Task<IActionResult> UpdateAsync([FromRoute] int postId, [FromBody] PostUpdateRequest updatedPost)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                if (currentUser == null || !currentUser.User.Posts.Exists(x => x.PostId == postId) && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                var postResponse = await _postService.UpdateAsync(postId, updatedPost);

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
        public async Task<IActionResult> DeleteAsync([FromRoute] int postId)
        {
            try
            {
                LoginResponse? currentUser = (LoginResponse?)HttpContext.Items["Login"];

                if (currentUser == null || !currentUser.User.Posts.Exists(x => x.PostId == postId) && currentUser.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }

                var postResponse = await _postService.DeleteAsync(postId);

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
