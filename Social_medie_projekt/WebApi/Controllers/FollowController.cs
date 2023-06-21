namespace WebApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }


        [HttpGet]
        [Route("{userId}/{followingId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int userId, [FromRoute] int followingId)
        {
            try
            {
                FollowResponse? followResponse = await _followService.FindByIdAsync(userId, followingId);

                if (followResponse == null)
                {
                    return NotFound();
                }

                return Ok(followResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("find-followers/{userId}")]
        public async Task<IActionResult> FindAllByUserIdAsync([FromRoute] int userId)
        {
            try
            {
                var followResponse = await _followService.FindAllByUserIdAsync(userId);

                if (followResponse == null)
                {
                    return NotFound();
                }

                return Ok(followResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("following/{followingId}")]
        public async Task<IActionResult> FindAllByFollowingUserIdAsync([FromRoute] int followingId)
        {
            try
            {
                var followResponse = await _followService.FindAllByFollowingUserIdAsync(followingId);

                if (followResponse == null)
                {
                    return NotFound();
                }

                return Ok(followResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }




        [HttpPost]
        [Route("follow")]
        public async Task<IActionResult> CreateAsync([FromBody] FollowRequest follow)
        {
            try
            {
                FollowResponse followResponse = await _followService.CreateAsync(follow);

                return Ok(followResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("unfollow/{userId}/{followingId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int userId, [FromRoute] int followingId)
        {
            try
            {
                var followResponse = await _followService.DeleteAsync(userId, followingId);

                if (followResponse == null)
                {
                    return NotFound();
                }

                return Ok(followResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
