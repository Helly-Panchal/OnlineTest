using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.GetDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.Interface;
using OnlineTest.Services.Services;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        #region field
        private readonly ITestService _testService;
        #endregion

        #region Constructor
        public TestController(ITestService testService)
        {
            _testService = testService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public ActionResult<GetTestDTO> GetTest(int PageNo, int RowsPerPage)
        {
            return Ok(_testService.GetTestUsingPagination(PageNo, RowsPerPage));
        }

        [HttpPost]
        public IActionResult AddTest(AddTestDTO test)
        {
            return Ok(_testService.AddTest(test));
        }

        [HttpPut]
        public IActionResult UpdateTest(UpdateTestDTO test)
        {
            return Ok(_testService.UpdateTest);
        }

        #endregion
    }
}
