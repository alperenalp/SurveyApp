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
    public class FilledSurveyService : IFilledSurveyService
    {
        private readonly IFilledSurveyRepository _repository;
        private readonly IMapper _mapper;

        public FilledSurveyService(IFilledSurveyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateFilledSurveyAsync(CreateNewFilledSurveyRequest request)
        {
            var filledSurvey = request.ConvertToFilledSurvey(_mapper);
            return await _repository.CreateFilledSurveyAsync(filledSurvey);
        }

        public async Task CreateFilledSurveyOptionAsync(CreateNewFilledSurveyOptionRequest request)
        {
            var filledSurveyOption = request.ConvertToFilledSurveyOption(_mapper);
            await _repository.CreateFilledSurveyOptionAsync(filledSurveyOption);
        }

        public async Task<bool> CreateWithIsSuccessAsync(CreateNewFilledSurveyRequest request)
        {
            var filledSurvey = request.ConvertToFilledSurvey(_mapper);
            return await _repository.CreateWithIsSuccesAsync(filledSurvey);
        }

        public async Task<bool> IsFilledSurveyExistsAsync(int filledSurveyId)
        {
            return await _repository.IsExistsAsync(filledSurveyId);
        }
    }
}
