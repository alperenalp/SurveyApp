namespace SurveyApp.MVC.Models
{
    public class SurveyDisplayVM
    {
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public DateTime? CreatedAt { get; set; }
        public IList<QuestionDisplayVM> Questions {get; set;}
        public IList<FilledSurveyDisplayVM>? FilledSurveys { get; set; }
    }
}
