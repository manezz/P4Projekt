namespace WebApi.Database.Entities
{
    public class Posts
    {

        [Key]
        public int PostId { get; set; } = 0;

        [ForeignKey("User.UserId")]
        public int UserId { get; set; } = 0;

        [Column(TypeName = "nvarchar(32)")]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "text")]
        public string Desc { get; set; } = string.Empty ;

        [Column(TypeName = "int")]
        public int? Likes { get; set; } = 0;

        [Column(TypeName ="datetime")]
        public DateTime Date { get; set; } = DateTime.Now;

        public User User { get; set; }

        [ForeignKey("Tag.TagId")]
        public int TagId { get; set; } = 0;

        public List<Tag> Tags { get; set; } = new();

    }
}
