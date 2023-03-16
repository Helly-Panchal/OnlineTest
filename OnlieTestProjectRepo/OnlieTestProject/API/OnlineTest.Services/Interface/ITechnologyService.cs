using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Model;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.UpdateDTO;

namespace OnlineTest.Services.Interface
{
    public interface ITechnologyService
    {
        ResponseDTO GetTechnology();
        ResponseDTO GetAllTechnologyUsingPagination(int PageNo, int RowsPerPage);
        ResponseDTO GetTechnologyById(int id);
        ResponseDTO GetTechnologyByName(string name);
        ResponseDTO AddTechnology(AddTechnologyDTO technology);
        ResponseDTO UpdateTechnology(UpdateTechnologyDTO technology);
    }
}
