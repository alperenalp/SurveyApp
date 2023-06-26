using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Entities
{
    public class Question : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }
        public IList<Option> Options { get; set; }
    }
}
