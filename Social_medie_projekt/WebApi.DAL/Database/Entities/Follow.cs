namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(UserId), nameof(FollowingUserId))]
    public class Follow
    {
        [ForeignKey("User.UserId")]
        public int UserId { get; set; }

        [ForeignKey("FollowingUser.UserId")]
        public int FollowingUserId { get; set; }

        public User User { get; set; } = null!;

        public User FollowingUser { get; set; } = null!;
    }
}
