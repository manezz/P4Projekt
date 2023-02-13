namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(PostId), nameof(TagId))]
    public class PostsTag
    {
        [ForeignKey("posts.PostId")]
        public int PostId { get; set; }

        [ForeignKey("tag.TagId")]
        public int TagId { get; set; }

        public Posts posts { get; set; }

        public Tag tag { get; set; }
    }
}
