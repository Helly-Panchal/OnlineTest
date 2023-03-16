using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.UpdateDTO;

namespace OnlineTest.Services.Interface
{
    public interface IQuestionService
    {
        ResponseDTO GetQuestion();
        ResponseDTO GetQuestionById(int id);
        ResponseDTO AddQuestion(AddQuestionDTO question);
        ResponseDTO UpdateQuestion(UpdateQuestionDTO question);
    }
}
