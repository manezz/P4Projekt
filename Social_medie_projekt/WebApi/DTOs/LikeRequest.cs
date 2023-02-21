namespace WebApi.DTOs
{
    public class LikeRequest
    {
        [Required]
        public int KeyId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int PostId { get; set; }

        public Boolean IsLiked { get; set; }
    }
}
