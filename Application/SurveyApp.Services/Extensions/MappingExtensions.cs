﻿using AutoMapper;
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

        public static T ConvertToDto<T>(this IList<Survey> request, IMapper mapper)
        {
            return mapper.Map<T>(request);
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

        public static T ConvertToDto<T>(this IList<Question> request, IMapper mapper)
        {
            return mapper.Map<T>(request);
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

        public static T ConvertToDto<T>(this IList<Option> request, IMapper mapper)
        {
            return mapper.Map<T>(request);
        }

        public static Option ConvertToOption<T>(this T request, IMapper mapper)
        {
            return mapper.Map<Option>(request);
        }

        //User
        public static T ConvertToDto<T>(this User user, IMapper mapper)
        {
            return mapper.Map<T>(user);
        }

        public static User ConvertToUser<T>(this T request, IMapper mapper)
        {
            return mapper.Map<User>(request);
        }


        //FilledSurvey
        public static FilledSurvey ConvertToFilledSurvey<T>(this T request, IMapper mapper)
        {
            return mapper.Map<FilledSurvey>(request);
        }

        public static T ConvertToDto<T>(this IList<FilledSurvey> request, IMapper mapper)
        {
            return mapper.Map<T>(request);
        }


        //FilledSurveyOption
        public static FilledSurveyOption ConvertToFilledSurveyOption<T>(this T request, IMapper mapper)
        {
            return mapper.Map<FilledSurveyOption>(request);
        }

        public static T ConvertToDto<T>(this IList<FilledSurveyOption> request, IMapper mapper)
        {
            return mapper.Map<T>(request);
        }
    }
}
