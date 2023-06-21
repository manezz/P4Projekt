namespace WebApi.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> FindByIdAsync(int id);
        Task<User> CreateAsync(User newUser);
        Task<User?> UpdateAsync(int id, User updatedUser);
        Task<User?> DeleteAsync(int id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User newUser)
        {
            //if (_context.User.Any(u => u.Login.Email == newUser.Login.Email))
            //{
            //    throw new Exception(String.Format("The email {0} is not available", newUser.Login.Email));
            //}

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            var user = await FindByIdAsync(newUser.UserId);

            return user!;
        }

        public async Task<User?> DeleteAsync(int id)
        {
            var user = await FindByIdAsync(id);

            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User?> FindByIdAsync(int id)
        {
            return await _context.User
                .Include(l => l.Login)
                .Include(p => p.Posts.OrderByDescending(posts => posts.Date))
                .ThenInclude(x => x.PostLikes)
                .Include(F => F.Follow)
                .Include(x => x.UserImage)
                .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.User
                .Include(l => l.Login)
                .Include(p => p.Posts)
                .ThenInclude(x => x.PostLikes)
                .Include(F => F.Follow)
                .Include(x => x.UserImage)
                .ToListAsync();
        }

        public async Task<User?> UpdateAsync(int id, User updatedUser)
        {
            var user = await FindByIdAsync(id);

            if (user != null)
            {
                user.UserName = updatedUser.UserName;
                user.UserImage.Image = updatedUser.UserImage.Image;

                await _context.SaveChangesAsync();
            }
            return user;
        }
    }
}
