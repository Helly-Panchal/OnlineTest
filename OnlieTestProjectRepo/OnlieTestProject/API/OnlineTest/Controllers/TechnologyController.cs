using AutoMapper;
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
    public class TechnologyController : ControllerBase
    {
        #region field
        private readonly ITechnologyService _technologyService;
        #endregion

        #region Constructor
        public TechnologyController(ITechnologyService technologyService)
        {
            _technologyService = technologyService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public ActionResult<GetTechnologyDTO> GetTechnology(int PageNo, int RowsPerPage)
        {
            return Ok(_technologyService.GetAllTechnologyUsingPagination(PageNo, RowsPerPage));
        }

        [HttpPost]
        public IActionResult AddTechnology(AddTechnologyDTO technology)
        {
            return Ok(_technologyService.AddTechnology(technology));
        }

        [HttpPut]
        public IActionResult UpdateTechnology(UpdateTechnologyDTO technology)
        {
            return Ok(_technologyService.UpdateTechnology(technology));
        }
        #endregion
    }
}
