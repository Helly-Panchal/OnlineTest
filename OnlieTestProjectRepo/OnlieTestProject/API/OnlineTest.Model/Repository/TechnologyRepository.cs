using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class TechnologyRepository : ITechnologyRepository
    {
       
        private readonly OnlineTestContext _context;
 
        public TechnologyRepository(OnlineTestContext context)
        {
            _context = context;
        }

        public IEnumerable<Technology> GetTechnology()
        {
            return _context.Technologies.ToList();
        }

        public IEnumerable<Technology> GetAllTechnologyUsingPagination(int PageNo, int RowsPerPage)
        {
            return _context.Technologies.Skip((PageNo - 1) * RowsPerPage).Take(RowsPerPage).ToList();
        }

        public Technology GetTechnologyById(int id)
        {
            return _context.Technologies.FirstOrDefault(x => x.Id == id);
        }

        public Technology GetTechnologyByName(string name)
        {
            return _context.Technologies.FirstOrDefault(x => x.TechName == name);
        }

        public bool AddTechnology(Technology technology)
        {
            _context.Technologies.Add(technology);
            return _context.SaveChanges() > 0;

        }

        public bool UpdateTechnology(Technology technology)
        {
            //_context.Technologies.Update(technology);
            _context.Entry(technology).Property("TechName").IsModified = true;
            _context.Entry(technology).Property("ModifiedBy").IsModified = true;
            _context.Entry(technology).Property("ModifiedOn").IsModified = true;
            return _context.SaveChanges() > 0;
        }
    }
}
