using SurveyApp.Entities;

namespace SurveyApp.MVC.Models
{
    public class SurveyAnswerDisplayVM
    {
        public SurveyDisplayVM Survey { get; set; }
        public IList<FilledSurveyOptionVM> FilledSurveyOptions { get; set; }
    }
}
