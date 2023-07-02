using AutoMapper;
using SurveyApp.DTOs.Requests;
using SurveyApp.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Services
{
    public interface IQuestionService
    {
        Task<int> CreateQuestionAsync(CreateNewQuestionRequest request);
        Task<IEnumerable<QuestionDisplayResponse>> GetQuestionsBySurveyIdAsync(int surveyId);
    }
}
