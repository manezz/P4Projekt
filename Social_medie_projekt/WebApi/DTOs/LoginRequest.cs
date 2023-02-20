namespace WebApi.DTOs
{
    public class LoginRequest
    {
        [Required]
        [StringLength(64, ErrorMessage = "Email cannot be longer than 64 chars")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(64)]
        public string Password { get; set; } = string.Empty;

        [Range(0, 1, ErrorMessage = "Type can either be 0 for admin, or 1 for user.")]
        public Role Type { get; set; }

        public LoginUserRequest? User { get; set; }
    }

    public class LoginUserRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "Cannot be longer than 32 chars.")]
        public string UserName { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
