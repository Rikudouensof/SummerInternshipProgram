using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;

namespace SummerInternshipProgram.API.Services.Interfaces
{
    public interface IParagraphQuestionService
    {

        Task<BaseResponseDto<string>> Add(BaseRequestDto<ParagraphQuestion> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<ParagraphQuestion> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Delete(BaseRequestDto<IdPayload> input, string applicantId);
        Task<BaseResponseDto<ParagraphQuestion>> GetSingle(BaseRequestDto<IdPayload> input, string applicantId);
        Task<BaseResponseDto<List<ParagraphQuestion>>> GetAll(BaseRequestDto<string> input, string applicantId);
    }
}
