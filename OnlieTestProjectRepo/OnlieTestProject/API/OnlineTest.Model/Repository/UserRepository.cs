using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Fields
        private readonly OnlineTestContext _context;
        #endregion

        #region Constructor
        public UserRepository(OnlineTestContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public IEnumerable<User> GetUsers()
        {
            return _context.Users.Where(u => u.IsActive == true).ToList();
        }
        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id && u.IsActive == true);
        }
        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.IsActive == true);
        }
        public IEnumerable<User> GetUsersUsingPagination(int PageNo, int RowsPerPage)
        {
            return _context.Users.Where(u => u.IsActive == true).Skip((PageNo - 1) * RowsPerPage).Take(RowsPerPage).ToList();
        }
        public int AddUser(User user)
        {
            _context.Users.Add(user);
            if(_context.SaveChanges() > 0)
            {
                return user.Id;
            }
            else
            {
                return 0;
            }
        }
        public bool UpdateUser(User user)
        {
            _context.Entry(user).Property("Email").IsModified = true;
            _context.Entry(user).Property("Name").IsModified = true;
            _context.Entry(user).Property("MobileNo").IsModified = true;

            return _context.SaveChanges() > 0;
        }
        public bool DeleteUser(User user)
        {
            _context.Entry(user).Property("IsActive").IsModified = true;
            return _context.SaveChanges() > 0;
        }
        #endregion
    }
}
