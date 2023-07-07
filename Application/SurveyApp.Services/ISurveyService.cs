using SurveyApp.DTOs.Requests;
using SurveyApp.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Services
{
    public interface ISurveyService
    {
        Task<int> CreateSurveyAsync(CreateNewSurveyRequest request);
        Task<IEnumerable<SurveyDisplayResponse>> GetAllAsync();
        Task<IEnumerable<FilledSurveyDisplayResponse>> GetAllFilledSurveyAsync();
        Task<IEnumerable<FilledSurveyOptionDisplayResponse>> GetFilledSurveyOptionsByFSId(int filledSurveyId);
        Task<IEnumerable<FilledSurveyDisplayResponse>> GetFilledSurveyListBySurveyIdAsync(int surveyId);
        Task<SurveyDisplayResponse> GetSurveyByIdAsync(int id);
        Task<bool> IsSurveyExistsAsync(int id);
    }
}
