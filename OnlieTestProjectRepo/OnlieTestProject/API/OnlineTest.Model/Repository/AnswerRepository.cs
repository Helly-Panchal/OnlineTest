using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        #region Fields
        private readonly OnlineTestContext _context;
        #endregion

        #region Constructor
        public AnswerRepository(OnlineTestContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public IEnumerable<Answer> GetAnswer()
        {
            return _context.Answers.Where( a => a.IsActive == true).ToList();
        }

        public Answer GetAnswerById(int id)
        {
            return _context.Answers.FirstOrDefault( a => a.Id == id && a.IsActive == true);
        }

        public IEnumerable<Answer> GetAnswerByQuestionId(int questionId)
        {
            return (from QAMap in _context.QuestionAnswerMapping
                    join Ans in _context.Answers
                    on QAMap.AnswerId equals Ans.Id
                    where QAMap.QuestionId == questionId && QAMap.IsActive == true
                    select new Answer
                    {
                        Id = Ans.Id,
                        Ans = Ans.Ans,
                        IsActive = Ans.IsActive,
                        CreatedBy = Ans.CreatedBy,
                        CreateOn = Ans.CreateOn
                    }).ToList();
        }

        public int AddAnswer(Answer answer)
        {
            _context.Add(answer);
            if(_context.SaveChanges() > 0)
            {
                return answer.Id;
            }
            else
            {
                return 0;
            }
        }

        public bool UpdateAnswer(Answer answer)
        {
            _context.Entry(answer).Property("Ans").IsModified = true;
            return _context.SaveChanges() > 0;
        }

        public bool DeleteAnswer(Answer answer) 
        {
            _context.Entry(answer).Property("IsActive").IsModified = true;
            return _context.SaveChanges() > 0;
        }

        public Answer AnswerExists(int testId, int questionId, string ans)
        {
            var result = (from QAMap in _context.QuestionAnswerMapping
                          join Ans in _context.Answers
                          on QAMap.AnswerId equals Ans.Id
                          where QAMap.TestId == testId && QAMap.QuestionId == questionId && Ans.Ans == ans
                          select new Answer
                          {
                              Id = Ans.Id
                          }).FirstOrDefault();
            return result;
        }
        #endregion
    }
}
