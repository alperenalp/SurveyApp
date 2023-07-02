using SurveyApp.DTOs.Requests;
using SurveyApp.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Services
{
    public interface IOptionService
    {
        Task CreateOptionAsync(CreateNewOptionRequest request);
        Task<IEnumerable<OptionDisplayResponse>> GetOptionsByQuestionIdAsync(int questionId);
    }
}
