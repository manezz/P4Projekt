namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(PostId), nameof(TagId))]
    public class PostTag
    {
        [ForeignKey("Posts.PostId")]
        public int PostId { get; set; }

        [ForeignKey("Tag.TagId")]
        public int TagId { get; set; }
        public string? Name { get; set; }

        public Post? Posts { get; set; }

        public Tag? Tag { get; set; }
    }
}
