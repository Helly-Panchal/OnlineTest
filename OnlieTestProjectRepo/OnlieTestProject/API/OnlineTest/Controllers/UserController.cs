using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Model;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.GetDTO;
using OnlineTest.Services.DTO.UpdateDTO;
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
        public ActionResult<GetUserDTO> GetUsers(int PageNo, int RowsPerPage)
        {
            return Ok(_userService.GetUsersUsingPagination(PageNo, RowsPerPage));
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
        public IActionResult DeleteUser(int Id)
        {
            return Ok(_userService.DeleteUser(Id));
        }
        #endregion
    }
}