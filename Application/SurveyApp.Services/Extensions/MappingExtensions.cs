using AutoMapper;
using SurveyApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Services.Extensions
{
    public static class MappingExtensions
    {
        //Survey
        public static T ConvertToDto<T>(this Survey survey, IMapper mapper)
        {
            return mapper.Map<T>(survey);
        }

        public static Survey ConvertToSurvey<T>(this T request, IMapper mapper)
        {
            return mapper.Map<Survey>(request);
        }


        //Question
        public static T ConvertToDto<T>(this Question question, IMapper mapper)
        {
            return mapper.Map<T>(question);
        }

        public static Question ConvertToQuestion<T>(this T request, IMapper mapper)
        {
            return mapper.Map<Question>(request);
        }


        //Option
        public static T ConvertToDto<T>(this Option option, IMapper mapper)
        {
            return mapper.Map<T>(option);
        }

        public static Option ConvertToOption<T>(this T request, IMapper mapper)
        {
            return mapper.Map<Option>(request);
        }


    }
}
