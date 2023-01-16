namespace WebApi.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Login> Login { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>().HasData(
                new Login
                {
                    LoginId = 1,
                    Email = "Test1@mail.dk",
                    Password = "password"
                },

                new Login
                {
                    LoginId = 2,
                    Email = "Test2@mail.dk",
                    Password = "password"

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
                
        }
    }
}
