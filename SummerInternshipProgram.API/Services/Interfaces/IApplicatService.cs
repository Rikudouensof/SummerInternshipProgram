using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Dtos.DatabaseServiceDto;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;

namespace SummerInternshipProgram.API.Services.Interfaces
{
    public interface IApplicatService
    {
        Task<BaseResponseDto<string>> Add(BaseRequestDto<ApplicantRequestDto> input);
        Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<ApplicantRequestDto> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Delete(BaseRequestDto<IdPayload> input);
        Task<BaseResponseDto<Applicant>> GetSingle(BaseRequestDto<IdPayload> input);
        Task<BaseResponseDto<List<ApplicantRequestDto>>> GetAll(BaseRequestDto<string> input);
    }
}
