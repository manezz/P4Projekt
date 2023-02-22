namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(PostId), nameof(TagId))]
    public class PostsTag
    {
        [ForeignKey("Posts.PostId")]
        public int PostId { get; set; }

        [ForeignKey("Tag.TagId")]
        public int TagId { get; set; }

        public Posts? Posts { get; set; }

        public Tag? Tag { get; set; }
    }
}
