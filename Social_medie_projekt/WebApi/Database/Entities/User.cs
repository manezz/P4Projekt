namespace WebApi.Database.Entities
{
    public class User : ISoftDelete
    {
        [Key]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string UserName { get; set; } = string.Empty;

        [Column(TypeName = "datetime")]
        public DateTime Created { get; set; } = DateTime.Now;

        [ForeignKey("Login.LoginId")]
        public int LoginId { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        public Login? Login { get; set; }

        public UserImage? Image { get; set; }

        public List<Post>? Posts { get; set; } = new();

        public List<Follow>? Follow { get; set; } = new();
    }
}
