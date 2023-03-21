using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class TechnologyRepository : ITechnologyRepository
    {
        #region Fields
        private readonly OnlineTestContext _context;

        #endregion

        #region Constructor
        public TechnologyRepository(OnlineTestContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods

        public IEnumerable<Technology> GetTechnology()
        {
            return _context.Technologies.Where(t => t.IsActive == true).ToList();
        }

        public IEnumerable<Technology> GetAllTechnologyUsingPagination(int PageNo, int RowsPerPage)
        {
            return _context.Technologies.Where(t => t.IsActive == true).Skip((PageNo - 1) * RowsPerPage).Take(RowsPerPage).ToList();
        }

        public Technology GetTechnologyById(int id)
        {
            return _context.Technologies.FirstOrDefault(t => t.Id == id && t.IsActive == true);
        }

        public Technology GetTechnologyByName(string name)
        {
            return _context.Technologies.FirstOrDefault(t => t.TechName == name && t.IsActive == true);
        }

        public int AddTechnology(Technology technology)
        {
            _context.Add(technology);
            if (_context.SaveChanges() > 0)
            {
                return technology.Id;
            }
            else
            {
                return 0;
            }

        }

        public bool UpdateTechnology(Technology technology)
        {
            _context.Entry(technology).Property("TechName").IsModified = true;
            _context.Entry(technology).Property("ModifiedBy").IsModified = true;
            _context.Entry(technology).Property("ModifiedOn").IsModified = true;
            return _context.SaveChanges() > 0;
        }

        public bool DeleteTechnology(Technology technology)
        {
            _context.Entry(technology).Property("IsActive").IsModified = true;
            return _context.SaveChanges() > 0;
        }
        #endregion
    }
}
