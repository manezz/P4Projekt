namespace WebApi.Database.Entities
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; } = 0;

        [Column(TypeName = "nvarchar(32)")]
        public string Tag { get; set; } = string.Empty;
    }
}
