namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(UserId))]
    public class UserImage
    {
        [ForeignKey("User.UserId")]
        public int UserId { get; set; }

        [Column(TypeName = "varbinary(max)")]
        public byte[] Image { get; set; } = Array.Empty<byte>();

        public User User { get; set; } = null!;
    }
}
