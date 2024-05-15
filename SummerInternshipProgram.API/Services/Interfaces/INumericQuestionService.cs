using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;

namespace SummerInternshipProgram.API.Services.Interfaces
{
    public interface INumericQuestionService
    {
        Task<BaseResponseDto<string>> Add(BaseRequestDto<NumericQuestion> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<NumericQuestion> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Delete(BaseRequestDto<IdPayload> input, string applicantId);
        Task<BaseResponseDto<NumericQuestion>> GetSingle(BaseRequestDto<IdPayload> input, string applicantId);
        Task<BaseResponseDto<List<NumericQuestion>>> GetAll(BaseRequestDto<string> input, string applicantId);
    }
}
