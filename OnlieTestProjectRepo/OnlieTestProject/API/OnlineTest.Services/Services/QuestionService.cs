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

namespace OnlineTest.Services.Services
{
    public class QuestionService : IQuestionService
    {
        #region Fields
        private readonly IQuestionRepository _questionRepository;
        private readonly ITestRepository _testRepository;
        #endregion

        #region Constructor
        public QuestionService(IQuestionRepository questionRepository, ITestRepository testRepository)
        {
            _questionRepository = questionRepository;
            _testRepository = testRepository;
        }
        #endregion

        #region Methods

        public ResponseDTO GetQuestion()
        {
            var response = new ResponseDTO();
            try
            {
                var questions = _questionRepository.GetQuestion().Select(q => new QuestionDTO()
                {
                    Id = q.Id,
                    QuestionName = q.QuestionName,
                    Que = q.Que,
                    Type = q.Type,
                    Weightage = q.Weightage,
                    SortOrder = q.SortOrder,
                    TestId = q.TestId
                }).ToList();
                response.Status = 200;
                response.Data = questions;
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
                    response.Error = "User not found";
                    return response;
                }

                var result = new QuestionDTO()
                {
                    Id = questionById.Id,
                    QuestionName = questionById.QuestionName,
                    Que = questionById.Que,
                    Type = questionById.Type,
                    Weightage = questionById.Weightage,
                    SortOrder = questionById.SortOrder,
                    TestId = questionById.TestId
                };
                response.Status = 200;
                response.Data = questionById;
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

        public ResponseDTO AddQuestion(QuestionDTO question)
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
                var addFlag = _questionRepository.AddQuestion(new Question
                {
                    QuestionName = question.QuestionName,
                    Que = question.Que,
                    Type = question.Type,
                    Weightage = question.Weightage,
                    SortOrder = question.SortOrder,
                    IsActive = true,
                    CreatedBy = question.CreatedBy,
                    CreateOn = DateTime.UtcNow,
                    TestId = question.TestId
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


        public ResponseDTO UpdateQuestion(QuestionDTO question)
        {
            var response = new ResponseDTO();
            try
            {
                var updateFlag = _questionRepository.UpdateQuestion(new Question
                {
                    Id = question.Id,
                    QuestionName = question.QuestionName,
                    Que = question.Que,
                    Weightage = question.Weightage,
                    SortOrder = question.SortOrder,
                    IsActive = question.IsActive
                });

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