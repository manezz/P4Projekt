namespace WebApi.DTOs
{
    public class LikeRequest
    {
        [Required]
        public int KeyId { get; set; } = 0;

        [Required]
        public int UserId { get; set; } = 0;

        [Required]
        public int PostId { get; set; } = 0;
    }
}
