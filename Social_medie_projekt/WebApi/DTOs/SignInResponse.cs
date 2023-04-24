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

        public SignInUserUserImageResponse UserImage { get; set; } = new();
    }

    public class SignInUserUserImageResponse
    {
        public string? Image { get; set; }
    }
}