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
        [Route("UnLike")]
        public async Task<IActionResult> UnlikePost([FromBody] LikeRequest like)
        {
            try
            {
                var likeResponse = await _likeService.DeleteLikeAsync(like);

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
