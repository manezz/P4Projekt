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
        [Route("{followerId}/{followingId}")]
        public async Task<IActionResult> FindFollow([FromRoute] int followerId, [FromRoute] int followingId)
        {
            try
            {
                FollowResponse followResponse = await _followService.FindFollow(followerId, followingId);

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
        [Route("followers/{followerId}")]
        public async Task<IActionResult> FindUsersFollowing([FromRoute] int followerId)
        {
            try
            {
                var followResponse = await _followService.FindUsersFollowing(followerId);

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
        [Route("{followerId}/{followingId}")]
        public async Task<IActionResult> Unfollow([FromRoute] int followerId, [FromRoute] int followingId)
        {
            try
            {
                var followResponse = await _followService.Unfollow(followerId, followingId);

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
