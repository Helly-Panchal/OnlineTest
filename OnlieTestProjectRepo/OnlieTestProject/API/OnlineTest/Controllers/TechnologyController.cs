using Microsoft.AspNetCore.Mvc;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.Interface;

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
        public IActionResult GetTechnology()
        {
            return Ok(_technologyService.GetTechnology());
        }

        [HttpGet("paginated")]
        public IActionResult GetAllTechnologyUsingPagination(int PageNumber, int RowsPerPage)
        {
            return Ok(_technologyService.GetAllTechnologyUsingPagination(PageNumber, RowsPerPage));
        }

        [HttpGet("id")]
        public IActionResult GetTechnologyById(int id)
        {
            return Ok(_technologyService.GetTechnologyById(id));
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

        [HttpDelete]
        public IActionResult DeleteTechnology(int id)
        {
            return Ok(_technologyService.DeleteTechnology(id));
        }
        #endregion
    }
}
