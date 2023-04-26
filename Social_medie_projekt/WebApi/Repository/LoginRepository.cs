namespace WebApi.Repository
{

    public interface ILoginRepository
    {
        Task<Login> CreateAsync(Login newUser);
        Task<List<Login>> GetAllAsync();
        Task<Login?> GetByIdAsync(int loginId);
        Task<Login?> GetByEmailAsync(string email);
        Task<Login?> UpdateByIdAsync(int loginId, Login updatedLogin);
        Task<Login?> DeleteByIdAsync(int loginId);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly DatabaseContext _context;

        public LoginRepository(DatabaseContext context) { _context = context; }

        public async Task<Login> CreateAsync(Login newUser)
        {
            if (_context.User.Any(u => u.Login.Email.ToLower() == newUser.Email.ToLower()))
            {
                throw new Exception(string.Format("User already exists", newUser.Email));
            }

            _context.Login.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<List<Login>> GetAllAsync()
        {
            return await _context.Login
                .Include(login => login.User)
                .Include(posts => posts.User.Posts.OrderByDescending(Post => Post.Date))
                .ThenInclude(post => post.PostLikes)
                .Include(x => x.User.UserImage)
                .ToListAsync();
        }

        public async Task<Login?> GetByIdAsync(int loginId)
        {
            return await _context.Login
                .Include(login => login.User)
                .Include(posts => posts.User.Posts)
                .ThenInclude(post => post.PostLikes)
                .Include(x => x.User.UserImage)
                .FirstOrDefaultAsync(login => login.LoginId == loginId);
        }

        public async Task<Login?> GetByEmailAsync(string email)
        {
            return await _context.Login
                .Include(L => L.User)
                .Include(P => P.User.Posts.OrderByDescending(post => post.Date))
                .ThenInclude(post => post.PostLikes)
                .Include(x => x.User.UserImage)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Login?> UpdateByIdAsync(int loginId, Login updatedLogin)
        {
            var login = await GetByIdAsync(loginId);

            if (login != null)
            {
                login.Role = updatedLogin.Role;
                login.Email = updatedLogin.Email;
                login.Password = updatedLogin.Password;

                await _context.SaveChangesAsync();
            }
            return login;
        }

        public async Task<Login?> DeleteByIdAsync(int loginId)
        {
            var login = await GetByIdAsync(loginId);

            if (login != null)
            {
                _context.Remove(login);
                await _context.SaveChangesAsync();
            }

            return login;
        }
    }
}
