namespace WebApi.DTOs
{
    public class LoginResponse
    {
        public int LoginId { get; set; }
        public string Email { get; set; } = string.Empty;
        public Role Type { get; set; } = 0;
        public LoginUserResponse? User { get; set; } = new();
    }


    public class LoginUserResponse
    {
        public int UserId { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
