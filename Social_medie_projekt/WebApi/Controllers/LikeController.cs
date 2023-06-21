namespace WebApi.API.Controllers
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
        [Route("{userId}/{postId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int userId, [FromRoute] int postId)
        {
            try
            {
                LikeResponse? likeResponse = await _likeService.FindByIdAsync(userId, postId);

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
        public async Task<IActionResult> FindAllByUserIdAsync([FromRoute] int userId)
        {
            try
            {
                var likeResponse = await _likeService.FindAllByUserIdAsync(userId);

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
        public async Task<IActionResult> CreateAsync([FromBody] LikeRequest like)
        {
            try
            {
                LikeResponse likeResponse = await _likeService.CreateAsync(like);

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
        public async Task<IActionResult> DeleteAsync([FromRoute] int userId, [FromRoute] int postId)
        {
            try
            {
                var likeResponse = await _likeService.DeleteAsync(userId, postId);

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
