namespace WebApi.DTOs
{
    public class SignInResponse
    {
        public string Token { get; set; } = string.Empty;

        public LoginResponse? LoginResponse { get; set; }
    }
}