namespace WebApi.DTOs
{
    public class PostResponse
    {
        public int PostId { get; set; }
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Desc { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int? Likes { get; set; }

        public PostUserResponse? User { get; set; } = new();


    }
    public class PostUserResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
