namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAllTagsAsync()
        {
            try
            {
                List<TagResponse> tags = await _tagService.GetAllTagsAsync();

                if (tags.Count == 0)
                {
                    return NoContent();
                }
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpGet]
        [Route("{tagId}")]
        public async Task<IActionResult> GetTagByIdAsync([FromRoute] int tagId)
        {
            try
            {
                var tagResponse = await _tagService.GetTagById(tagId);

                if (tagResponse == null)
                {
                    return NotFound();
                }
                return Ok(tagResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.User, Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateTagAsync([FromBody] TagRequest newTag)
        {
            try
            {
                TagResponse tagResponse = await _tagService.CreateTagAsync(newTag);

                return Ok(tagResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }

}
