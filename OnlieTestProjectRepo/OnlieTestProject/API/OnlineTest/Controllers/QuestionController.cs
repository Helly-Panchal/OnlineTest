using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineTest.Model;
using OnlineTest.Services.DTO;
using OnlineTest.Services.Interface;

namespace OnlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        #region field
        public readonly IQuestionService _questionService;
        #endregion

        #region Constructor
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        #endregion

        #region Methods

        [HttpGet]
        public ActionResult<QuestionDTO> GetQuestion()
        {
            return Ok(_questionService.GetQuestion());
        }

        [HttpPost]
        public IActionResult AddQuestion(QuestionDTO question)
        {
            return Ok(_questionService.AddQuestion(question));
        }

        [HttpPut]
        public IActionResult UpdateQuestion(QuestionDTO question)
        {
            return Ok(_questionService.UpdateQuestion(question));
        }
    #endregion
    }
}
