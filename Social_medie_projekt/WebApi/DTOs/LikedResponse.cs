namespace WebApi.DTOs
{
    public class LikedResponse
    {
        public int UserId { get; set; }

        public int PostId { get; set; }

        public DateTime LikedTime { get; set; }
    }
}
