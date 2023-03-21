using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class QuestionAnswerMapRepository : IQuestionAnswerMapRepository
    {
        #region Fields
        private readonly OnlineTestContext _context;
        #endregion

        #region Constructor
        public QuestionAnswerMapRepository(OnlineTestContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public int AddMap(QuestionAnswerMap map)
        {
            _context.QuestionAnswerMapping.Add(map);
            if(_context.SaveChanges() > 0)
            {
                return map.Id;
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
