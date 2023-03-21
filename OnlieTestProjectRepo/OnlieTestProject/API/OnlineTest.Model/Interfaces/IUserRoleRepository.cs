namespace OnlineTest.Model.Interfaces
{
    public interface IUserRoleRepository
    {
        List<String> GetRoles(int userId);
        bool AddRole(UserRole userRole);
    }
}
