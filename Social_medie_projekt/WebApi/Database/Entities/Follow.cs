namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(FollowerUserId), nameof(FollowingUserId))]
    public class Follow
    {
        [ForeignKey("User.UserId")]
        public int FollowerUserId { get; set; } // The user who follows

        [ForeignKey("User.UserId")]
        public int FollowingUserId { get; set; } // other users

    }
}
