namespace WebApi.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Login> Login { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Login>().HasData({
            //    new Login
            //    {
            //        LoginId = 1,
            //        Email = "Test1@mail.dk"
            //        Pass
            //    }
            //})
        }
    }
}
