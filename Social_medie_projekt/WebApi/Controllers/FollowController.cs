namespace WebApi.Controllers
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
        public async Task<IActionResult> FindFollow([FromRoute] int userId, [FromRoute] int followingId)
        {
            try
            {
                FollowResponse? followResponse = await _followService.FindFollow(userId, followingId);

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
        public async Task<IActionResult> FindUsersFollowing([FromRoute] int userId)
        {
            try
            {
                var followResponse = await _followService.FindUsersFollowing(userId);

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
        public async Task<IActionResult> FindUsersFollowers([FromRoute] int followingId)
        {
            try
            {
                var followResponse = await _followService.FindUsersFollowers(followingId);

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
        public async Task<IActionResult> Follow([FromBody] FollowRequest follow)
        {
            try
            {
                FollowResponse followResponse = await _followService.Follow(follow);

                return Ok(followResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("unfollow/{userId}/{followingId}")]
        public async Task<IActionResult> Unfollow([FromRoute] int userId, [FromRoute] int followingId)
        {
            try
            {
                var followResponse = await _followService.Unfollow(userId, followingId);

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
