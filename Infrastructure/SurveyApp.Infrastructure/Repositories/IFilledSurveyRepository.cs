using SurveyApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Infrastructure.Repositories
{
    public interface IFilledSurveyRepository : IRepository<FilledSurvey>
    {
        Task<int> CreateFilledSurveyAsync(FilledSurvey filledSurvey);
        Task CreateFilledSurveyOptionAsync(FilledSurveyOption filledSurveyOption);
        Task<bool> CreateWithIsSuccesAsync(FilledSurvey filledSurvey);
    }
}
