using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.UpdateDTO;

namespace OnlineTest.Services.Interface
{
    public interface ITestService
    {
        ResponseDTO GetTest();
        ResponseDTO GetTestUsingPagination(int PageNo, int RowsPerPage);
        ResponseDTO GetTestById(int id);
        ResponseDTO AddTest(AddTestDTO test);
        ResponseDTO UpdateTest(UpdateTestDTO test);
    }
}
