namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(UserId), nameof(PostId))]
    public class Liked
    {
        [Key]
        public int KeyId { get; set; } = 0;

        [ForeignKey("User.UserId")]
        public int UserId { get; set; } = 0;

        [ForeignKey("Posts.PostId")]
        public int PostId { get; set; } = 0;

        public Boolean IsLiked { get; set; } = false;
    }
}
