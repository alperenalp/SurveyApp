namespace SurveyApp.MVC.Models
{
    public class FilledSurveyOptionVM
    {
        public string? Text { get; set; }
        public bool IsChecked { get; set; }
        public int OptionId { get; set; }
        public OptionDisplayVM? Option { get; set; }
    }
}
