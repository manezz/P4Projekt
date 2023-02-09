namespace WebApi.DTOs
{
    public class TagRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
