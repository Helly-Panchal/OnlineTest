using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Services.DTO;

namespace OnlineTest.Services.Interface
{
    public interface IQuestionService
    {
        ResponseDTO GetQuestion();
        ResponseDTO GetQuestionById(int id);
        ResponseDTO AddQuestion(QuestionDTO question);
        ResponseDTO UpdateQuestion(QuestionDTO question);
    }
}
