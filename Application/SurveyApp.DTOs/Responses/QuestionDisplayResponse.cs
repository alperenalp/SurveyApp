using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.DTOs.Responses
{
    public class QuestionDisplayResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
    }
}
