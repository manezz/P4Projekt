namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(UserId), nameof(PostId))]
    public class Like : ISoftDelete
    {
        [ForeignKey("User.UserId")]
        public int UserId { get; set; }

        [ForeignKey("Post.PostId")]
        public int PostId { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        public Post? Post { get; set; }

        public User? User { get; set; }
    }
}
