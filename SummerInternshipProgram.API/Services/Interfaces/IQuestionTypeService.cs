using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;

namespace SummerInternshipProgram.API.Services.Interfaces
{
    public interface IQuestionTypeService  
    {

        Task<BaseResponseDto<string>> Add(BaseRequestDto<QuestionType> input);
        Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<QuestionType> input);
        Task<BaseResponseDto<BoolPayload>> Delete(BaseRequestDto<IdPayload> input);
        Task<BaseResponseDto<QuestionType>> GetSingle(BaseRequestDto<IdPayload> input);
        Task<BaseResponseDto<List<QuestionType>>> GetAll(BaseRequestDto<string> input);
    }
}
