using Newtonsoft.Json;
using SummerInternshipProgram.API.Constants;
using SummerInternshipProgram.API.Data;
using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Helpers.Interface;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;
using SummerInternshipProgram.API.Services.Interfaces;

namespace SummerInternshipProgram.API.Services.Implementation
{
    public class DropdownQuestionService : IDropdownQuestionService
    {
        private readonly ILogHelper _logger;
        private string classname = nameof(DropdownQuestionService);
        private EmploymentDbContext _db;
        public DropdownQuestionService(ILogHelper logger, EmploymentDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<BaseResponseDto<string>> Add(BaseRequestDto<DropdownQuestion> input, string applicantId)
        {
            var methodName = $" {classname}/{nameof(Add)}";
            var output = new BaseResponseDto<string>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {
                var question = input.Request;
                if (string.IsNullOrEmpty(applicantId))
                {
                    _db.DropdownQuestions.Add(question);
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                    output.Response = "Question saved succesfully";
                }
                else
                {
                    if (string.IsNullOrEmpty(input.Request.Answer.ToString()))
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = GeneralResponse.failureMessage;
                        output.Response = "No input Answer found";
                        return output;
                    }
                    var applicant = _db.Applicants.Where(m => m.Id == applicantId).FirstOrDefault();
                    if (applicant is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = GeneralResponse.failureMessage;
                        output.Response = "Applicant does not exist";
                        return output;
                    }
                    var answer = JsonConvert.SerializeObject(question);
                    applicant.DropdownAnswers.Add(JsonConvert.DeserializeObject<ApplicantDropdownQuestion>(answer));
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                    output.Response = $"Question saved successfuly into {applicant.LastName}'s data";
                    _db.Applicants.Update(applicant);
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = "Question not saved succesfully";
            }
            _logger.LogInformation(input.RequestId, $"Response:{output}", input.Ip, methodName);
            return output;
        }

        public async Task<BaseResponseDto<BoolPayload>> Delete(BaseRequestDto<IdPayload> input, string applicantId)
        {
            var methodName = $" {classname}/{nameof(Delete)}";
            var output = new BaseResponseDto<BoolPayload>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {
                var question = _db.DropdownQuestions.Where(m => m.Id == input.Request.Id).FirstOrDefault();

                if (string.IsNullOrEmpty(applicantId))
                {
                    if (question is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Question does not exist";
                        output.Response = new BoolPayload()
                        {
                            IsTrue = false
                        };
                        return output;
                    }
                    else
                    {
                        _db.DropdownQuestions.Remove(question);
                        output.Response = new BoolPayload()
                        {
                            IsTrue = true
                        };
                        output.ResponseCode = GeneralResponse.sucessCode;
                        output.ResponseMessage = GeneralResponse.sucessMessage;
                        _db.SaveChanges();
                    }

                }
                else
                {

                    var applicant = _db.Applicants.Where(m => m.Id == applicantId).FirstOrDefault();
                    if (applicant is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Applicant does not exist";
                        output.Response = new BoolPayload()
                        {
                            IsTrue = false
                        };
                        _logger.logWarning(input.RequestId, $"Appicant {applicantId} does not exist", input.Ip, methodName);
                        return output;
                    }
                    var toDeleteAnswer = applicant.DropdownAnswers.Where(m => m.Id == input.Request.Id).FirstOrDefault();
                    if (toDeleteAnswer is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Applicant does not have this answer";
                        output.Response = new BoolPayload()
                        {
                            IsTrue = false
                        };
                        _logger.logWarning(input.RequestId, $"Appicant, {applicantId};s answer does not exist", input.Ip, methodName);
                        return output;
                    }
                    applicant.DropdownAnswers.Remove(toDeleteAnswer);
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = $"Question successfuly deleted from {applicant.LastName}'s data";
                    output.Response = new BoolPayload()
                    {
                        IsTrue = true
                    };
                    _db.Applicants.Update(applicant);

                }
                _db.SaveChanges();


            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = new BoolPayload()
                {
                    IsTrue = false
                };
            }
            _logger.LogInformation(input.RequestId, $"Response:{output}", input.Ip, methodName);
            return output;
        }

        public async Task<BaseResponseDto<List<DropdownQuestion>>> GetAll(BaseRequestDto<string> input, string applicantId)
        {
            var methodName = $" {classname}/{nameof(GetAll)}";
            var output = new BaseResponseDto<List<DropdownQuestion>>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {

                if (applicantId is null)
                {
                    var question = _db.DropdownQuestions.ToList();
                    if (question.Any())
                    {
                        output.Response = question.ToList();
                        output.ResponseCode = GeneralResponse.sucessCode;
                        output.ResponseMessage = GeneralResponse.sucessMessage;
                    }
                    else
                    {
                        output.Response = new List<DropdownQuestion>();
                        output.ResponseCode = GeneralResponse.sucessCode;
                        output.ResponseMessage = "There are no available question";
                    }
                }
                else
                {
                    var applicant = _db.Applicants.Where(m => m.Id == applicantId).FirstOrDefault();
                    if (applicant is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Applicant does not exist";
                        output.Response = new List<DropdownQuestion>();

                        _logger.logWarning(input.RequestId, $"Appicant {applicantId} does not exist", input.Ip, methodName);
                        return output;
                    }
                    var alltypeQuestion = applicant.DropdownAnswers;
                    if (alltypeQuestion.Any())
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Applicant does not have any answer";
                        output.Response = new List<DropdownQuestion>();
                        _logger.logWarning(input.RequestId, $"Appicant, {applicantId}, does not have this type of answer", input.Ip, methodName);
                        return output;
                    }
                    var answers = JsonConvert.SerializeObject(alltypeQuestion.ToList());
                    output.Response = JsonConvert.DeserializeObject<List<DropdownQuestion>>(answers);
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = new List<DropdownQuestion>();
            }
            _logger.LogInformation(input.RequestId, $"Response:{output}", input.Ip, methodName);
            return output;
        }

        public async Task<BaseResponseDto<DropdownQuestion>> GetSingle(BaseRequestDto<IdPayload> input, string applicantId)
        {
            var methodName = $" {classname}/{nameof(GetSingle)}";
            var output = new BaseResponseDto<DropdownQuestion>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {
                if (string.IsNullOrEmpty(applicantId))
                {
                    var question = _db.DropdownQuestions.Where(m => m.Id == input.Request.Id).FirstOrDefault();
                    if (question is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Question does not exist";
                        output.Response = new DropdownQuestion();
                        return output;
                    }
                    else
                    {
                        output.Response = question;
                        output.ResponseCode = GeneralResponse.sucessCode;
                        output.ResponseMessage = GeneralResponse.sucessMessage;
                    }
                }
                else
                {
                    var applicant = _db.Applicants.Where(m => m.Id == applicantId).FirstOrDefault();
                    if (applicant is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Applicant does not exist";
                        output.Response = new DropdownQuestion();

                        _logger.logWarning(input.RequestId, $"Appicant {applicantId} does not exist", input.Ip, methodName);
                        return output;
                    }
                    var alltypeQuestion = applicant.DropdownAnswers.Where(m => m.Id == input.Request.Id).FirstOrDefault();
                    if (alltypeQuestion is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Applicant does not have any answer";
                        output.Response = new DropdownQuestion();
                        _logger.logWarning(input.RequestId, $"Appicant, {applicantId}, does not have this answer", input.Ip, methodName);
                        return output;
                    }

                    output.Response = alltypeQuestion;
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = new DropdownQuestion();
            }
            _logger.LogInformation(input.RequestId, $"Response:{output}", input.Ip, methodName);
            return output;
        }

        public async Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<DropdownQuestion> input, string applicantId)
        {
            var methodName = $" {classname}/{nameof(Update)}";
            var output = new BaseResponseDto<BoolPayload>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {
                var question = _db.DropdownQuestions.Where(m => m.Id == input.Request.Id).FirstOrDefault();
                if (string.IsNullOrEmpty(applicantId))
                {

                    if (question != null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Question does not exist";
                        output.Response.IsTrue = false;
                        return output;
                    }
                    _db.DropdownQuestions.Update(question);
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                    output.Response.IsTrue = true;
                }
                else
                {
                    var applicant = _db.Applicants.Where(m => m.Id == applicantId).FirstOrDefault();
                    if (applicant is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Applicant does not exist";
                        output.Response = new BoolPayload()
                        {
                            IsTrue = false
                        };
                        _logger.logWarning(input.RequestId, $"Appicant {applicantId} does not exist", input.Ip, methodName);
                        return output;
                    }
                    var alltypeQuestion = applicant.DropdownAnswers.Where(m => m.Id == input.Request.Id).FirstOrDefault();
                    if (alltypeQuestion is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Applicant does not have any answer";
                        output.Response = new BoolPayload()
                        {
                            IsTrue = false
                        };
                        _logger.logWarning(input.RequestId, $"Appicant, {applicantId}, does not have this answer", input.Ip, methodName);
                        return output;
                    }
                    applicant.DropdownAnswers.Remove(alltypeQuestion);
                    var answer = JsonConvert.SerializeObject(question);
                    applicant.DropdownAnswers.Add(JsonConvert.DeserializeObject<ApplicantDropdownQuestion>(answer));
                    output.Response = new BoolPayload()
                    {
                        IsTrue = true
                    };
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                    _db.Applicants.Update(applicant);
                }

                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response.IsTrue = false;
            }
            _logger.LogInformation(input.RequestId, $"Response:{output}", input.Ip, methodName);
            return output;
        }
    }
}
