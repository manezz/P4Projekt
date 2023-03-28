namespace WebApi.DTOs
{
    public class SignInResponse
    {
        public int LoginId { get; set; }

        public string Email { get; set; } = string.Empty;

        public Role Role { get; set; }

        public SignInUserResponse? User { get; set; }

        public string? Token { get; set; }
    }

    public class SignInUserResponse
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public DateTime Created { get; set; }

        public List<UserPostSignInResponse>? Posts { get; set; }
    }

    public class UserPostSignInResponse
    {
        public int PostId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Desc { get; set; } = string.Empty;

        public int? Likes { get; set; }

        public DateTime Date { get; set; }
    }
}