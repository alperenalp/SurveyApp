using SurveyApp.DTOs.Requests;

namespace SurveyApp.API.Models
{
    public class SurveyRequestVM
    {
        public string Title { get; set; }
        public int UserId { get; set; }
        public IList<QuestionRequestVM> Questions { get; set; }
    }
}
