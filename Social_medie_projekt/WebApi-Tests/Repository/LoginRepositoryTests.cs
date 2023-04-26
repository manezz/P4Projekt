namespace WebApi_Tests.Repository
{
    public class LoginRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        private readonly LoginRepository _loginRepository;

        public LoginRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "LoginRepositoryTests")
                .Options;

            _context = new(_options);

            _loginRepository = new LoginRepository(_context);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfLogins_WhereLoginsExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            _context.Login.Add(
                new Login
                {
                    LoginId = 1,
                    Email = "Test1@mail.dk",
                    Password = "password",
                    Role = 0
                });
            _context.Login.Add(
                new Login
                {
                    LoginId = 2,
                    Email = "Test2@mail.dk",
                    Password = "password",
                    Role = (Role)1
                });
            await _context.SaveChangesAsync();

            // Act

            var result = await _loginRepository.GetAllAsync();

            // Assert

            Assert.NotNull(result);
            Assert.IsType<List<Login>>(result);
            Assert.Equal(2, result.Count);
        }
    }
}
