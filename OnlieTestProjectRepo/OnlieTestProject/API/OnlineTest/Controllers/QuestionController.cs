using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Model;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.GetDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.Interface;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        #region field
        private readonly IQuestionService _questionService;
        #endregion

        #region Constructor
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        #endregion

        #region Methods

        [HttpGet]
        public ActionResult<GetQuestionDTO> GetQuestion()
        {
            return Ok(_questionService.GetQuestion());
        }

        [HttpPost]
        public IActionResult AddQuestion(AddQuestionDTO question)
        {
            return Ok(_questionService.AddQuestion(question));
        }

        [HttpPut]
        public IActionResult UpdateQuestion(UpdateQuestionDTO question)
        {
            return Ok(_questionService.UpdateQuestion(question));
        }
    #endregion
    }
}
