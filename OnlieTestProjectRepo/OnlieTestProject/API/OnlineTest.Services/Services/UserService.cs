using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore.Query;
using OnlineTest.Model;
using OnlineTest.Model.Interfaces;
using OnlineTest.Model.Repository;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.GetDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Services.Services
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        #endregion

        #region Constructors
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public ResponseDTO GetUsers()
        {
            var response = new ResponseDTO();
            try
            {
                var users = _mapper.Map<List<GetUserDTO>>(_userRepository.GetUsers().ToList());

                response.Status = 200;
                response.Data = users;
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
        public ResponseDTO GetUsersUsingPagination(int PageNo, int RowsPerPage)
        {
            var response = new ResponseDTO();
            try
            {
                var users = _mapper.Map<List<GetUserDTO>>(_userRepository.GetUsersUsingPagination(PageNo, RowsPerPage).ToList());

                response.Status = 200;
                response.Data = users;
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

                var addFlag = _userRepository.AddUser(_mapper.Map<User>(user));

                if (addFlag)
                {
                    response.Status = 204;
                    response.Message = "Created";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "User is not added";
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
        public ResponseDTO DeleteUser(int Id)
        {
            var response = new ResponseDTO();
            try
            {
                var userById = GetUserById(Id);
                if (userById == null)
                {
                    response.Status = 404;
                    response.Message = "Not found";
                    response.Error = "User not found.";
                    return response;
                }
                var deleteFlag = _userRepository.DeleteUser(Id);
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
            if (result == null || result.Password != user.Password)
                return null;
            return _mapper.Map<GetUserDTO>(result);
        }
        #endregion
    }
}