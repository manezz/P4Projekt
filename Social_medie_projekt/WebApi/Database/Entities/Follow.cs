namespace WebApi.Database.Entities
{
    [PrimaryKey(nameof(UserId), nameof(FollowingId))]
    public class Follow
    {
        [ForeignKey("User.UserId")]
        public int UserId { get; set; }

        [ForeignKey("User.UserId")]
        public int FollowingId { get; set; }

    }
}
