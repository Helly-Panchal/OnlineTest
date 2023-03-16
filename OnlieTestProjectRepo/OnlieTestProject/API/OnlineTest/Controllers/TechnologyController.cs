using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Services.DTO;
using OnlineTest.Services.Interface;
using OnlineTest.Services.Services;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologyController : ControllerBase
    {
        #region field
        public readonly ITechnologyService _technologyService;
        #endregion

        #region Constructor
        public TechnologyController(ITechnologyService technologyService)
        {
            _technologyService = technologyService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public ActionResult<TechnologyDTO> GetTechnology(int PageNo, int RowsPerPage)
        {
            return Ok(_technologyService.GetAllTechnologyUsingPagination(PageNo, RowsPerPage));
        }

        [HttpPost]
        public IActionResult AddTechnology(TechnologyDTO technology)
        {
            return Ok(_technologyService.AddTechnology(technology));
        }

        [HttpPut]
        public IActionResult UpdateTechnology(TechnologyDTO technology)
        {
            return Ok(_technologyService.UpdateTechnology(technology));
        }
        #endregion
    }
}
