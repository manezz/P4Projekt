using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebApi_Tests.Database.Fixture;

namespace WebApi_Tests.Database
{
    public class LoginDatabaseTests : IClassFixture<TestDatabaseFixture>
    {
        //private readonly LoginRepository _loginRepository;

        public LoginDatabaseTests(TestDatabaseFixture fixture)
            => Fixture = fixture;

        //public LoginDatabaseTests(TestDatabaseFixture fixture)
        //{
        //    Fixture = fixture;

        //    var _context = Fixture.CreateContext();

        //    _loginRepository = new LoginRepository(_context);
        //}

        public TestDatabaseFixture Fixture { get; }

        [Fact]
        public async void GetAllAsync_ShouldReturnListOfLogins_WhereLoginsExists()
        {
            // Arange
            using var _context = Fixture.CreateContext();
            LoginRepository _loginRepository = new(_context);

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
