namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(UserId), nameof(FollowingId))]
    public class Follow : ISoftDelete
    {
        [ForeignKey("User.UserId")]
        public int UserId { get; set; }

        [ForeignKey("User.UserId")]
        public int FollowingId { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        public User? User { get; set; }

    }
}
