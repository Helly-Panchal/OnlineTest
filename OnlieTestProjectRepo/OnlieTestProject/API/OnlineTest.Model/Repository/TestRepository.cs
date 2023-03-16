using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly OnlineTestContext _context;
        public TestRepository(OnlineTestContext context)
        {
            _context = context;
        }

        public IEnumerable<Test> GetTest()
        {
            return _context.Tests.ToList();
        }
        public IEnumerable<Test> GetTestUsingPagination(int PageNo, int RowsPerPage)
        {
            return _context.Tests.Skip((PageNo - 1) * RowsPerPage).Take(RowsPerPage).ToList();
        }

        public Test GetTestById(int id)
        {
            return _context.Tests.FirstOrDefault(x => x.Id == id);
        }

        public bool AddTest(Test test)
        {
            _context.Tests.Add(test);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateTest(Test test)
        {
            _context.Entry(test).Property("TestName").IsModified = true;
            _context.Entry(test).Property("Description").IsModified = true;
            _context.Entry(test).Property("ExpireOn").IsModified = true;
            return _context.SaveChanges() > 0;
        }
    }
}
