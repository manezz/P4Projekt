namespace WebApi.DTOs
{
    public class LoginRequest
    {
        [Required]
        [StringLength(64, ErrorMessage = "Email cannot be longer than 64 Chars")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength (64)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Range (0, 1, ErrorMessage = "Type must be 0 for user or 1 for admin")]
        public Role Type { get; set; }
    }

   
}
