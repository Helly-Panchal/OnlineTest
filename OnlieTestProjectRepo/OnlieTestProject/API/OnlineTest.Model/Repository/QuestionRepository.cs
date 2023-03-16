using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly OnlineTestContext _context;
        public QuestionRepository(OnlineTestContext context)
        {
            _context = context;
        }
        public IEnumerable<Question> GetQuestion()
        {
            return _context.Questions.ToList();
        }

        public Question GetQuestionById(int id)
        {
            return _context.Questions.FirstOrDefault(x => x.Id == id);
        }

        public bool AddQuestion(Question question)
        {
            _context.Questions.Add(question);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateQuestion(Question question)
        {
            //_context.Questions.Update(question);
            _context.Entry(question).Property("QuestionName").IsModified = true;
            _context.Entry(question).Property("Que").IsModified = true;
            _context.Entry(question).Property("Type").IsModified = true;
            _context.Entry(question).Property("Weightage").IsModified = true;
            _context.Entry(question).Property("SortOrder").IsModified = true;
            _context.Entry(question).Property("IsActive").IsModified = true;

            return _context.SaveChanges() > 0;
        }

    }
}
