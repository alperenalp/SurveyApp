using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.DTOs.Requests
{
    public class CreateNewSurveyRequest
    {
        public string Title { get; set; }
        public int UserId { get; set; }
    }
}
