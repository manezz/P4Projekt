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


        [HttpGet]
        [Route("{keyId}")]
        public async Task<IActionResult> CheckLike([FromRoute] int keyId)
        {
            try
            {
                LikeResponse likeResponse = await _likeService.CheckLike(keyId);

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

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetAllUsersLikes([FromRoute] int userId)
        {
            try
            {
                LikeResponse likeResponse = await _likeService.CheckLike(userId);

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

        [HttpPost]
        [Route("like")]
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

        [HttpDelete]
        [Route("{keyId}")]
        public async Task<IActionResult> UnlikePost([FromRoute] int keyId)
        {
            try
            {
                var likeResponse = await _likeService.DeleteLikeAsync(keyId);

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
