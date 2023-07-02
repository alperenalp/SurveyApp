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
    public class OptionService : IOptionService
    {
        private readonly IOptionRepository _repository;
        private readonly IMapper _mapper;

        public OptionService(IOptionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateOptionAsync(CreateNewOptionRequest request)
        {
            var option = request.ConvertToOption(_mapper);
            await _repository.CreateAsync(option);
        }

        public async Task<IEnumerable<OptionDisplayResponse>> GetOptionsByQuestionIdAsync(int questionId)
        {
            var options = await _repository.GetAllByQuestionId(questionId);
            var response = options.ConvertToDto<IEnumerable<OptionDisplayResponse>>(_mapper);
            return response;
        }
    }
}
