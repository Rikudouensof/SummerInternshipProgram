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
    public class GenderService : IGenderService
    {
        private readonly ILogHelper _logger;
        private string classname = nameof(DateQuestionService);
        private EmploymentDbContext _db;
        public GenderService(ILogHelper logger, EmploymentDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<BaseResponseDto<string>> Add(BaseRequestDto<Gender> input, string applicantId)
        {
            var methodName = $" {classname}/{nameof(Add)}";
            var output = new BaseResponseDto<string>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {
                var gender = input.Request;
                if (string.IsNullOrEmpty(applicantId))
                {
                    _db.Genders.Add(gender);
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                    output.Response = "Gender saved succesfully";
                }
                else
                {
                    if (string.IsNullOrEmpty(input.Request.Name.ToString()))
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = GeneralResponse.failureMessage;
                        output.Response = "No input Gender found";
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
                    if (applicant.Gender is not null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = GeneralResponse.failureMessage;
                        output.Response = "Applicant Gender already set";
                        return output;
                    }
                    var serializedAnswer = JsonConvert.SerializeObject(gender);
                    applicant.Gender = JsonConvert.DeserializeObject<ApplicantGender>(serializedAnswer);
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                    output.Response = $"Gender saved successfuly into {applicant.LastName}'s data";
                    _db.Applicants.Update(applicant);
                }
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = "Gender not saved succesfully";
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
                var question = _db.Genders.Where(m => m.Id == input.Request.Id).FirstOrDefault();

                if (string.IsNullOrEmpty(applicantId))
                {
                    if (question is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Gender does not exist";
                        output.Response = new BoolPayload()
                        {
                            IsTrue = false
                        };
                        return output;
                    }
                    else
                    {
                        _db.Genders.Remove(question);
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
                    var toDeleteAnswer = applicant.Gender;
                   
                    output.ResponseCode = GeneralResponse.failureCode;
                    output.ResponseMessage = $"Cannot Delete Applicant's Gender";
                    output.Response = new BoolPayload()
                    {
                        IsTrue = false
                    };
                 

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

        public async Task<BaseResponseDto<List<Gender>>> GetAll(BaseRequestDto<string> input, string applicantId)
        {
            var methodName = $" {classname}/{nameof(GetAll)}";
            var output = new BaseResponseDto<List<Gender>>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {

                if (applicantId is null)
                {
                    var gender = _db.Genders.ToList();
                    if (gender.Any())
                    {
                        output.Response = gender.ToList();
                        output.ResponseCode = GeneralResponse.sucessCode;
                        output.ResponseMessage = GeneralResponse.sucessMessage;
                    }
                    else
                    {
                        output.Response = new List<Gender>();
                        output.ResponseCode = GeneralResponse.sucessCode;
                        output.ResponseMessage = "There are no available gender";
                    }
                }
                else
                {
                    var applicant = _db.Applicants.Where(m => m.Id == applicantId).FirstOrDefault();
                    if (applicant is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Applicant does not exist";
                        output.Response = new List<Gender>();

                        _logger.logWarning(input.RequestId, $"Appicant {applicantId} does not exist", input.Ip, methodName);
                        return output;
                    }
                    var gender = applicant.Gender;
                    if (gender is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Applicant does not have any answer";
                        output.Response = new List<Gender>();
                        _logger.logWarning(input.RequestId, $"Appicant, {applicantId}, does not have any gender", input.Ip, methodName);
                        return output;
                    }

                    output.Response = new List<Gender>();
                    output.Response.Add(gender);
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = new List<Gender>();
            }
            _logger.LogInformation(input.RequestId, $"Response:{output}", input.Ip, methodName);
            return output;
        }

        public async Task<BaseResponseDto<Gender>> GetSingle(BaseRequestDto<IdPayload> input, string applicantId)
        {
            var methodName = $" {classname}/{nameof(GetSingle)}";
            var output = new BaseResponseDto<Gender>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {
                if (string.IsNullOrEmpty(applicantId))
                {
                    var gender = _db.Genders.Where(m => m.Id == input.Request.Id).FirstOrDefault();
                    if (gender is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Question does not exist";
                        output.Response = new Gender();
                        return output;
                    }
                    else
                    {
                        output.Response = gender;
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
                        output.Response = new Gender();

                        _logger.logWarning(input.RequestId, $"Appicant {applicantId} does not exist", input.Ip, methodName);
                        return output;
                    }
                    var usergender = applicant.Gender;
                    if (usergender is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Applicant does not have any answer";
                        output.Response = new Gender();
                        _logger.logWarning(input.RequestId, $"Appicant, {applicantId}, does not have this gender", input.Ip, methodName);
                        return output;
                    }

                    output.Response = usergender;
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = new Gender();
            }
            _logger.LogInformation(input.RequestId, $"Response:{output}", input.Ip, methodName);
            return output;
        }

        public async Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<Gender> input, string applicantId)
        {
            var methodName = $" {classname}/{nameof(Update)}";
            var output = new BaseResponseDto<BoolPayload>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {
                var gender = _db.Genders.Where(m => m.Id == input.Request.Id).FirstOrDefault();
                if (string.IsNullOrEmpty(applicantId))
                {

                    if (gender != null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Question does not exist";
                        output.Response.IsTrue = false;
                        return output;
                    }
                    _db.Genders.Update(gender);
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
                    var userGender = applicant.Gender;
                    if (userGender is null)
                    {
                        output.ResponseCode = GeneralResponse.failureCode;
                        output.ResponseMessage = "Applicant does not have any gender";
                        output.Response = new BoolPayload()
                        {
                            IsTrue = false
                        };
                        _logger.logWarning(input.RequestId, $"Appicant, {applicantId}, does not have this gender", input.Ip, methodName);
                        return output;
                    }
                   
                    applicant.Gender = userGender;
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
