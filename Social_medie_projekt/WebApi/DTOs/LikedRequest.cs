namespace WebApi.DTOs
{
    public class LikedRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
