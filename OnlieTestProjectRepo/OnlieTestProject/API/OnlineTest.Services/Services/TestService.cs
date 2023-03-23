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
    public class TestService : ITestService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ITestRepository _testRepository;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        #endregion

        #region Constructor
        public TestService(ITestRepository testRepository, ITechnologyRepository technologyRepository, IQuestionRepository questionRepository,IAnswerRepository answerRepository ,IMapper mapper)
        {
            _testRepository = testRepository;
            _technologyRepository = technologyRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
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

                var result = _mapper.Map<GetTestDTO>(testById);

                var questionsList = _mapper.Map<List<GetQuestionDTO>>(_questionRepository.GetQuestionByTestId(testById.Id).ToList());
                foreach(var question in questionsList)
                {
                    var answerList = _mapper.Map<List<GetAnswerDTO>>(_answerRepository.GetAnswerByQuestionId(question.Id).ToList());
                    question.Answers = answerList;
                }
                result.Questions = questionsList;

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
        public ResponseDTO GetTestByTechnologyId(int technologyId)
        {
            var response = new ResponseDTO();
            try
            {
                var technologyById = _technologyRepository.GetTechnologyById(technologyId);
                if (technologyById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Technology not found";
                    return response;
                }

                var result = _mapper.Map<List<GetTestDTO>>(_testRepository.GetTestsByTechnologyId(technologyId).ToList());
                response.Status = 200;
                response.Message = "Ok";
                response.Data = result;
            }
            catch(Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO AddTest(int userId, AddTestDTO test)
        {
            var response = new ResponseDTO();
            try
            {
                var technologyById = _technologyRepository.GetTechnologyById(test.TechnologyId);
                if (technologyById == null)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Technology not found";
                    return response;
                }

                var testExists = _testRepository.TestExists(_mapper.Map<Test>(test));
                if (testExists != null)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Test already exists";
                    return response;
                }

                test.IsActive = true;
                test.CreatedBy = userId;
                test.CreatedOn = DateTime.UtcNow;

                var testId = _testRepository.AddTest(_mapper.Map<Test>(test));
                if(testId == 0)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Could not add test";
                    return response;
                }
                response.Status = 201;
                response.Message = "Created";
                response.Data = testId;
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
                var testById = _testRepository.GetTestById(test.Id);
                if (testById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Test not found";
                    return response;
                }

                var testExists = _testRepository.TestExists(_mapper.Map<Test>(test));
                if (testExists != null && test.Id != testExists.Id)
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Test already exists";
                    return response;
                }

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
        public ResponseDTO DeleteTest(int id)
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
                testById.IsActive = false;
                var deleteFlag = _testRepository.DeleteTest(_mapper.Map<Test>(testById));
                if (deleteFlag)
                {
                    response.Status = 204;
                    response.Message = "Deleted Successfully";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Deleted";
                    response.Error = "Test is not deleted";
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
