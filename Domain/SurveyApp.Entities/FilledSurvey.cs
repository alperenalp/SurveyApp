using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Entities
{
    public class FilledSurvey : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public IList<FilledSurveyOption> FilledSurveyOptions { get; set; }
    }
}
