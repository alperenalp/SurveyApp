namespace SurveyApp.MVC.Models
{
    public class SurveyDisplayVM
    {
        public string SurveyTitle { get; set; }
        public IEnumerable<QuestionDisplayVM> Questions {get; set;}
    }
}
