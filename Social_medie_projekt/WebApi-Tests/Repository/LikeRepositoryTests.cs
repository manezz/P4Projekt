namespace WebApi_Tests.Repository
{
    public class LikeRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        private readonly LikeRepository _likeRepository;

        public LikeRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "LikeRepositoryTests")
                .Options;

            _context = new(_options);

            _likeRepository = new LikeRepository(_context);
        }
    }
}
