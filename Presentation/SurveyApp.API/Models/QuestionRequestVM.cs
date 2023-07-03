using SurveyApp.Entities;

namespace SurveyApp.API.Models
{
    public class QuestionRequestVM
    {
        public string Title { get; set; }
        public int Type { get; set; }
        public List<OptionRequestVM> Options { get; set; }
    }
}
