using SummerInternshipProgram.API.Constants;
using SummerInternshipProgram.API.Data;
using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Dtos.DatabaseServiceDto;
using SummerInternshipProgram.API.Helpers.Interface;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;
using SummerInternshipProgram.API.Services.Interfaces;

namespace SummerInternshipProgram.API.Services.Implementation
{
    public class InternshipProgramService : IInternshipProgramService
    {
        private readonly ILogHelper _logger;
        private string classname = nameof(InternshipProgramService);
        private EmploymentDbContext _db;
        public InternshipProgramService(ILogHelper logger, EmploymentDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<BaseResponseDto<string>> Add(BaseRequestDto<InternshipProgram> input)
        {
            var methodName = $" {classname}/{nameof(Add)}";
            var output = new BaseResponseDto<string>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {
                var applicant = input.Request;
                _db.Programs.Add(applicant);
                output.ResponseCode = GeneralResponse.sucessCode;
                output.ResponseMessage = GeneralResponse.sucessMessage;
                output.Response = "Program saved succesfully";
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = "Program not saved succesfully";
            }
            _logger.LogInformation(input.RequestId, $"Response:{output}", input.Ip, methodName);
            return output;
        }

        public async Task<BaseResponseDto<BoolPayload>> Delete(BaseRequestDto<IdPayload> input)
        {
            var methodName = $" {classname}/{nameof(Delete)}";
            var output = new BaseResponseDto<BoolPayload>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {

                var applicant = _db.Programs.Where(m => m.Id == input.Request.Id).FirstOrDefault();
                if (applicant is null)
                {
                    output.ResponseCode = GeneralResponse.failureCode;
                    output.ResponseMessage = "Program does not exist";
                    output.Response = new BoolPayload()
                    {
                        IsTrue = false
                    };
                    return output;
                }
                else
                {
                    _db.Programs.Remove(applicant);
                    output.Response = new BoolPayload()
                    {
                        IsTrue = true
                    };
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                    _db.SaveChanges();
                }

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

        public async Task<BaseResponseDto<List<InternshipProgram>>> GetAll(BaseRequestDto<string> input)
        {
            var methodName = $" {classname}/{nameof(GetAll)}";
            var output = new BaseResponseDto<List<InternshipProgram>>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {

                var program = _db.Programs.ToList();

                if (program.Any())
                {
                    output.Response = program.ToList();
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                }
                else
                {
                    output.Response = new List<InternshipProgram>();
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = "There are no available program";
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = new List<InternshipProgram>();
            }
            _logger.LogInformation(input.RequestId, $"Response:{output}", input.Ip, methodName);
            return output;
        }

        public async Task<BaseResponseDto<InternshipProgram>> GetSingle(BaseRequestDto<IdPayload> input)
        {
            var methodName = $" {classname}/{nameof(GetSingle)}";
            var output = new BaseResponseDto<InternshipProgram>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {

                var program = _db.Programs.Where(m => m.Id == input.Request.Id).FirstOrDefault();
                if (program is null)
                {
                    output.ResponseCode = GeneralResponse.failureCode;
                    output.ResponseMessage = "Program does not exist";
                    output.Response = new InternshipProgram();
                    return output;
                }
                else
                {
                    output.Response = program;
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = new InternshipProgram();
            }
            _logger.LogInformation(input.RequestId, $"Response:{output}", input.Ip, methodName);
            return output;
        }

        public async Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<InternshipProgram> input)
        {
            var methodName = $" {classname}/{nameof(Update)}";
            var output = new BaseResponseDto<BoolPayload>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {
                var dbData = _db.Programs.Where(m => m.Id == input.Request.Id).FirstOrDefault();
                if (dbData != null)
                {
                    output.ResponseCode = GeneralResponse.failureCode;
                    output.ResponseMessage = "Program does not exist";
                    output.Response.IsTrue = false;
                    return output;
                }
                dbData = input.Request;



                _db.Programs.Update(dbData);
                output.ResponseCode = GeneralResponse.sucessCode;
                output.ResponseMessage = GeneralResponse.sucessMessage;
                output.Response.IsTrue = true;
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
