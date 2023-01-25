namespace WebApi.DTOs
{
    public class TagResponse
    {
        public int TagId { get; set; }
        public string tag { get; set; }
        public List<TagPostResponse> posts { get; set; } = new();
    }
    public class TagPostResponse
    {
        public int PostId { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Desc { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int? Likes { get; set; }
    }
}
