namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(UserId))]
    public class UserImage
    {
        [ForeignKey("User.UserId")]
        public int UserId { get; set; }

        [Column(TypeName = "varbinary(max)")]
        public byte[]? Image { get; set; }

        public User? User { get; set; }
    }
}
