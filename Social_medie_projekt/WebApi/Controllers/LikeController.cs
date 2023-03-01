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

        [HttpPost]
        //[Route("like")]
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
