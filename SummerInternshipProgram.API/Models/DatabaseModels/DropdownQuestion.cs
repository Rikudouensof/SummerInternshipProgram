namespace SummerInternshipProgram.API.Models.DatabaseModels
{
    public class DropdownQuestion : BaseQuestionModel
    {
        

        public List<string> QuestionOptions { get; set; }

        public string Answer { get; set; }
    }
}
