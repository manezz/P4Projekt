namespace WebApi.DTOs
{
    public class PostUpdateRequest
    {
        [Required]
        [MaxLength(100, ErrorMessage ="You have reached the title limit")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(300, ErrorMessage = "You have reached the limit of 300 Characters")]
        public string Desc { get; set; } = string.Empty;

        public string Tags { get; set; } = string.Empty;
    }
}
