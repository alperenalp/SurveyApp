namespace SurveyApp.MVC.Models
{
    public class QuestionDisplayVM
    {
        public string Title { get; set; }
        public IList<OptionDisplayVM> Options { get; set; }
    }
}
