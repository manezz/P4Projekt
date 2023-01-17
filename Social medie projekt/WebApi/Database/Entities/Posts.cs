namespace WebApi.Database.Entities
{
    public class Posts
    {

        [Key]
        public int PostId { get; set; } = 0;

        [ForeignKey("User.UserId")]
        public int UserId { get; set; } = 0;

        [Column(TypeName = "text")]
        public string PostInput { get; set; } = string.Empty;

        [Column(TypeName = "int")]
        public int? Likes { get; set; } = 0;

        [Column(TypeName ="datetime")]
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
