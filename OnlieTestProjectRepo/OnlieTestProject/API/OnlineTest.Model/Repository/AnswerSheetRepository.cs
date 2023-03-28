using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class AnswerSheetRepository : IAnswerSheetRepository
    {
        #region Fields
        private readonly OnlineTestContext _context;
        #endregion

        #region Constructor
        public AnswerSheetRepository(OnlineTestContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public bool AddAnswerSheet(List<AnswerSheet> answerSheet)
        {
            _context.AddRange(answerSheet);
            return _context.SaveChanges() > 0;
        }
        #endregion
    }
}
