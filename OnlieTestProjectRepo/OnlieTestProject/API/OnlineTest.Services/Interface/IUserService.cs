using OnlineTest.Model;
using OnlineTest.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Services.Interface
{
    public interface IUserService
    {
        ResponseDTO GetUsers();
        ResponseDTO GetUsersUsingPagination(int PageNo, int RowsPerPage);
        ResponseDTO GetUserById(int id);
        ResponseDTO GetUserByEmail(string email);
        ResponseDTO AddUser(UserDTO user);
        ResponseDTO UpdateUser(UserDTO user);
        ResponseDTO DeleteUser(int Id);
        ResponseDTO IsUserExists(TokenDTO model);

    }
}
