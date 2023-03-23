using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        #region Fields
        private readonly OnlineTestContext _context;
        #endregion

        #region Constructor
        public QuestionRepository(OnlineTestContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public IEnumerable<Question> GetQuestionByTestId(int testId)
        {
            return _context.Questions.Where(q => q.TestId == testId && q.IsActive == true).ToList();
        }

        public Question GetQuestionById(int id)
        {
            return _context.Questions.FirstOrDefault(q => q.Id == id && q.IsActive == true);
        }

        public int AddQuestion(Question question)
        {
            _context.Add(question);
            if(_context.SaveChanges() > 0)
            {
                return question.Id;
            }
            else
            {
                return 0;
            }
        }

        public bool UpdateQuestion(Question question)
        {
            _context.Entry(question).Property("QuestionName").IsModified = true;
            _context.Entry(question).Property("Que").IsModified = true;
            _context.Entry(question).Property("Weightage").IsModified = true;
            _context.Entry(question).Property("SortOrder").IsModified = true;
            return _context.SaveChanges() > 0;
        }

        public bool DeleteQuestion(Question question)
        {
            _context.Entry(question).Property("IsActive").IsModified = true;
            return _context.SaveChanges() > 0;
        }

        public Question QuestionExists(Question question)
        {
            var result = _context.Questions.FirstOrDefault(q => q.TestId == question.TestId && q.Que == question.Que && q.IsActive == true);
            return result;
        }
        #endregion
    }
}
