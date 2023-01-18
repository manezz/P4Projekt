namespace WebApi.DTOs
{
    public class PostRequest
    {
        [Required]
        [MaxLength(300, ErrorMessage = "You have reached the limit of 300 Characters")]
        public string PostInput { get; set; } = string.Empty;

    }
}
