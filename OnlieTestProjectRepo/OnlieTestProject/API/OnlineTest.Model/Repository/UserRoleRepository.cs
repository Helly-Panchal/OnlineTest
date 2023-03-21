using OnlineTest.Model.Interfaces;

namespace OnlineTest.Model.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        #region Fields
        private readonly OnlineTestContext _context;
        #endregion

        #region Constructor
        public UserRoleRepository(OnlineTestContext context)
        {
            _context = context;
        }
        #endregion

        #region Enum
        private enum RoleTypeMap 
        {
            Admin = 1,
            User = 2
        }
        #endregion

        #region Methods
        public List<String> GetRoles(int userId)
        {
            var result = _context.UserRoles.Where(u => u.UserId == userId).ToList();
            List<String> roles = new List<String>();
            foreach (var row in result)
            {
                roles.Add(((RoleTypeMap)row.RoleId).ToString());
            }
            return roles;
        }

        public bool AddRole(UserRole userRole)
        {
            _context.Add(userRole);
            return _context.SaveChanges() > 0;
        }
        #endregion
    }
}
