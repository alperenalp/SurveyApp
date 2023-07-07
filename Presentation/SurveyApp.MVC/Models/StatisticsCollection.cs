namespace SurveyApp.MVC.Models
{
    public class StatisticsCollection
    {
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<QuestionDisplayVM> Questions { get; set; }
        public IList<FilledSurveyDisplayVM> FilledSurveys { get; set; }
        public void ClearAllFilledSurveys() => FilledSurveys.Clear();
        public int TotalFilledSurveys() => FilledSurveys.Count();
        public IEnumerable<FilledSurveyOptionVM> QuestionOptions(int questionId)
        {
            var filledSurveyOptions = new List<FilledSurveyOptionVM>();
            foreach (var filledSurvey in FilledSurveys)
            {
                foreach (var filledOption in filledSurvey.FilledSurveyOptions)
                {
                    if (filledOption.Option.Question.Id == questionId)
                    {
                        filledSurveyOptions.Add(filledOption);
                    }
                }
            }

            return filledSurveyOptions;
        }
        public IEnumerable<object> CountOptions(IEnumerable<FilledSurveyOptionVM> options)
        {
            return options.GroupBy(t => t.OptionId).Select(t => new { OptionId = t.Key, Count = t.Count()} );
        }
    }
}
