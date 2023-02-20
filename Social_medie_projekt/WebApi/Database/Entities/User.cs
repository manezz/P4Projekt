namespace WebApi.Database.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string UserName { get; set; } = string.Empty;

        [Column(TypeName = "datetime")]
        public DateTime Created { get; set; } = DateTime.Now;

        [ForeignKey("Login.LoginId")]
        public int LoginId { get; set; }

        public Login Login { get; set; }

        public List<Posts>? Posts { get; set; } = new();

    }
}
