using OnlineTest.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineTestContext _context;
        public UserRepository(OnlineTestContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool AddUser(User user)
        {
            _context.Users.Add(user);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateUser(User user)
        {
            _context.Entry("user").Property("Email");
            _context.Entry("user").Property("Name");
            _context.Entry("user").Property("MobileNo");

            return _context.SaveChanges() > 0;
        }

        public bool DeleteUser(int id)
        {
            var del = _context.Users.Find(id);
            if (del != null)
            {
                _context.Users.Remove(del);
            }
            return _context.SaveChanges() > 0;
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

        public IEnumerable<User> GetUsersUsingPagination(int PageNo, int RowsPerPage)
        {
            return _context.Users.Skip((PageNo - 1) * RowsPerPage).Take(RowsPerPage).ToList();
        }
    }
}
