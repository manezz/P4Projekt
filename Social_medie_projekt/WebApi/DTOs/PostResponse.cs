namespace WebApi.DTOs
{
    public class PostResponse
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Desc { get; set; }
        public DateTime Date { get; set; }
        public int? Likes { get; set; }
        public PostUserResponse User { get; set; } = new();
        public List<TagResponse>? Tags { get; set; }
    }

        //public List<TagResponse> Tags { get; set; }
    }

    public class PostUserResponse
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

