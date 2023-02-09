namespace WebApi.DTOs
{
    public class SignInRequest
    {
        [Required]
        [StringLength(64, ErrorMessage = "Email cannot be longer than 64 chars")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(64, ErrorMessage = "Password cannot be longer than 64 chars")]
        public string Password { get; set; } = string.Empty;
    }
}
