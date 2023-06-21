namespace WebApi.DAL.Database.Entities
{
    public class Post : ISoftDelete
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

        public PostLikes PostLikes { get; set; } = null!;

        public User User { get; set; } = null!;

        public List<PostTag> PostTags { get; } = new();

        public List<Tag> Tags { get; } = new();
    }
}
