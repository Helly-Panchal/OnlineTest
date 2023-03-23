namespace OnlineTest.Model.Interfaces
{
    public interface ITestRepository
    {
        IEnumerable<Test> GetTest();
        IEnumerable<Test> GetTestUsingPagination(int PageNo, int RowsPerPage);
        Test GetTestById(int id);
        IEnumerable<Test> GetTestsByTechnologyId(int technologyId);
        int AddTest(Test test);
        bool UpdateTest(Test test);
        bool DeleteTest(Test test);
        Test TestExists(Test test);
    }
}
