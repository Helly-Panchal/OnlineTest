using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model.Interfaces
{
    public interface ITechnologyRepository
    {
        IEnumerable<Technology> GetTechnology();
        IEnumerable<Technology> GetAllTechnologyUsingPagination(int PageNo, int RowsPerPage);
        Technology GetTechnologyById (int id);
        Technology GetTechnologyByName(string name);
        bool AddTechnology(Technology technology);
        bool UpdateTechnology(Technology technology);   
    }
}
