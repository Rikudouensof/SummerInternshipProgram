using SummerInternshipProgram.API.Dtos.DatabaseServiceDto;
using SummerInternshipProgram.API.Dtos;
using SummerInternshipProgram.API.Models.DatabaseModels;
using SummerInternshipProgram.API.Models.HelperModel;

namespace SummerInternshipProgram.API.Services.Interfaces
{
    public interface IInternshipProgramService 
    {
        Task<BaseResponseDto<string>> Add(BaseRequestDto<InternshipProgram> input);
        Task<BaseResponseDto<BoolPayload>> Update(BaseRequestDto<InternshipProgram> input);
        Task<BaseResponseDto<BoolPayload>> Delete(BaseRequestDto<IdPayload> input);
        Task<BaseResponseDto<InternshipProgram>> GetSingle(BaseRequestDto<IdPayload> input);
        Task<BaseResponseDto<List<InternshipProgram>>> GetAll(BaseRequestDto<string> input);

    }
}
