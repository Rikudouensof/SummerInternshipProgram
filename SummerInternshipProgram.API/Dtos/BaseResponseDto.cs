namespace SummerInternshipProgram.API.Dtos
{
    public class BaseResponseDto<T> where T : class
    {

        public string ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public T Response { get; set; }
    }
}
