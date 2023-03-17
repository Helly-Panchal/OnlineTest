using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly OnlineTestContext _context;
        public AnswerRepository(OnlineTestContext context)
        {
            _context = context;
        }
        public IEnumerable<Answer> GetAnswer()
        {
            return _context.Answers.ToList();
        }

        public Answer GetAnswerById(int id)
        {
            return _context.Answers.FirstOrDefault( x => x.Id == id);
        }
        public bool AddAnswer(Answer answer)
        {
            _context.Answers.Add(answer);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateAnswer(Answer answer)
        {
            //_context.Answers.Update(answer);
            _context.Entry(answer).Property("Ans").IsModified = true;
            return _context.SaveChanges() > 0;
        }
    }
}
