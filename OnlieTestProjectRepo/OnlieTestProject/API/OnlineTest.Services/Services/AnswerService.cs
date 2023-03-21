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
    public class AnswerService : IAnswerService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ITestRepository _testRepository;
        private readonly IQuestionAnswerMapRepository _questionAnswerMapRepository;

        #endregion

        #region Constructor
        public AnswerService(IAnswerRepository answerRepository,IQuestionRepository questionRepository,ITestRepository test,IQuestionAnswerMapRepository questionAnswerMapRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _testRepository = test;
            _questionAnswerMapRepository = questionAnswerMapRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public ResponseDTO GetAnswer()
        {
            var response = new ResponseDTO();
            try
            {
                var result = _mapper.Map<List<GetAnswerDTO>>(_answerRepository.GetAnswer().ToList());

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
        public ResponseDTO GetAnswerById(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var answerById = _answerRepository.GetAnswerById(id);
                if (answerById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Answer not found";
                    return response;
                }

                var result = _mapper.Map<GetAnswerDTO>(answerById);

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
        public ResponseDTO AddAnswer(AddAnswerDTO answer)
        {
            var response = new ResponseDTO();
            try
            {
                var testById = _testRepository.GetTestById(answer.TestId);
                if (testById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Test not found";
                    return response;
                }

                var questionById = _questionRepository.GetQuestionById(answer.QuestionId);
                if (questionById == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Question not found";
                    return response;
                }

                var existsFlag = _answerRepository.IsAnswerExists(answer.TestId, answer.QuestionId, answer.Ans);
                if (existsFlag)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Answer already exists";
                    return response;
                }

                var answerId = _answerRepository.AddAnswer(_mapper.Map<Answer>(answer));
                if (answerId == 0)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Answer is not added";
                    return response;
                }
                var map = new QuestionAnswerMap
                {
                    TestId = answer.TestId,
                    QuestionId = answer.QuestionId,
                    AnswerId = answerId,
                    IsActive = true,
                    CreatedBy = answer.CreatedBy,
                    CreateOn = answer.CreateOn,
                };
                _questionAnswerMapRepository.AddMap(map);
                response.Status = 201;
                response.Message = "Created";
                response.Data = answerId;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO UpdateAnswer(UpdateAnswerDTO answer)
        {
            var response = new ResponseDTO();
            try
            {
                var answerId = _answerRepository.GetAnswerById(answer.Id);
                if(answerId == null)
                {
                    response.Status = 404;
                    response.Message = "Not Found";
                    response.Error = "Answer not found";
                    return response;
                }

                var existsFlag = _answerRepository.IsAnswerExists(answer.TestId, answer.QuestionId, answer.Ans);
                if (existsFlag)
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Answer already exists";
                    return response;
                }

                var updateFlag = _answerRepository.UpdateAnswer(_mapper.Map<Answer>(answer));
                if (updateFlag)
                {
                    response.Status = 204;
                    response.Message = "Updated";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Answer is not updated";
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
        public ResponseDTO DeleteAnswer(int id)
        {
            var response = new ResponseDTO();
            try
            {
                var answerById = _answerRepository.GetAnswerById(id);
                if(answerById == null)
                {
                    response.Status = 404;
                    response.Message = "Not found";
                    response.Error = "Answer not found";
                    return response;
                }
                answerById.IsActive = false;
                var deleteFlag = _answerRepository.DeleteAnswer(_mapper.Map<Answer>(answerById));
                if (deleteFlag)
                {
                    response.Status = 204;
                    response.Message = "Deleted";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Deleted";
                    response.Error = "Answer is not deleted";
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
