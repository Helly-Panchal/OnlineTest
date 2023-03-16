using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineTest.Model;
using OnlineTest.Model.Interfaces;
using OnlineTest.Model.Repository;
using OnlineTest.Services.DTO;
using OnlineTest.Services.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineTest.Services.Services
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        private readonly ITechnologyRepository _technologyRepository;

        public TestService(ITestRepository testRepository, ITechnologyRepository technologyRepository)
        {
            _testRepository = testRepository;
            _technologyRepository = technologyRepository;
        }

        public ResponseDTO GetTest()
        {
            var response = new ResponseDTO();
            try
            {
                var tests = _testRepository.GetTest().Select(test => new TestDTO()
                {
                    Id = test.Id,
                    TestName = test.TestName,
                    Description = test.Description,
                    ExpireOn = test.ExpireOn,
                    TechnologyId = test.TechnologyId
                }).ToList();
                response.Status = 200;
                response.Data = tests;
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

        public ResponseDTO GetTestUsingPagination(int PageNo, int RowsPerPage)
        {
            var response = new ResponseDTO();
            try
            {
                var test = _testRepository.GetTestUsingPagination(PageNo, RowsPerPage).Select(s => new TestDTO
                {
                    Id = s.Id,
                    TestName = s.TestName,
                    Description = s.Description,
                    ExpireOn = s.ExpireOn,
                    TechnologyId = s.TechnologyId
                }).ToList();
                response.Status = 200;
                response.Data = test;
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

        public ResponseDTO GetTestById(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var testById = _testRepository.GetTestById(id);
                if (testById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Test not found";
                    return response;
                }

                var result = new TestDTO()
                {
                    Id = testById.Id,
                    TestName = testById.TestName,
                    Description = testById.Description,
                    ExpireOn = testById.ExpireOn,
                    TechnologyId = testById.TechnologyId
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

        public ResponseDTO AddTest(TestDTO test)
        {
            var response = new ResponseDTO();
            try
            {
                //check technology is exists or not for which test is added.
                var technologyById = _technologyRepository.GetTechnologyById(test.TechnologyId);
                if (technologyById == null)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Technology not found";
                    return response;
                }

                var addFlag = _testRepository.AddTest(new Test
                {
                    TestName = test.TestName,
                    Description = test.Description,
                    CreatedBy = test.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    ExpireOn = test.ExpireOn,
                    TechnologyId = test.TechnologyId
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
                    response.Error = "Test is not added";
                }
            }
            catch(Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO UpdateTest(TestDTO test)
        {
            var response = new ResponseDTO();
            try
            {
                
                var updateFlag = _testRepository.UpdateTest(new Test
                {
                    Id = test.Id,
                    TestName = test.TestName,
                    Description = test.Description,
                    ExpireOn = test.ExpireOn,
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
    }
}
