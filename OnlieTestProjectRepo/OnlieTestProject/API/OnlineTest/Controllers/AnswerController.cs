using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Model.Interfaces;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.GetDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.Interface;
using OnlineTest.Services.Services;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        #region Fields
        public readonly IAnswerService _answerService;
        #endregion

        #region Constructor
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public ActionResult<GetAnswerDTO> GetAnswer()
        {
            return Ok(_answerService.GetAnswer());
        }

        [HttpPost]
        public IActionResult AddAnswer(AddAnswerDTO answer)
        {
            return Ok(_answerService.AddAnswer(answer));
        }

        [HttpPut]
        public IActionResult UpdateAnswer(UpdateAnswerDTO answer)
        {
            return Ok(_answerService.UpdateAnswer(answer));
        }
        #endregion
    }
}
