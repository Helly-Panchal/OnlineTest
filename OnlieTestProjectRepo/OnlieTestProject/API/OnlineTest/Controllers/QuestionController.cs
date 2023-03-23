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
        public IActionResult GetQuestionByTestId(int id)
        {
            return Ok(_questionService.GetQuestionByTestId(id));
        }

        [HttpGet("id")]
        public IActionResult GetQuestionById(int id)
        {
            return Ok(_questionService.GetQuestionById(id));
        }

        [HttpPost]
        public IActionResult AddQuestion(AddQuestionDTO question)
        {
            return Ok(_questionService.AddQuestion(Convert.ToInt32(User.FindFirstValue("Id")), question));
        }

        [HttpPut]
        public IActionResult UpdateQuestion(UpdateQuestionDTO question)
        {
            return Ok(_questionService.UpdateQuestion(question));
        }

        [HttpDelete]
        public IActionResult DeleteQuestion(int id)
        {
            return Ok(_questionService.DeleteQuestion(id));
        }
        #endregion
    }
}
