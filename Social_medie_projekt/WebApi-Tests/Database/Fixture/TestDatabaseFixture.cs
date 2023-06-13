namespace WebApi_Tests.Database.Fixture
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BlogDBTest";

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
            => new(
                new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer(ConnectionString)
                .Options);
    }
}
