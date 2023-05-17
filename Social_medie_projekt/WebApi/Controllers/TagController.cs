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
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<TagResponse> tags = await _tagService.GetAllAsync();

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
        public async Task<IActionResult> GetByIdAsync([FromRoute] int tagId)
        {
            try
            {
                var tagResponse = await _tagService.GetByIdAsync(tagId);

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
        public async Task<IActionResult> CreateAsync([FromBody] TagRequest newTag)
        {
            try
            {
                TagResponse tagResponse = await _tagService.CreateAsync(newTag);

                return Ok(tagResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }

}
