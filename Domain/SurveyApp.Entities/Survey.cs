using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SurveyApp.Entities
{
    public class Survey : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Question> Questions { get; set; }
        public IList<FilledSurvey> FilledSurveys { get; set; }
    }
}
