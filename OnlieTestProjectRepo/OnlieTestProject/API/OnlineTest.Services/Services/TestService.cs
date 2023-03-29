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
    public class TestService : ITestService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly ITestLinkRepository _testLinkRepository;
        private readonly IAnswerSheetRepository _answerSheetRepository;
        private readonly IMailService _mailService;
        private readonly IMailOutBoundRepository _mailOutBoundRepository;
        #endregion

        #region Constructor
        public TestService(IUserRepository userRepository, IUserRoleRepository userRoleRepository , ITestRepository testRepository, ITechnologyRepository technologyRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, ITestLinkRepository testLinkRepository, IAnswerSheetRepository answerSheetRepository, IMapper mapper, IMailService mailService, IMailOutBoundRepository mailOutBoundRepository)
        {
            _userRepository = userRepository;
            _technologyRepository = technologyRepository;
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
            _testLinkRepository = testLinkRepository;
            _answerSheetRepository = answerSheetRepository;
            _mapper = mapper;
            _mailService = mailService;
            _mailOutBoundRepository = mailOutBoundRepository;
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
                foreach (var question in questionsList)
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
            catch (Exception ex)
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
                if (testId == 0)
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

        //testEmailLink
        public ResponseDTO AddTestLink(int adminId, int testId, string userEmail)
        {
            var response = new ResponseDTO();
            try
            {
                //check if user is exists or not.
                var userByEmail = _userRepository.GetUserByEmail(userEmail);
                int userId;
                if (userByEmail == null)
                {
                    var user = new User
                    {
                        Name = userEmail,
                        Email = userEmail,
                        Password = "Password",
                        MobileNo = "0000000000",
                        IsActive = true
                    };
                    userId = _userRepository.AddUser(user);
                    _userRoleRepository.AddRole(new UserRole
                    {
                        UserId = userId,
                        RoleId = 2
                    });
                }
                else
                {
                    userId = userByEmail.Id;
                }

                //check if test is exists or not.
                var testById = _testRepository.GetTestById(testId);
                if (testById == null)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Test not found";
                    return response;                   
                }

                //check if link is already created and is not expired
                var existsFlag = _testLinkRepository.IsTestLinkExists(testId, userByEmail.Id);
                if (existsFlag)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Test link already exists";
                    return response;
                }
                

                var testLink = new TestEmailLink
                {
                    TestId = testId,
                    UserId = userByEmail.Id,
                    Token = Guid.NewGuid(),
                    AccessCount = 0,
                    ExpireOn = DateTime.UtcNow.AddDays(7),
                    IsActive = true,
                    CreatedBy = adminId,
                    CreatedOn = DateTime.UtcNow,
                };

                var addTest = _testLinkRepository.AddTestLink(testLink);
                if (addTest == 0)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Test link is not added";
                    return response;
                }

                // send email to candidate
                var mail = new MailDTO
                {
                    To = userEmail,
                    Subject = "Link to begin test",
                    Body = $"<p>Hi,</p><p>Here is your <a href=\"{testLink.Token}\" target=\"_blank\">link</a> to begin the test.</p><p>All the best!</p>"
                };
                _mailService.SendMail(mail);

                // store record in email outbound table
                var mailOutbound = new MailOutBound
                {
                    To = mail.To,
                    Body = mail.Body,
                    TestLinkId = testId,
                    CreatedBy = adminId,
                    CreatedOn = DateTime.UtcNow
                };
                _mailOutBoundRepository.AddMailOutBound(mailOutbound);

                response.Status = 201;
                response.Message = "Created";
                response.Data = addTest;
            }
            catch (Exception ex)
            {
                response.Status = 500;
                response.Message = "Internal Server Error";
                response.Error = ex.Message;
            }
            return response;
        }
        public ResponseDTO GetTestLink(string token, string userEmail)
        {
            var response = new ResponseDTO();
            try
            {
                var testLink = _testLinkRepository.GetTestLink(Guid.Parse(token));
                if (testLink == null)
                {
                    response.Status = 400;
                    response.Message = "Not Created";
                    response.Error = "Test link is not exists or it is expired";
                    return response;
                }

                testLink.AccessCount += 1;   
                
                var userById = _userRepository.GetUserById(testLink.UserId);
                if (userEmail.ToLower() != userById.Email.ToLower())
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Email is incorrect";
                    return response;
                }
                response = GetTestById(testLink.TestId);
                if(response.Status == 200)
                {
                    testLink.AccessOn = DateTime.UtcNow;
                    _testLinkRepository.UpdateTestLink(testLink);
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

        
        //user submitted data
        public ResponseDTO SubmittedTestData(AddAnswerSheetDTO answerSheet)
        {
            var response = new ResponseDTO();
            try
            {            
                //check if test is already submitted or not.
                var testLink = _testLinkRepository.GetTestLink(answerSheet.Token);
                if(testLink.SubmittedOn != null)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Test is already submitted";
                    return response;
                }

                //Check if test has been expired or not.
                if(testLink.ExpireOn <= DateTime.UtcNow)
                {
                    response.Status = 400;
                    response.Message = "Bad Request";
                    response.Error = "Test link has expired";
                    return response;
                }

                //add this answersheet to database
                answerSheet.CreatedTime = DateTime.UtcNow;
                List<AnswerSheet> answerSheets = new List<AnswerSheet>();
                foreach(var question in  answerSheet.Questions)
                {
                    var sheet = new AnswerSheet
                    {
                        Token = answerSheet.Token,
                        QuestionId = question.QuestionId,
                        AnswerId = question.AnswerId,
                        CreatedTime = answerSheet.CreatedTime
                    };
                    answerSheets.Add(sheet);                                //added to list
                }
                _answerSheetRepository.AddAnswerSheet(answerSheets);        //passed list to the db.

                //update the value of submit on
                testLink.SubmittedOn = DateTime.UtcNow;
                _testLinkRepository.UpdateTestLink(testLink);

                response.Status = 200;
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
        #endregion
    }
}
