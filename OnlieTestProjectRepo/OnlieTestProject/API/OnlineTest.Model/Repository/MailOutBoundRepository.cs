using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class MailOutBoundRepository : IMailOutBoundRepository
    {
        #region Fields
        private readonly OnlineTestContext _context;
        #endregion

        #region Constructor
        public MailOutBoundRepository(OnlineTestContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public int AddMailOutBound(MailOutBound mailOutBound)
        {
            _context.Add(mailOutBound);
            if (_context.SaveChanges() > 0)
                return mailOutBound.Id;
            else
                return 0;
        }
        #endregion
    }
}
