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

namespace OnlineTest.Services.Services
{
    public class QuestionService : IQuestionService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;
        private readonly ITestRepository _testRepository;
        #endregion

        #region Constructor
        public QuestionService(IQuestionRepository questionRepository, ITestRepository testRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _testRepository = testRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public ResponseDTO GetQuestion()
        {
            var response = new ResponseDTO();
            try
            {
                var result = _mapper.Map<List<GetQuestionDTO>>(_questionRepository.GetQuestion().ToList());

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
        public ResponseDTO AddQuestion(AddQuestionDTO question)
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

                var addFlag = _questionRepository.AddQuestion(_mapper.Map<Question>(question));

                if (addFlag)
                {
                    response.Status = 204;
                    response.Message = "Created";
                }
                else
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Question is not added";
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
        public ResponseDTO UpdateQuestion(UpdateQuestionDTO question)
        {
            var response = new ResponseDTO();
            try
            {
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

        #endregion
    }
}