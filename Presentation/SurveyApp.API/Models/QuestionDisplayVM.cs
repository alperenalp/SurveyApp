using SurveyApp.Entities;

namespace SurveyApp.MVC.Models
{
    public class QuestionDisplayVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public IEnumerable<OptionDisplayVM> Options { get; set; }
    }
}
