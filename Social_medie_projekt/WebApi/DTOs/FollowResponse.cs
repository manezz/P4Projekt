namespace WebApi.DTOs
{
    public class FollowResponse
    {
        // THE user
        public int FollowerUserId { get; set; }

        // other users
        public int FollowingUserId { get; set; }
    }
}
