namespace WebApi.SharedKernel.DTOs
{
    public class LoginRequest
    {
        [Required]
        [StringLength(64, ErrorMessage = "Email cannot be longer than 64 chars")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(64)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Range(0, 1, ErrorMessage = "Type must be 0 for Admin or 1 for User")]
        public Role Role { get; set; }

        public LoginUserRequest User { get; set; } = null!;
    }

    public class LoginUserRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "Cannot be longer than 32 chars.")]
        public string UserName { get; set; } = string.Empty;

        public LoginUserUserImage UserImage { get; set; } = null!;
    }

    public class LoginUserUserImage
    {
        public string Image { get; set; } = string.Empty;
    }
}
