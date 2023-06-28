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
    public class QuestionService : IQuestionService
    {
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _repository;

        public QuestionService(IQuestionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> CreateQuestionAsync(CreateNewQuestionRequest request)
        {
            var question = request.ConvertToQuestion(_mapper);
            int id = await _repository.CreateQuestionAsync(question);
            return id;
        }
    }
}
