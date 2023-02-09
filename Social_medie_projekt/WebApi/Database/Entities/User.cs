namespace WebApi.Database.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [ForeignKey("Login.LoginId")]
        public int LoginId { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string FirstName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(32)")] 
        public string LastName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(32)")]
        public string Address { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Created { get; set; } = DateTime.Now;

        public Login Login { get; set; }

        public List<Posts> Posts { get; set; } = new();

        public List<Liked> Líked { get; set; } = new();
    }
}
