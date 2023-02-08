namespace WebApi.Database.Entities
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }

        [ForeignKey("User.UserId")]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "text")]
        public string Desc { get; set; } = string.Empty ;

        [Column(TypeName = "int")]
        public int? Likes { get; set; } = 0;

        [Column(TypeName ="datetime")]
        public DateTime Date { get; set; } = DateTime.Now;

        public User User { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
