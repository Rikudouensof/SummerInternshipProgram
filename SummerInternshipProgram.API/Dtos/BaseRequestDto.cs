using System.ComponentModel.DataAnnotations;

namespace SummerInternshipProgram.API.Dtos
{
    public class BaseRequestDto<T> where T : class
    {

        [Required]
        public string RequestId { get; set; }


        [Required]
        public string Ip { get; set; }



        [Required]
        public T Request { get; set; }


    }
}
