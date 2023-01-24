namespace WebApi.DTOs
{
    public class PostRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage ="You have reached the title limit")]
        public string Title { get; set; }

        [Required]
        [MaxLength(300, ErrorMessage = "You have reached the limit of 300 Characters")]
        public string Desc { get; set; } = string.Empty;

        [Required]
        //public List<Tag> Tags { get; set; } = new();
        public List<PostTagRequest> Tags { get; set; } = new();

    }
    public class PostTagRequest
    {
        public string ?Tag { get; set; }
    }
}
