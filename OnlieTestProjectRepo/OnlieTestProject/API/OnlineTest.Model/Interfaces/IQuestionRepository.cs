using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model.Interfaces
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetQuestion();
        Question GetQuestionById(int id);
        bool AddQuestion(Question question);
        bool UpdateQuestion(Question question);
    }
}
