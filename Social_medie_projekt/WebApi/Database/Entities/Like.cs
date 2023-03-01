namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(UserId), nameof(PostId))]
    public class Like
    {
        [Key]
        public int UserId { get; set; } = 0;        

        [Key]
        public int PostId { get; set; } = 0;

        public Post? Post { get; set; }


        public User? User { get; set; }

    }
}
