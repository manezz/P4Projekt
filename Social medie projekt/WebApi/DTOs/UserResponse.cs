namespace WebApi.DTOs
{
    public class UserResponse
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public UserLoginResponse Login { get; set; }
    }
    public class UserLoginResponse
    {
        public int LoginId { get; set; }
        public string Email { get; set; } = string.Empty;
        public Role Type { get; set; }
    }
}
