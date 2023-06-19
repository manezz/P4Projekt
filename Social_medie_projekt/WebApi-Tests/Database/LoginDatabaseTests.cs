namespace WebApi_Tests.Database
{
    public class LoginDatabaseTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly LoginRepository _loginRepository;
        private readonly DatabaseContext _context;
        private readonly TestDatabaseFixture _fixture;

        public LoginDatabaseTests(TestDatabaseFixture fixture)
        {
            _fixture = fixture;

            _context = _fixture.CreateContext();

            _loginRepository = new LoginRepository(_context);
        }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfLogins_WhereLoginsExists()
        {
            // Arange
            _context.Database.BeginTransaction();

            string email1 = "Test50@mail.dk";
            string email2 = "Test51@mail.dk";

            _context.Login.Add(
                new Login
                {
                    Email = email1,
                    Password = "password",
                    Role = 0
                });
            _context.Login.Add(
                new Login
                {
                    Email = email2,
                    Password = "password",
                    Role = (Role)1
                });
            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();

            // Act
            var result = await _loginRepository.GetAllAsync();

            var logins = _context.Login.Where(x => x.Email == email1 || x.Email == email2).ToList();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Login>>(result);
            Assert.Equal(2, logins.Count);
        }
    }
}
