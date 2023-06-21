namespace WebApi.SharedKernel.DTOs
{
    public class LikeRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int PostId { get; set; }
    }
}
