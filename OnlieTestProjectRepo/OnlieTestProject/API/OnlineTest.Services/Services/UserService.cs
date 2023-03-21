using AutoMapper;
using OnlineTest.Model;
using OnlineTest.Model.Interfaces;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.GetDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.Interface;

namespace OnlineTest.Services.Services
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IHasherService _hasherService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        #endregion

        #region Constructors
        public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository ,IMapper mapper, IHasherService hasherService)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
            _hasherService = hasherService;
        }
        #endregion

        #region Methods
        public ResponseDTO GetUsers()
        {
            var response = new ResponseDTO();
            try
            {
                var result = _mapper.Map<List<GetUserDTO>>(_userRepository.GetUsers().ToList());

                response.Status = 200;
                response.Message = "Ok";
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO GetUsersUsingPagination(int PageNo, int RowsPerPage)
        {
            var response = new ResponseDTO();
            try
            {
                var result = _mapper.Map<List<GetUserDTO>>(_userRepository.GetUsersUsingPagination(PageNo, RowsPerPage).ToList());

                response.Status = 200;
                response.Message = "Ok";
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO GetUserByEmail(string email)
        {
            var response = new ResponseDTO();
            try
            {
                var userByEmail = _userRepository.GetUserByEmail(email);
                if (userByEmail == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "User not found";
                    return response;
                }

                var result = _mapper.Map<GetUserDTO>(userByEmail);

                response.Status = 200;
                response.Message = "Ok";
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO GetUserById(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var userById = _userRepository.GetUserById(id);
                if (userById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "User not found";
                    return response;
                }

                var result = _mapper.Map<GetUserDTO>(userById);

                response.Status = 200;
                response.Data = result;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO AddUser(AddUserDTO user)
        {
            var response = new ResponseDTO();
            try
            {
                var userByEmail = _userRepository.GetUserByEmail(user.Email);
                if (userByEmail != null)
                {
                    response.Status = 400;
                    response.Message = "Not created";
                    response.Error = "Email is already exists.";
                    return response;
                }

                user.Password = _hasherService.Hash(user.Password);
                var userId = _userRepository.AddUser(_mapper.Map<User>(user));
                if(userId == 0)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "User is not added";
                    return response;
                }
                if (user.IsAdmin)
                {
                    var roleAdmin = new UserRole
                    {
                        UserId = userId,
                        RoleId = 1
                    };
                    _userRoleRepository.AddRole(roleAdmin);
                }
                else
                {
                    var roleUser = new UserRole
                    {
                        UserId = userId,
                        RoleId = 2
                    };
                    _userRoleRepository.AddRole(roleUser);
                }
                response.Status = 201;
                response.Message = "Created";
                response.Data = userId;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO UpdateUser(UpdateUserDTO user)
        {
            var response = new ResponseDTO();
            try
            {
                var userById = _userRepository.GetUserById(user.Id);
                if (userById == null)
                {
                    response.Status = 404;
                    response.Message = "Not found";
                    response.Error = "User not found.";
                    return response;
                }

                var userByEmail = _userRepository.GetUserByEmail(user.Email);
                if (userByEmail != null && userByEmail.Id != user.Id)
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Email is already exists";
                    return response;
                }

                var updateFlag = _userRepository.UpdateUser(_mapper.Map<User>(user));

                if (updateFlag)
                {
                    response.Status = 204;
                    response.Message = "Updated Successfully";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Not Updated";
                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO DeleteUser(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var userById = _userRepository.GetUserById(id);
                if (userById == null)
                {
                    response.Status = 404;
                    response.Message = "Not found";
                    response.Error = "User not found.";
                    return response;
                }

                userById.IsActive = false;
                var deleteFlag = _userRepository.DeleteUser(userById);
                if (deleteFlag)
                {
                    response.Status = 200;
                    response.Message = "Deleted Successfully";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Deleted";
                    response.Error = "Not Deleted";
                }
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public GetUserDTO IsUserExists(TokenDTO user)
        {
            var result = _userRepository.GetUserByEmail(user.Username);
            if (result == null || result.Password != _hasherService.Hash(user.Password))
            {
                return null;
            }
            return _mapper.Map<GetUserDTO>(result);
        }
        #endregion
    }
}