using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.DTOs.Requests
{
    public class CreateNewQuestionRequest
    {
        public string Title { get; set; }
        public int Type { get; set; }
        public int SurveyId { get; set; }
    }
}
