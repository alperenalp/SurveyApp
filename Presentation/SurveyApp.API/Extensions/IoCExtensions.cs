using Microsoft.EntityFrameworkCore;
using SurveyApp.Infrastructure.Data;
using SurveyApp.Infrastructure.Repositories;
using SurveyApp.Services;
using SurveyApp.Services.Mappings;

namespace SurveyApp.API.Extensions
{
    public static class IoCExtensions
    {
        public static WebApplicationBuilder AddInjections(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISurveyService, SurveyService>();
            builder.Services.AddScoped<ISurveyRepository, EFSurveyRepository>();

            builder.Services.AddScoped<IQuestionService, QuestionService>();
            builder.Services.AddScoped<IQuestionRepository, EFQuestionRepository>();

            builder.Services.AddScoped<IOptionService, OptionService>();
            builder.Services.AddScoped<IOptionRepository, EFOptionRepository>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, EFUserRepository>();

            builder.Services.AddAutoMapper(typeof(MapProfile));

            var connectionString = builder.Configuration.GetConnectionString("db");
            builder.Services.AddDbContext<SurveyAppDbContext>(opt => opt.UseSqlServer(connectionString));

            return builder;
        }
    }
}
