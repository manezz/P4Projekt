namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(PostId))]
    public class PostLikes
    {
        [ForeignKey("Post.PostId")]
        public int PostId { get; set; }

        [Column(TypeName = "int")]
        public int Likes { get; set; } = 0;

        public Post Post { get; set; } = null!;
    }
}
