using AutoMapper;
using SurveyApp.DTOs.Requests;
using SurveyApp.DTOs.Responses;
using SurveyApp.Entities;
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

        public async Task<IEnumerable<SurveyDisplayResponse>> GetAllAsync()
        {
            var surveys = await _repository.GetAllAsync();
            var response = surveys.ConvertToDto<IEnumerable<SurveyDisplayResponse>>(_mapper);
            return response;
        }

        public async Task<IEnumerable<FilledSurveyDisplayResponse>> GetAllFilledSurveyAsync()
        {
            var filledSurveys = await _repository.GetAllFilledSurveyAsync();
            var response = filledSurveys.ConvertToDto<IEnumerable<FilledSurveyDisplayResponse>>(_mapper);
            return response;
        }

        public async Task<IEnumerable<FilledSurveyOptionDisplayResponse>> GetFilledSurveyOptionsByFSId(int filledSurveyId)
        {
            var filledSurveyOptions = await _repository.GetFilledSurveyOptionsByFSId(filledSurveyId);
            var response = filledSurveyOptions.ConvertToDto<IEnumerable<FilledSurveyOptionDisplayResponse>>(_mapper);
            return response;
        }

        public async Task<IEnumerable<FilledSurveyDisplayResponse>> GetFilledSurveyListBySurveyIdAsync(int surveyId)
        {
            var filledSurveys = await _repository.GetFilledSurveyListBySurveyIdAsync(surveyId);
            var response = filledSurveys.ConvertToDto<IEnumerable<FilledSurveyDisplayResponse>>(_mapper);
            return response;
        }

        public async Task<SurveyDisplayResponse> GetSurveyByIdAsync(int id)
        {
            var survey = await _repository.GetByIdAsync(id);
            var response = survey.ConvertToDto<SurveyDisplayResponse>(_mapper);
            return response;
        }

        public async Task<bool> IsSurveyExistsAsync(int id)
        {
            return await _repository.IsExistsAsync(id);
        }
    }
}
