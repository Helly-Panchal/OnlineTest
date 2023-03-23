using AutoMapper;
using OnlineTest.Model;
using OnlineTest.Model.Interfaces;
using OnlineTest.Model.Repository;
using OnlineTest.Services.DTO;
using OnlineTest.Services.DTO.AddDTO;
using OnlineTest.Services.DTO.GetDTO;
using OnlineTest.Services.DTO.UpdateDTO;
using OnlineTest.Services.Interface;

namespace OnlineTest.Services.Services
{
    public class QuestionService : IQuestionService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly ITestRepository _testRepository;
        #endregion

        #region Constructor
        public QuestionService(IQuestionRepository questionRepository, ITestRepository testRepository, IAnswerRepository answerRepository ,IMapper mapper)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _testRepository = testRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public ResponseDTO GetQuestionByTestId(int testId)
        {
            var response = new ResponseDTO();
            try
            {
                var testById = _testRepository.GetTestById(testId);
                if (testById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Test not found";
                    return response;
                }
                var data = _mapper.Map<List<GetQuestionDTO>>(_questionRepository.GetQuestionByTestId(testId).ToList());
                response.Status = 200;
                response.Message = "Ok";
                response.Data = data;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO GetQuestionById(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var questionById = _questionRepository.GetQuestionById(id);
                if (questionById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Question not found";
                    return response;
                }

                var result = _mapper.Map<GetQuestionDTO>(questionById);
                result.Answers = _mapper.Map<List<GetAnswerDTO>>(_answerRepository.GetAnswerByQuestionId(questionById.Id).ToList());

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
        public ResponseDTO AddQuestion(int userId,AddQuestionDTO question)
        {
            var response = new ResponseDTO();
            try
            {
                //check for a question is there any test is there or not.
                var testById = _testRepository.GetTestById(question.TestId);
                if(testById == null)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Test not found";
                    return response;
                }

                var questionExists = _questionRepository.QuestionExists(_mapper.Map<Question>(question));
                if (questionExists != null)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Question already exists";
                    return response;
                }

                question.IsActive = true;
                question.CreatedBy = userId;
                question.CreatedOn = DateTime.UtcNow;

                var questionId = _questionRepository.AddQuestion(_mapper.Map<Question>(question));
                if (questionId == 0)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Question is not added";
                    return response;
                }
                response.Status = 201;
                response.Message = "Created";
                response.Data = questionId;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO UpdateQuestion(UpdateQuestionDTO question)
        {
            var response = new ResponseDTO();
            try
            {
                var questionById = _questionRepository.GetQuestionById(question.Id);
                if (questionById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Question not found";
                    return response;
                }

                var questionExists = _questionRepository.QuestionExists(_mapper.Map<Question>(question));
                if (questionExists != null && question.Id != questionExists.Id)
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Question already exists";
                    return response;
                }

                var updateFlag = _questionRepository.UpdateQuestion(_mapper.Map<Question>(question));
                if (updateFlag)
                {
                    response.Status = 204;
                    response.Message = "Updated";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Question not Updated";
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
        public ResponseDTO DeleteQuestion(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var questionById = _questionRepository.GetQuestionById(id);
                if(questionById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Question not found";
                    return response;
                }

                questionById.IsActive = false;
                
                var deleteFlag = _questionRepository.DeleteQuestion(_mapper.Map<Question>(questionById));
                if (deleteFlag)
                {
                    response.Status = 204;
                    response.Message = "Deleted Successfully";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not deleted";
                    response.Error = "Question is not deleted";
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
        #endregion
    }
}