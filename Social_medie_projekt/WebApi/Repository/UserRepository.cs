namespace WebApi.Repository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> FindUserByIdAsync(int id);
        Task<User> CreateUserAsync(User newUser);
        Task<User?> UpdateUserAsync(int id, User updatedUser);
        Task<User?> DeleteUserAsync(int id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            if (_context.User.Any(u => u.Login.Email == newUser.Login.Email))
            {
                throw new Exception(String.Format("The email {0} is not available", newUser.Login.Email));
            }

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            newUser = await FindUserByIdAsync(newUser.UserId);
            return newUser;
        }

        public async Task<User?> DeleteUserAsync(int id)
        {
            var user = await FindUserByIdAsync(id);

            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User?> FindUserByIdAsync(int id)
        {
            return await _context.User
                .Include(l => l.Login)
                .Include(p => p.Posts.OrderByDescending(posts => posts.Date))
                .ThenInclude(x => x.PostLikes)
                .Include(F => F.Follow)
                .Include(x => x.UserImage)
                .FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.User
                .Include(l => l.Login)
                .Include(p => p.Posts)
                .ThenInclude(x => x.PostLikes)
                .Include(F => F.Follow)
                .Include(x => x.UserImage)
                .ToListAsync();
        }

        public async Task<User?> UpdateUserAsync(int id, User updatedUser)
        {
            var user = await FindUserByIdAsync(id);

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
