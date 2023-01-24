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

    }
}
