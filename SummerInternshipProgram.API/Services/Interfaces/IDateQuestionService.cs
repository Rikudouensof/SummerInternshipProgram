using SummerInternshipProgram.API.Dtos.DatabaseServiceDto;
using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;

namespace SummerInternshipProgram.API.Services.Interfaces
{
    public interface IDateQuestionService
    {

        Task<BaseResponseDto<string>> Add(BaseRequestDto<DateQuestion> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<DateQuestion> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Delete(BaseRequestDto<IdPayload> input, string applicantId);
        Task<BaseResponseDto<DateQuestion>> GetSingle(BaseRequestDto<IdPayload> input, string applicantId);
        Task<BaseResponseDto<List<DateQuestion>>> GetAll(BaseRequestDto<string> input, string applicantId);
    }
}
