namespace WebApi.DTOs
{
    public class TagResponse
    {
        public int PostId { get; set; }
        public int TagId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
