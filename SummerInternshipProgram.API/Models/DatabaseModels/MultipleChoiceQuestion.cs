namespace SummerInternshipProgram.API.Models.DatabaseModels
{
    public class MultipleChoiceQuestion : BaseQuestionModel
    {

        

        public List<string> QuestionOptions { get; set; }

        public List<string> QuestionAnswers { get; set; }
    }
}
