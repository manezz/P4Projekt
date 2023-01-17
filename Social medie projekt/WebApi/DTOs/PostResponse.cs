namespace WebApi.DTOs
{
    public class PostResponse
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string PostInput { get; set; }

        public int? Likes { get; set; }

    }
}
