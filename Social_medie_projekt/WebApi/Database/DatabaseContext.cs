namespace WebApi.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Login> Login { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Liked> Liked { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>(e =>
            {
                e.HasIndex(t => t.Name).IsUnique();
            });

            modelBuilder.Entity<Login>().HasData(
                new Login
                {
                    LoginId = 1,
                    Email = "Test1@mail.dk",
                    Password = "password",
                    Type = 0
                },

                new Login
                {
                    LoginId = 2,
                    Email = "Test2@mail.dk",
                    Password = "password",
                    Type = (Role)1
                });

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    LoginId = 1,
                    UserName = "tester 1",
                    Created = DateTime.Now
                },

                new User
                {
                    UserId = 2,
                    LoginId = 2,
                    UserName = "222test222",
                    Created = DateTime.Now
                });

            modelBuilder.Entity<Posts>()
                .HasData(
                new Posts
                {
                    PostId = 1,
                    Title = "testestestest",
                    Desc = "tadnawdnada",
                    Likes = 1,
                    Date = DateTime.Now,
                    UserId = 1,
                    
                },

                new Posts
                {
                    PostId = 2,
                    Title = "Test!",
                    Desc = "Woooooo!",
                    Likes = 0,
                    Date = DateTime.Now,
                    UserId = 2,
                    
                });

            modelBuilder.Entity<Liked>().HasData(
                new Liked
                {
                    UserId = 2,
                    PostId = 1,
                    LikedTime = DateTime.Now
                });

            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    Name = "sax",
                    TagId = 1,
                },
                new Tag
                {
                    Name = "fax",
                    TagId = 2,
                },
                new Tag
                {
                    Name = "howdy",
                    TagId = 3,
                });
        }
    }
}
