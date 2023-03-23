namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(PostId), nameof(TagId))]
    public class PostTag
    {
        [ForeignKey("Post.PostId")]
        public int PostId { get; set; }

        [ForeignKey("Tag.TagId")]
        public int TagId { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        public Post? Post { get; set; }

        public Tag? Tag { get; set; }
    }
}
