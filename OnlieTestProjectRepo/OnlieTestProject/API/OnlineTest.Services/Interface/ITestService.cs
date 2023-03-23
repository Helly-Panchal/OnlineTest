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
        ResponseDTO GetTestByTechnologyId(int technologyId);
        ResponseDTO AddTest(int userId, AddTestDTO test);
        ResponseDTO UpdateTest(UpdateTestDTO test);
        ResponseDTO DeleteTest(int id);

        //TestEmailLink
        ResponseDTO AddTestLink(int adminId, int testId, string userEmail);
    }
}
