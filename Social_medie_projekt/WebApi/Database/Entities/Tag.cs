namespace WebApi.Database.Entities
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string tag { get; set; } = string.Empty;

        public List<Posts> Posts { get; set; }
    }
}
