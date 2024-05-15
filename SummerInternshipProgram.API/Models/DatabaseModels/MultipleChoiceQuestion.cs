using SummerInternshipProgram.API.Models.HelperModel;

namespace SummerInternshipProgram.API.Models.DatabaseModels
{
    public class MultipleChoiceQuestion : BaseQuestionDbModel
    {

        

        public List<QuestionItem> QuestionOptions { get; set; }

        public List<QuestionItem>? QuestionAnswers { get; set; }
    }
}
