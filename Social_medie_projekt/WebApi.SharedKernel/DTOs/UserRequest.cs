namespace WebApi.SharedKernel.DTOs
{
    public class UserRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "Cannot be longer than 32 chars.")]
        public string UserName { get; set; } = string.Empty;

        public UserUserImage? UserImage { get; set; }
    }

    public class UserUserImage
    {
        public string? Image { get; set; }
    }
}
