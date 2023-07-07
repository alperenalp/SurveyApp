using AutoMapper;
using SurveyApp.DTOs.Requests;
using SurveyApp.DTOs.Responses;
using SurveyApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Services.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Survey, CreateNewSurveyRequest>().ReverseMap();
            CreateMap<Survey, SurveyDisplayResponse>().ReverseMap();

            CreateMap<Question, CreateNewQuestionRequest>().ReverseMap();
            CreateMap<Question, QuestionDisplayResponse>().ReverseMap();

            CreateMap<Option, CreateNewOptionRequest>().ReverseMap();
            CreateMap<Option, OptionDisplayResponse>().ReverseMap();

            CreateMap<User, CreateNewUserRequest>().ReverseMap();
            CreateMap<User, ValidateUserResponse>().ReverseMap();

            CreateMap<FilledSurvey, CreateNewFilledSurveyRequest>().ReverseMap();
            CreateMap<FilledSurvey, FilledSurveyDisplayResponse>().ReverseMap();

            CreateMap<FilledSurveyOption, CreateNewFilledSurveyOptionRequest >().ReverseMap();
            CreateMap<FilledSurveyOption, FilledSurveyOptionDisplayResponse >().ReverseMap();

        }
    }
}
