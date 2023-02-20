namespace WebApi.Repository
{

    public interface ILoginRepository
    {
        Task<Login> RegisterAsync(Login newUser);
        Task<List<Login>> GetAllLoginAsync();
        Task<Login?> FindLoginByIdAsync(int loginId);
        Task<Login> FindLoginByEmailAsync(string email);
        Task<Login?> UpdateLoginById(int loginId, Login updatedLogin);
        Task<Login?> DeleteLoginByIdAsync(int loginId);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly DatabaseContext _context;

        public LoginRepository(DatabaseContext context){ _context = context; }



        public async Task<Login> RegisterAsync(Login newUser)
        {
            if (_context.User.Any(u => u.Login.Email.ToLower() == newUser.Email.ToLower()))
            {
                throw new Exception(String.Format("User already exists", newUser.Email));
            }

            _context.Login.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }




        public async Task<List<Login>> GetAllLoginAsync()
        {
            return await _context.Login
                .Include(l => l.User)
                .ToListAsync();
        }

        public async Task<Login?> FindLoginByIdAsync(int loginId)
        {
            return await _context.Login
                .Include(l => l.User)
                .FirstOrDefaultAsync(x => x.LoginId == loginId);
        }

        public async Task<Login> FindLoginByEmailAsync(string email)
        {   
            return await _context.Login
                .Include(L => L.User)
                .Include(p => p.User.Posts)
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
