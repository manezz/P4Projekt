namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(UserId), nameof(PostId))]
    public class Like
    {
        [ForeignKey("User.UserId")]
        public int UserId { get; set; }

        [ForeignKey("Post.PostId")]
        public int PostId { get; set; }

        public Post Post { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
