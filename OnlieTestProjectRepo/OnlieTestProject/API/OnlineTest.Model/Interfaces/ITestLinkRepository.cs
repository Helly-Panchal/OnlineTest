namespace OnlineTest.Model.Interfaces
{
    public interface ITestLinkRepository
    {

        int AddTestLink(TestEmailLink testEmailLink);
        bool IsTestLinkExists(int testId, int userId);
        
    }
}
