using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.DTOs.Responses
{
    public class FilledSurveyOptionDisplayResponse
    {
        public int FilledSurveyId { get; set; }
        public int OptionId { get; set; }
        public string? Text { get; set; }
    }
}
