using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsersUsingPagination(int PageNo, int RowsPerPage);
        User GetUserById(int id);
        User GetUserByEmail(string email);
        bool AddUser(User user);
        bool UpdateUser(User user); 
        bool DeleteUser(int Id);
    }
}
