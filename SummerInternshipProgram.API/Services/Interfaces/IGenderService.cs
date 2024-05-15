using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;

namespace SummerInternshipProgram.API.Services.Interfaces
{
    public interface IGenderService
    {
        Task<BaseResponseDto<string>> Add(BaseRequestDto<Gender> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<Gender> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Delete(BaseRequestDto<IdPayload> input, string applicantId);
        Task<BaseResponseDto<Gender>> GetSingle(BaseRequestDto<IdPayload> input, string applicantId);
        Task<BaseResponseDto<List<Gender>>> GetAll(BaseRequestDto<string> input, string applicantId);
    }
}
