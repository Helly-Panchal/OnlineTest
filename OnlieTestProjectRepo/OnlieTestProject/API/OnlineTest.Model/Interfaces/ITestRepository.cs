using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model.Interfaces
{
    public interface ITestRepository
    {
        IEnumerable<Test> GetTest();
        IEnumerable<Test> GetTestUsingPagination(int PageNo, int RowsPerPage);
        Test GetTestById(int id);
        bool AddTest(Test test);
        bool UpdateTest(Test test);
    }
}
