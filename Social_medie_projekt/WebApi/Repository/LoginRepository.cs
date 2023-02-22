namespace WebApi.Repository
{

    public interface ILoginRepository
    {
        Task<Login> RegisterAsync(Login newUser);
        Task<List<Login>> GetAllLoginAsync();
        Task<Login> CreateLoginAsync(Login newLogin);
        Task<Login?> FindLoginByIdAsync(int loginId);
        Task<Login?> FindLoginByEmailAsync(string email);
        Task<Login?> UpdateLoginById(int loginId, Login updatedLogin);
        Task<Login?> DeleteLoginByIdAsync(int loginId);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly DatabaseContext _context;

        public LoginRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Login> RegisterAsync(Login newUser)
        {
            if (_context.User.Any(u => u.Login.Email == newUser.Email))
            {
                throw new Exception(string.Format("The email {0} is not available", newUser.Email));
            }

            _context.Login.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<List<Login>> GetAllLoginAsync()
        {
            return await _context.Login
                .Include(l => l.User)
                .Include(p => p.User.Posts.OrderByDescending(Posts => Posts.Date))
                .ToListAsync();
        }

        public async Task<Login> CreateLoginAsync(Login newLogin)
        {
            _context.Login.Add(newLogin);
            await _context.SaveChangesAsync();
            return newLogin;
        }

        public async Task<Login?> FindLoginByIdAsync(int loginId)
        {
            return await _context.Login
                .Include(l => l.User)
                .Include(p => p.User.Posts)
                .FirstOrDefaultAsync(x => x.LoginId == loginId);
        }

        public async Task<Login?> FindLoginByEmailAsync(string email)
        {
            return await _context.Login
                .Include(L => L.User)
                .Include(P => P.User.Posts.OrderByDescending(Posts => Posts.Date))
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Login?> UpdateLoginById(int loginId, Login updatedLogin)
        {
            var login = await FindLoginByIdAsync(loginId);

            if (login != null)
            {
                login.Type = updatedLogin.Type;
                login.Email = updatedLogin.Email;
                login.Password = updatedLogin.Password;

                await _context.SaveChangesAsync();
            }
            return login;
        }

        public async Task<Login?> DeleteLoginByIdAsync(int loginId)
        {
            var login = await FindLoginByIdAsync(loginId);

            if (login != null)
            {
                _context.Remove(login);
                await _context.SaveChangesAsync();
            }

            return login;
        }
    }
}
