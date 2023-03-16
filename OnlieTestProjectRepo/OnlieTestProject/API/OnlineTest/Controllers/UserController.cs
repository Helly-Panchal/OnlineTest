using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Model;
using OnlineTest.Services.DTO;
using OnlineTest.Services.Interface;
using Xamarin.Essentials;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region field
        public readonly IUserService _userService;
        #endregion

        #region Constructor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Methods

        [HttpGet]
        public ActionResult<UserDTO> GetUsers(int PageNo, int RowsPerPage)
        {
            return Ok(_userService.GetUsersUsingPagination(PageNo, RowsPerPage));
        }

        [HttpPost]
        public IActionResult AddUser(UserDTO user)
        {
            return Ok(_userService.AddUser(user));
        }

        [HttpPut]
        public IActionResult UpdateUser(UserDTO user)
        {
            return Ok(_userService.UpdateUser(user));
        }


        [HttpDelete]
        public IActionResult DeleteUser(int Id)
        {
            return Ok(_userService.DeleteUser(Id));
        }
        #endregion
    }
}