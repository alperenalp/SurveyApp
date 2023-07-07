using SurveyApp.DTOs.Responses;
using SurveyApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace SurveyApp.MVC.Models
{
    public class FilledSurveyDisplayVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SurveyId { get; set; }
        public IList<FilledSurveyOptionVM> FilledSurveyOptions { get; set; }
    }
}