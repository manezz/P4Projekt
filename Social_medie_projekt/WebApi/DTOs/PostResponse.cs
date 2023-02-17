namespace WebApi.DTOs
{
    public class PostResponse
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Desc { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public int? Likes { get; set; }

        public PostUserResponse? User { get; set; }

        public List<TagResponse>? Tags { get; set; }
    }
    public class PostUserResponse
    {
        public string UserName { get; set; } = string.Empty;
    }
    //public class PostTagResponse
    //{
    //    public int TagId { get; set; }

    //    public string Name { get; set; }
    //}
}
