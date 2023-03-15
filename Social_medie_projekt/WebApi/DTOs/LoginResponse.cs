namespace WebApi.DTOs
{
    public class LoginResponse
    {
        public int LoginId { get; set; }

        public string Email { get; set; } = string.Empty;

        public Role Role { get; set; }

        public LoginUserResponse User { get; set; } = new();

        public string? Token { get; set; }
    }

    public class LoginUserResponse
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public List<UserPostLoginResponse> Posts { get; set; } = new();
    }

    public class UserPostLoginResponse
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Desc { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public UserPostLoginPostLikesResponse PostLikes { get; set; } = new();
    }

    public class UserPostLoginPostLikesResponse
    {
        public int Likes { get; set; }
    }
}

