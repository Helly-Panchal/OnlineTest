using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class TestLinkRepository : ITestLinkRepository
    {
        #region Fields
        private readonly OnlineTestContext _context;
        #endregion

        #region Constructor
        public TestLinkRepository(OnlineTestContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        public TestEmailLink GetTestLink(Guid token)
        {
            return _context.TestEmailLinks.FirstOrDefault(t => t.Token == token && t.ExpireOn > DateTime.UtcNow);
        }
        public int AddTestLink(TestEmailLink testEmailLink)
        {
            _context.Add(testEmailLink);
            if (_context.SaveChanges() > 0)
            {
                return testEmailLink.Id;
            }
            else
            {
                return 0;
            }
        }

        public bool IsTestLinkExists(int testId, int userId)
        {
            var result = _context.TestEmailLinks.FirstOrDefault(t => t.TestId == testId && t.UserId == userId && t.ExpireOn > DateTime.UtcNow);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateTestLink(TestEmailLink testLink)
        {
            _context.Entry(testLink).Property("AccessOn").IsModified = true;
            _context.Entry(testLink).Property("AccessCount").IsModified = true;
            return _context.SaveChanges() > 0;
        }
        #endregion

    }
}
