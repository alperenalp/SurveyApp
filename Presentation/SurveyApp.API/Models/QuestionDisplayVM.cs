namespace SurveyApp.MVC.Models
{
    public class QuestionDisplayVM
    {
        public string Title { get; set; }
        public IEnumerable<OptionDisplayVM> Options { get; set; }
    }
}
