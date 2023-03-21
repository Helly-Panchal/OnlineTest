namespace OnlineTest.Model.Interfaces
{
    public interface ITechnologyRepository
    {
        IEnumerable<Technology> GetTechnology();
        IEnumerable<Technology> GetAllTechnologyUsingPagination(int PageNo, int RowsPerPage);
        Technology GetTechnologyById (int id);
        Technology GetTechnologyByName(string name);
        int AddTechnology(Technology technology);
        bool UpdateTechnology(Technology technology);   
        bool DeleteTechnology(Technology technology);
    }
}
