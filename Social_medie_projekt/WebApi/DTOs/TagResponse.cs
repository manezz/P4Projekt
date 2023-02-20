namespace WebApi.DTOs
{
    public class TagResponse
    {
        public int TagId { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<TagPostResponse>? Post { get; set; }
    }
    public class TagPostResponse
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Desc { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public int? Likes { get; set; }

        public PostUserResponse User { get; set; } = new();
    }
}
