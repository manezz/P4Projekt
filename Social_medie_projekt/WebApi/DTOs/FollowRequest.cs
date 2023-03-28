namespace WebApi.DTOs
{
    public class FollowRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int FollowingId { get; set; }
    }
}
