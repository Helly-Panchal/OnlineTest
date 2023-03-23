using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.Interface;
namespace OnlineTest.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region field
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Methods

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpGet("paginated")]
        public IActionResult GetUsersUsingPagination(int PageNo, int RowsPerPage)
        {
            return Ok(_userService.GetUsersUsingPagination(PageNo, RowsPerPage));
        }

        [HttpGet("id")]
        public IActionResult GetUserById(int id)
        {
            return Ok(_userService.GetUserById(id));
        }

        [HttpPost]
        public IActionResult AddUser(AddUserDTO user)
        {
            return Ok(_userService.AddUser(user));
        }

        [HttpPut]
        public IActionResult UpdateUser(UpdateUserDTO user)
        {
            return Ok(_userService.UpdateUser(user));
        }


        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            return Ok(_userService.DeleteUser(id));
        }
        #endregion
    }
}