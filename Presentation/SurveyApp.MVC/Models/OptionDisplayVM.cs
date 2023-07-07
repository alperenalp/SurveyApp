namespace SurveyApp.MVC.Models
{
    public class OptionDisplayVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public QuestionDisplayVM? Question { get; set; }
    }
}
