using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.DTOs.Requests
{
    public class CreateNewFilledSurveyRequest
    {
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
        public int SurveyId { get; set; }
    }
}
