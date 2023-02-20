namespace WebApi.DTOs
{
    public class LoginResponse
    {
        public int LoginId { get; set; }
        public string Email { get; set; } = string.Empty;
        public Role Type { get; set; } = 0;
        public LoginUserResponse? User { get; set; } = new();
    }

    public class LoginUserResponse
    {
        public int UserId { get; set; } 
        public string UserName { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public List<UserPostLoginResponse>? Posts { get; set; } = new();
    }

    public class UserPostLoginResponse
    {
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Desc { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;

        public int? Likes { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}

