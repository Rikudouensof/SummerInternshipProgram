using SummerInternshipProgram.API.Models.HelperModel;

namespace SummerInternshipProgram.API.Models.DatabaseModels
{
    public class DropdownQuestion : BaseQuestionDbModel
    {
        

        public List<QuestionItem> QuestionOptions { get; set; }

        public QuestionItem? Answer { get; set; }
    }
}
