using Microsoft.EntityFrameworkCore.Update.Internal;
using OnlineTest.Model;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.UpdateDTO;

namespace OnlineTest.Services.Interface
{
    public interface IAnswerService
    {
        ResponseDTO GetAnswer();
        ResponseDTO GetAnswerById(int id);
        ResponseDTO AddAnswer(AddAnswerDTO answer);
        ResponseDTO UpdateAnswer(UpdateAnswerDTO answer);
    }
}
