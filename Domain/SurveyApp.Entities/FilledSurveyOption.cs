using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Entities
{
    public class FilledSurveyOption
    {
        public int FilledSurveyId { get; set; }
        public FilledSurvey FilledSurvey { get; set; }
        public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}
