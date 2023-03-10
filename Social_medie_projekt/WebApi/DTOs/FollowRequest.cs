namespace WebApi.DTOs
{
    public class FollowRequest
    {
        [Required]
        public int FollowerUserId { get; set; }

        [Required]
        public int FollowingUserId { get; set; }
    }
}
