namespace OnlineTest.Model.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsersUsingPagination(int PageNo, int RowsPerPage);
        User GetUserById(int id);
        User GetUserByEmail(string email);
        int AddUser(User user);
        bool UpdateUser(User user); 
        bool DeleteUser(User user);
    }
}
