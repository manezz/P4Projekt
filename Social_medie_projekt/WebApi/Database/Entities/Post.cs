namespace WebApi.Database.Entities
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [ForeignKey("User.UserId")]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(1000)")]
        public string Desc { get; set; } = string.Empty;

        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        public PostLikes? PostLikes { get; set; }

        public User? User { get; set; }
    }
}
