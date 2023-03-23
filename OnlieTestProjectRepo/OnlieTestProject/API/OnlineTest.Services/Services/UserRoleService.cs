using OnlineTest.Model.Interfaces;
using OnlineTest.Services.Interface;

namespace OnlineTest.Services.Services
{
    public class UserRoleService : IUserRoleService
    {
        #region Fields
        private readonly IUserRoleRepository _userRoleRepository;
        #endregion

        #region Controler
        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        #endregion

        #region Methods
        public List<string> GetRoles(int userId)
        {
            return _userRoleRepository.GetRoles(userId);
        }
        #endregion
    }
}
