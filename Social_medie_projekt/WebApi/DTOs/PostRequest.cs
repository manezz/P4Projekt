namespace WebApi.DTOs
{
    public class PostRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "You have reached the title limit")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000, ErrorMessage = "You have reached the limit of 1000 Characters")]
        public string Desc { get; set; } = string.Empty;

        public List<TagRequest>? Tags { get; set; }

    }
}
