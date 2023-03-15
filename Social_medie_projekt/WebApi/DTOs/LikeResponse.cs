namespace WebApi.DTOs
{
    public class LikeResponse
    {
        public LikePostResponse Post { get; set; } = new();

        public LikeUserResponse User { get; set; } = new();
    }

    public class LikePostResponse
    {
        public int PostId { get; set; }
    }

    public class LikeUserResponse
    {
        public int UserId { get; set; }
    }
}
