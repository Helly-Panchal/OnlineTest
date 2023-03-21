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
        #region Fields
        private readonly OnlineTestContext _context;
        #endregion

        #region Constructor
        public TestRepository(OnlineTestContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        public IEnumerable<Test> GetTest()
        {
            return _context.Tests.Where(t => t.IsActive == true).ToList();
        }
        public IEnumerable<Test> GetTestUsingPagination(int PageNo, int RowsPerPage)
        {
            return _context.Tests.Where(t => t.IsActive == true).Skip((PageNo - 1) * RowsPerPage).Take(RowsPerPage).ToList();
        }

        public Test GetTestById(int id)
        {
            return _context.Tests.FirstOrDefault(t => t.Id == id && t.IsActive == true);
        }

        public IEnumerable<Test> GetTestsByTechnologyId(int technologyId)
        {
            return _context.Tests.Where(t => t.TechnologyId == technologyId && t.IsActive == true).ToList();
        }
        public int AddTest(Test test)
        {
            _context.Tests.Add(test);
            if(_context.SaveChanges() > 0)
            {
                return test.Id;
            }
            else
            {
                return 0;
            }
        }

        public bool UpdateTest(Test test)
        {
            _context.Entry(test).Property("TestName").IsModified = true;
            _context.Entry(test).Property("Description").IsModified = true;
            _context.Entry(test).Property("ExpireOn").IsModified = true;
            return _context.SaveChanges() > 0;
        }

        public bool DeleteTest(Test test)
        {
            _context.Entry(test).Property("IsActive").IsModified=true;
            return _context.SaveChanges() > 0;
        }
        #endregion
    }
}
