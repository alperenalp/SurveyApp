using SurveyApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Services
{
    public class FilledSurveyService : IFilledSurveyService
    {
        private readonly IFilledSurveyRepository _repository;

        public FilledSurveyService(IFilledSurveyRepository repository)
        {
            _repository = repository;
        }
    }
}
