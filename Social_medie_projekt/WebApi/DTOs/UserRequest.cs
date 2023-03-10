namespace WebApi.DTOs
{
    public class UserRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "Cannot be longer than 32 chars.")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public DateTime Created { get; set; } = DateTime.Now;

        public UserLogin Login { get; set; }
    }

    public class UserLogin
    {
        [Required]
        [StringLength(32, ErrorMessage = "Cannot be longer than 32 chars.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(32, ErrorMessage = "Cannot be longer than 32 chars. ")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(32, ErrorMessage = "Type can be either 0 for Admin or 1 for user")]
        public Role Type { get; set; }
    }
}
