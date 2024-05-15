using SummerInternshipProgram.API.Constants;
using SummerInternshipProgram.API.Data;
using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Dtos.DatabaseServiceDto;
using SummerInternshipProgram.API.Helpers.Implementation;
using SummerInternshipProgram.API.Helpers.Interface;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;
using SummerInternshipProgram.API.Services.Interfaces;
using System.Collections.Generic;

namespace SummerInternshipProgram.API.Services.Implementation
{
    public class ApplicatService : IApplicatService
    {
        private readonly ILogHelper _logger;
        private string classname = nameof(ApplicatService);
        private EmploymentDbContext _db;
        public ApplicatService(ILogHelper logger, EmploymentDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<BaseResponseDto<string>> Add(BaseRequestDto<ApplicantRequestDto> input)
        {
            var methodName = $" {classname}/{nameof(Add)}";
            var output = new BaseResponseDto<string>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {
                var applicant = ApplicantRequestDtoToApplicant(input.Request);
                _db.Applicants.Add(applicant);
                output.ResponseCode = GeneralResponse.sucessCode;
                output.ResponseMessage = GeneralResponse.sucessMessage;
                output.Response = "Applicant saved succesfully";
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = "Applicant not saved succesfully";
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

                var applicant = _db.Applicants.Where(m => m.Id == input.Request.Id).FirstOrDefault();
                if (applicant is null)
                {
                    output.ResponseCode = GeneralResponse.failureCode;
                    output.ResponseMessage = "Applicant does not exist";
                    output.Response = new BoolPayload()
                    {
                        IsTrue = false
                    };
                    return output;
                }
                else
                {
                    _db.Applicants.Remove(applicant);
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

        public async Task<BaseResponseDto<List<ApplicantRequestDto>>> GetAll(BaseRequestDto<string> input)
        {
            var methodName = $" {classname}/{nameof(GetAll)}";
            var output = new BaseResponseDto<List<ApplicantRequestDto>>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {

                var applicants = _db.Applicants.ToList();

                if (applicants.Any())
                {
                    output.Response = ListApplicantToApplicantRequestDto(applicants.ToList());
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                }
                else
                {
                    output.Response = new List<ApplicantRequestDto>();
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = "There are no available applicant";
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = new List<ApplicantRequestDto>();
            }
            _logger.LogInformation(input.RequestId, $"Response:{output}", input.Ip, methodName);
            return output;
        }

        public async Task<BaseResponseDto<Applicant>> GetSingle(BaseRequestDto<IdPayload> input)
        {
            var methodName = $" {classname}/{nameof(GetSingle)}";
            var output = new BaseResponseDto<Applicant>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {

                var applicant = _db.Applicants.Where(m => m.Id == input.Request.Id).FirstOrDefault();
                if (applicant is null)
                {
                    output.ResponseCode = GeneralResponse.failureCode;
                    output.ResponseMessage = "Applicant does not exist";
                    output.Response = new Applicant();
                    return output;
                }
                else
                {
                    output.Response = applicant;
                    output.ResponseCode = GeneralResponse.sucessCode;
                    output.ResponseMessage = GeneralResponse.sucessMessage;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(input.RequestId, $"Failed", input.Ip, methodName, ex);
                output.ResponseCode = GeneralResponse.failureCode;
                output.ResponseMessage = GeneralResponse.failureMessage;
                output.Response = new Applicant();
            }
            _logger.LogInformation(input.RequestId, $"Response:{output}", input.Ip, methodName);
            return output;
        }

        public async Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<ApplicantRequestDto> input, string applicantId)
        {
            var methodName = $" {classname}/{nameof(Update)}";
            var output = new BaseResponseDto<BoolPayload>();
            _logger.LogInformation(input.RequestId, $"New", input.Ip, methodName);
            try
            {
                var applicant = _db.Applicants.Where(m => m.Id == applicantId).FirstOrDefault();
                if (applicant != null)
                {
                    output.ResponseCode = GeneralResponse.failureCode;
                    output.ResponseMessage = "Applicant does not exist";
                    output.Response.IsTrue = false;
                    return output;
                }

                applicant = ApplicantRequestDtoToApplicant(input.Request);

                _db.Applicants.Update(applicant);
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



        private Applicant ApplicantRequestDtoToApplicant(ApplicantRequestDto input)
        {
            return new Applicant()
            {
                Id = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                Phone = input.Phone,
                IsPhoneInternal = input.IsPhoneInternal,
                IsCurrentResidenceHidden = input.IsCurrentResidenceHidden,
                IsCurrentResidenceInternal = input.IsCurrentResidenceInternal,
                IsDateOfBirthHidden = input.IsDateOfBirthHidden,
                IsDateOfBirthInternal = input.IsDateOfBirthInternal,
                IsIDNumberHidden = input.IsIDNumberHidden,
                IsIDNumberInternal = input.IsIDNumberInternal,
                IsNationalityHidden = input.IsNationalityHidden,
                IsNationalityInternal = input.IsNationalityInternal,
                IsPhoneHidden = input.IsPhoneHidden,
                Nationality = input.Nationality,
                CurrentResidence = input.CurrentResidence,
                IDNumber = input.IDNumber,
                DateOfBirth = GeneralHelper.StringToDate(input.DateOfBirth),
                
            };
        }

        private ApplicantRequestDto ApplicantToApplicantRequestDto(Applicant input)
        {
            return new ApplicantRequestDto()
            {
                Id = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                Phone = input.Phone,
                IsPhoneInternal = input.IsPhoneInternal,
                IsCurrentResidenceHidden = input.IsCurrentResidenceHidden,
                IsCurrentResidenceInternal = input.IsCurrentResidenceInternal,
                IsDateOfBirthHidden = input.IsDateOfBirthHidden,
                IsDateOfBirthInternal = input.IsDateOfBirthInternal,
                IsIDNumberHidden = input.IsIDNumberHidden,
                IsIDNumberInternal = input.IsIDNumberInternal,
                IsNationalityHidden = input.IsNationalityHidden,
                IsNationalityInternal = input.IsNationalityInternal,
                IsPhoneHidden = input.IsPhoneHidden,
                Nationality = input.Nationality,
                CurrentResidence = input.CurrentResidence,
                IDNumber = input.IDNumber,
                DateOfBirth = GeneralHelper.DateTimeToString(input.DateOfBirth),
                
            };
        }


        private List<ApplicantRequestDto> ListApplicantToApplicantRequestDto(List<Applicant> input)
        {
            var output = new List<ApplicantRequestDto>();
            foreach (var item in input)
            {
                output.Add(ApplicantToApplicantRequestDto(item));
            }

            return output;
        }
    }
}
