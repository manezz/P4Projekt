namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(LikeUserId), nameof(PostId))]
    public class Liked
    {
        [ForeignKey("LikeUser.UserId")]
        public int LikeUserId { get; set; }

        [ForeignKey("Posts.PostId")]
        public int PostId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LikedTime { get; set; } = DateTime.Now;

        public User? LikeUser { get; set; }

        public Posts? Posts { get; set; }
    }
}
