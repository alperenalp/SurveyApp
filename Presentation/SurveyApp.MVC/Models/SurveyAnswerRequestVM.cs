namespace SurveyApp.MVC.Models
{
    public class SurveyAnswerRequestVM
    {
        public string Email { get; set; }
        public SurveyRequestVM Survey { get; set; }
        public IList<FilledSurveyOptionVM> FilledSurveyOptions { get; set; }
    }
}
