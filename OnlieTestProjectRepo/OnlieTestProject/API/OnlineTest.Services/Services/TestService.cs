using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OnlineTest.Model;
using OnlineTest.Model.Interfaces;
using OnlineTest.Model.Repository;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.GetDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.Interface;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineTest.Services.Services
{
    public class TestService : ITestService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;
        private readonly ITechnologyRepository _technologyRepository;
        #endregion

        #region Constructor
        public TestService(ITestRepository testRepository, ITechnologyRepository technologyRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _technologyRepository = technologyRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public ResponseDTO GetTest()
        {
            var response = new ResponseDTO();
            try
            {
                var result = _mapper.Map<List<GetTestDTO>>(_testRepository.GetTest().ToList());

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
        public ResponseDTO GetTestUsingPagination(int PageNo, int RowsPerPage)
        {
            var response = new ResponseDTO();
            try
            {
                var result = _mapper.Map<List<GetTestDTO>>(_testRepository.GetTestUsingPagination(PageNo, RowsPerPage).ToList());

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

                var result = _mapper.Map<List<GetTestDTO>>(testById);

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
        public ResponseDTO AddTest(AddTestDTO test)
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

                var addFlag = _testRepository.AddTest(_mapper.Map<Test>(test));

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
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO UpdateTest(UpdateTestDTO test)
        {
            var response = new ResponseDTO();
            try
            {
                var updateFlag = _testRepository.UpdateTest(_mapper.Map<Test>(test));

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
        #endregion

    }
}
