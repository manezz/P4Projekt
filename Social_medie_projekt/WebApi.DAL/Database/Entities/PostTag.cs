namespace WebApi.DAL.Database.Entities
{
    [PrimaryKey(nameof(PostId), nameof(TagId))]
    public class PostTag
    {
        [ForeignKey("Post.PostId")]
        public int PostId { get; set; }

        [ForeignKey("Tag.TagId")]
        public int TagId { get; set; }

        public Post Post { get; set; } = null!;

        public Tag Tag { get; set; } = null!;
    }
}
