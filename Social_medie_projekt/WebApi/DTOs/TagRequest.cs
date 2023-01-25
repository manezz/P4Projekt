namespace WebApi.DTOs
{
    public class TagRequest
    {
        [Required]
        public int TagId { get; set; }
        [Required]
        public string tag { get; set; } = string.Empty;
    }
}
