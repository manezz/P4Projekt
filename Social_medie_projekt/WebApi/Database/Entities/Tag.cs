namespace WebApi.Database.Entities
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string Name { get; set; }

    }
}
