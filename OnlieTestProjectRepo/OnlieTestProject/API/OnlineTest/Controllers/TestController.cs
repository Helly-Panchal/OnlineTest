using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Services.DTO;
using OnlineTest.Services.Interface;
using OnlineTest.Services.Services;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        #region field
        public readonly ITestService _testService;
        #endregion

        #region Constructor
        public TestController(ITestService testService)
        {
            _testService = testService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public ActionResult<TestDTO> GetTest(int PageNo, int RowsPerPage)
        {
            return Ok(_testService.GetTestUsingPagination(PageNo, RowsPerPage));
        }

        [HttpPost]
        public IActionResult AddTest(TestDTO test)
        {
            return Ok(_testService.AddTest(test));
        }

        [HttpPut]
        public IActionResult UpdateTest(TestDTO test)
        {
            return Ok(_testService.UpdateTest);
        }

        #endregion
    }
}
