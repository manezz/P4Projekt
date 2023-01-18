namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(UserId), nameof(PostId))]
    public class Liked
    {
        [ForeignKey("User.UserId")]
        public int UserId { get; set; } = 0;

        [ForeignKey("Posts.PostId")]
        public int PostId { get; set; } = 0;

        [Column(TypeName = "datetime")]
        public DateTime LikedTime { get; set; } = DateTime.Now;
    }
}
