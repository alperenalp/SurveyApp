using SurveyApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Repositories
{
    public interface ISurveyRepository: IRepository<Survey>
    {
        Task<int> CreateSurveyAsync(Survey survey);
        Task<IList<FilledSurvey>> GetAllFilledSurveyAsync();
        Task<Survey> GetByIdForFillAsync(int id);
        Task<IList<FilledSurveyOption>> GetFilledSurveyOptionsByFSId(int filledSurveyId);
        Task<IList<FilledSurvey>> GetFilledSurveyListBySurveyIdAsync(int surveyId);
    }
}
