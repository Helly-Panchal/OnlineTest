using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.Interface;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public IActionResult GetTestByTechnologyId(int id)
        {
            return Ok(_testService.GetTestByTechnologyId(id));
        }

        [HttpGet("paginated")]
        public IActionResult GetTestUsingPagination(int PageNumber, int RowsPerPage)
        {
            return Ok(_testService.GetTestUsingPagination(PageNumber, RowsPerPage));
        }

        [HttpGet("id")]
        public IActionResult GetTestById(int id)
        {
            return Ok(_testService.GetTestById(id));
        }

        [HttpPost]
        public IActionResult AddTest(AddTestDTO test)
        {
            return Ok(_testService.AddTest(Convert.ToInt32(User.FindFirstValue("Id")), test));
        }

        [HttpPut]
        public IActionResult UpdateTest(UpdateTestDTO test)
        {
            return Ok(_testService.UpdateTest(test));
        }

        [HttpDelete]
        public IActionResult DeleteTest(int id)
        {
            return Ok(_testService.DeleteTest(id));
        }
        #endregion
    }
}
