namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        [Route("{postId}/{userId}")]
        public async Task<IActionResult> FindLike([FromRoute] int userId, [FromRoute] int postId)
        {
            try
            {
                LikeResponse likeResponse = await _likeService.FindLike(userId, postId);

                if (likeResponse == null)
                {
                    return NotFound();
                }

                return Ok(likeResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetAllLikesFromUser([FromRoute] int userId)
        {
            try
            {
                var likeResponse = await _likeService.GetAllLikesFromUser(userId);

                if (likeResponse == null)
                {
                    return NotFound();
                }

                return Ok(likeResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> LikePost([FromBody] LikeRequest like)
        {
            try
            {
                LikeResponse likeResponse = await _likeService.CreateLikeAsync(like);

                return Ok(likeResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpDelete]
        [Route("{userId}/{postId}")]
        public async Task<IActionResult> UnlikePost([FromRoute] int userId, [FromRoute] int postId)
        {
            try
            {
                var likeResponse = await _likeService.DeleteLikeAsync(userId, postId);

                if (likeResponse == null)
                {
                    return NotFound();
                }

                return Ok(likeResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
