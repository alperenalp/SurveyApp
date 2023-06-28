namespace SurveyApp.API.Models
{
    public class QuestionRequestVM
    {
        public string Title { get; set; }
        public List<OptionRequestVM> Options { get; set; }
    }
}
