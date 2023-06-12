namespace WebApi_Tests.Database.Fixture
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = @"Server=(localdb)\\MSSQLLocalDB;Database=BlogDBTest;Trusted_Connection=True";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public TestDatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public DatabaseContext CreateContext()
            => new DatabaseContext(
                new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer(ConnectionString)
                .Options);
    }
}
