namespace OnlineTest.Model.Interfaces
{
    public interface ITestLinkRepository
    {
        TestEmailLink GetTestLink(Guid token);
        int AddTestLink(TestEmailLink testEmailLink);
        bool IsTestLinkExists(int testId, int userId);
        bool UpdateTestLink(TestEmailLink testLink);

    }
}
