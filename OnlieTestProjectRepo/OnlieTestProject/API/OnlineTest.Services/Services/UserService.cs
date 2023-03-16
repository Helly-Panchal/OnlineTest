using Azure;
using Microsoft.EntityFrameworkCore.Query;
using OnlineTest.Model;
using OnlineTest.Model.Interfaces;
using OnlineTest.Model.Repository;
using OnlineTest.Services.DTO;
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
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ResponseDTO GetUsers()
        {
            var response = new ResponseDTO();
            try
            {
                var users = _userRepository.GetUsers().Select(user => new UserDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    MobileNo = user.MobileNo,
                    IsActive = user.IsActive
                }).ToList();
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
                var users = _userRepository.GetUsersUsingPagination(PageNo, RowsPerPage)
                .Select(user => new UserDTO()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    MobileNo = user.MobileNo,
                    IsActive = user.IsActive
                }).ToList();
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

                var result = new UserDTO()
                {
                    Id = userByEmail.Id,
                    Name = userByEmail.Name,
                    Email = userByEmail.Email,
                    MobileNo = userByEmail.MobileNo,
                    IsActive = userByEmail.IsActive
                };
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
                if(userById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "User not found";
                    return response;
                }

                var result = new UserDTO()
                {
                    Id = userById.Id,
                    Name = userById.Name,
                    Email = userById.Email,
                    MobileNo = userById.MobileNo,
                    IsActive = userById.IsActive
                };
                response.Status = 200;
                response.Data = result;
                response.Message = "Ok";
            }
            catch(Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO AddUser(UserDTO user)
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

                var addFlag = _userRepository.AddUser(new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    MobileNo = user.MobileNo,
                    Password = user.Password,
                    IsActive = true
                });

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
        public ResponseDTO UpdateUser(UserDTO user)
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

                var updateFlag = _userRepository.UpdateUser(new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    MobileNo = user.MobileNo,
                    Password = user.Password,
                    IsActive = user.IsActive
                });
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
        public ResponseDTO IsUserExists(TokenDTO user)
        {
            var response = new ResponseDTO();
            try
            {
                var result = _userRepository.GetUserByEmail(user.Username);
                if (result == null || result.Password != user.Password)
                {
                    response.Status = 404;
                    response.Message = "Not found";
                    response.Error = "User not found.";
                    return response;
                }
                new UserDTO()
                {
                    Id = result.Id,
                    Name = result.Name,
                    Email = result.Email,
                    MobileNo = result.MobileNo,
                    Password = result.Password,
                    IsActive = result.IsActive
                };
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
    }
}

