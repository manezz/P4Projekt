namespace WebApi.DTOs
{
    public class UserSignupRequest
    {
        [Required]
        [StringLength(64, ErrorMessage = "Email cannot be longer than 64 chars")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Range(0, 1, ErrorMessage = "Type can either be 0 for user, or 1 for admin.")]
        public Role Type { get; set; }

        [Required]
        [StringLength(64)]
        public string Password { get; set; } = string.Empty;

        public UserSignupUserRequest User { get; set; }
    }

    public class UserSignupUserRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "Cannot be longer than 32 chars.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(32, ErrorMessage = "Cannot be longer than 32 chars.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(64, ErrorMessage = "Cannot be longer than 64 chars.")]
        public string Address { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.Now;
    }
}
