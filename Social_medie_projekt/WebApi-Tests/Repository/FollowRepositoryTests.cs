namespace WebApi_Tests.Repository
{
    public class FollowRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        private readonly FollowRepository _followRepository;

        public FollowRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "FollowRepositoryTests")
                .Options;

            _context = new(_options);

            _followRepository = new FollowRepository(_context);
        }
    }
}
