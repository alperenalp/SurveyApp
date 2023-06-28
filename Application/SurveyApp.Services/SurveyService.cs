using AutoMapper;
using SurveyApp.DTOs.Requests;
using SurveyApp.Infrastructure.Repositories;
using SurveyApp.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _repository;
        private readonly IMapper _mapper;

        public SurveyService(ISurveyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateSurveyAsync(CreateNewSurveyRequest request)
        {
            var survey = request.ConvertToSurvey(_mapper);
            survey.CreatedAt = DateTime.Now;
            int surveyId = await _repository.CreateSurveyAsync(survey);
            return surveyId;
        }
    }
}
