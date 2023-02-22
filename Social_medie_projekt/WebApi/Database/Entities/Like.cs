namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(UserId), nameof(PostId))]
    public class Like
    {
        [Key]
        public int KeyId { get; set; } = 0;        

        [ForeignKey("Posts.PostId")]
        public int PostId { get; set; } = 0;

        public Post? Post { get; set; }




        [ForeignKey("User.UserId")]
        public int UserId { get; set; } = 0;

        public User? User { get; set; }

    }
}
