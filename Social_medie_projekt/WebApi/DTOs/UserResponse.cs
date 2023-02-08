namespace WebApi.DTOs
{
    public class UserResponse
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.Now;

        public UserLoginResponse Login { get; set; }

        public List<UserPostResponse> Posts { get; set; } = new();
    }

    public class UserLoginResponse
    {
        public int LoginId { get; set; }

        public string Email { get; set; } = string.Empty;

        public Role Type { get; set; }
    }

    public class UserPostResponse
    {
        public int PostId { get; set; } 

        public int UserId { get; set; } 

        public string Title { get; set; } = string.Empty;

        public string Desc { get; set; } = string.Empty;
      
        public int? Likes { get; set; } 

        public DateTime Date { get; set; } = DateTime.Now;
    }

    
}
