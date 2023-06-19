namespace WebApi.Database.Entities
{
    public class Login : ISoftDelete
    {
        [Key]
        public int LoginId { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(32)")]
        public string Password { get; set; } = string.Empty;

        [Column(TypeName = "int")]
        public Role Role { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; }

        public User User { get; set; } = null!;
    }
}
