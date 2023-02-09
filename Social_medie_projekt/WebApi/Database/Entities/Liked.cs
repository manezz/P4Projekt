namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(UserId), nameof(PostId))]
    public class Liked
    {
        [Key]
        public int LikedId { get; set; }

        [ForeignKey("Posts.UserId")]
        public int UserId { get; set; }

        [ForeignKey("Posts.PostId")]
        public int PostId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LikedTime { get; set; } = DateTime.Now;

        //public User User { get; set; }

        public Posts Posts { get; set; }
    }
}
