using SummerInternshipProgram.API.Dtos.DatabaseServiceDto;
using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;

namespace SummerInternshipProgram.API.Services.Interfaces
{
    public interface IDropdownQuestionService
    {
        Task<BaseResponseDto<string>> Add(BaseRequestDto<DropdownQuestion> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<DropdownQuestion> input, string applicantId);
        Task<BaseResponseDto<BoolPayload>> Delete(BaseRequestDto<IdPayload> input, string applicantId);
        Task<BaseResponseDto<DropdownQuestion>> GetSingle(BaseRequestDto<IdPayload> input, string applicantId);
        Task<BaseResponseDto<List<DropdownQuestion>>> GetAll(BaseRequestDto<string> input, string applicantId);
    }
}
