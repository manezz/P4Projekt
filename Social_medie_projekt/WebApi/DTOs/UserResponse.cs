namespace WebApi.DTOs
{
    public class UserResponse
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
        public DateTime Created { get; set; }

        public UserLoginResponse? Login { get; set; }

        public List<UserPostResponse> Posts { get; set; } = new();
        public List<UserFollowResponse> Follow { get; set; }
    }

    public class UserLoginResponse
    {
        public int LoginId { get; set; }

        public string Email { get; set; } = string.Empty;

        public Role Type { get; set; }
    }

    public class UserPostResponse
    {
        public int PostId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Desc { get; set; } = string.Empty;
        //public string Tags { get; set; } = string.Empty;

        public int? Likes { get; set; }

        public DateTime Date { get; set; }
    }

    public class UserFollowResponse
    {
        public int FollowerId{ get; set; }
        public int FollowingId { get; set; }
    }


}
