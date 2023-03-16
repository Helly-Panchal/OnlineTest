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
    public class AnswerService : IAnswerService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IAnswerRepository _answerRepository;
        private readonly ITestRepository _testRepository;
        #endregion

        #region Constructor
        public AnswerService(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
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
                var addFlag = _answerRepository.AddAnswer(_mapper.Map<Answer>(answer));

                if (addFlag)
                {
                    response.Status = 204;
                    response.Message = "Created";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Answer is not added";
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
        public ResponseDTO UpdateAnswer(UpdateAnswerDTO answer)
        {
            var response = new ResponseDTO();
            try
            {
                var updateFlag = _answerRepository.AddAnswer(_mapper.Map<Answer>(answer));

                if (updateFlag)
                {
                    response.Status = 204;
                    response.Message = "Updated";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Updated";
                    response.Error = "Answer not Updated";
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
