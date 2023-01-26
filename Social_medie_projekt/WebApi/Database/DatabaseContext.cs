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
                    FirstName = "test",
                    LastName = "1",
                    Address = "testvej 1",
                    Created = DateTime.Now
                },

                new User
                {
                    UserId = 2,
                    LoginId = 2,
                    FirstName = "test",
                    LastName = "2",
                    Address = "testvej 2",
                    Created = DateTime.Now
                });

            modelBuilder.Entity<Posts>().HasData(
                new Posts
                {
                    PostId = 1,
                    UserId = 1,
                    Title = "testestestest",
                    Desc = "tadnawdnada",
                    Likes = 1,
                    Date = DateTime.Now
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
                    tag = "sax",
                    TagId = 1,
                    PostId= 1,
                },
                new Tag
                {
                    tag = "fax",
                    TagId = 2,
                    PostId = 2,
                });
        }
    }
}
