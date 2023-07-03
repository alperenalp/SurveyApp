using SurveyApp.Entities;

namespace SurveyApp.MVC.Models
{
    public class QuestionDisplayVM
    {
        public string Title { get; set; }
        public int Type { get; set; }
        public IEnumerable<OptionDisplayVM> Options { get; set; }
    }
}
