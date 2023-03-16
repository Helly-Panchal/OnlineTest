using OnlineTest.Model;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.GetDTO;
using OnlineTest.Services.DTO.UpdateDTO;
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
        ResponseDTO AddUser(AddUserDTO user);
        ResponseDTO UpdateUser(UpdateUserDTO user);
        ResponseDTO DeleteUser(int Id);
        GetUserDTO IsUserExists(TokenDTO model);

    }
}
