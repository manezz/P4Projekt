namespace WebApi.DTOs
{
    public class TagResponse
    {
        public int TagId { get; set; }

        public string Name { get; set; }

        public List<TagPostResponse> post { get; set; }
    }
    public class TagPostResponse
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Desc { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int? Likes { get; set; }

        public PostUserResponse User { get; set; } = new();
    }
}
