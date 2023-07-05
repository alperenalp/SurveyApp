using SurveyApp.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Services
{
    public interface IFilledSurveyService
    {
        Task<int> CreateFilledSurveyAsync(CreateNewFilledSurveyRequest request);
        Task CreateFilledSurveyOptionAsync(CreateNewFilledSurveyOptionRequest request);
        Task<bool> CreateWithIsSuccessAsync(CreateNewFilledSurveyRequest request);
        Task<bool> IsFilledSurveyExistsAsync(int filledSurveyId);
    }
}
