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
    }
}
