using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;

namespace SummerInternshipProgram.API.Services.Interfaces
{
    public interface IYesOrNoQuestionService
    {
        Task<BaseResponseDto<string>> Add(BaseRequestDto<YesOrNoQuestion> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<YesOrNoQuestion> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Delete(BaseRequestDto<IdPayload> input, string applicantId);
        Task<BaseResponseDto<YesOrNoQuestion>> GetSingle(BaseRequestDto<IdPayload> input, string applicantId);
        Task<BaseResponseDto<List<YesOrNoQuestion>>> GetAll(BaseRequestDto<string> input, string applicantId);


    }
}
