﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SurveyApp.API.Models;
using SurveyApp.DTOs.Requests;
using SurveyApp.Entities;
using SurveyApp.MVC.Models;
using SurveyApp.Services;

namespace SurveyApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly IQuestionService _questionService;
        private readonly IOptionService _optionService;

        public SurveyController(IOptionService optionService, IQuestionService questionService, ISurveyService surveyService)
        {
            _optionService = optionService;
            _questionService = questionService;
            _surveyService = surveyService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(SurveyRequestVM surveyVM)
        {
            if (ModelState.IsValid)
            {
                var survey = new CreateNewSurveyRequest { Title = surveyVM.Title, UserId = surveyVM.UserId };
                var surveyId = await _surveyService.CreateSurveyAsync(survey);
                foreach (var questionVM in surveyVM.Questions)
                {
                    var question = new CreateNewQuestionRequest { Title = questionVM.Title, SurveyId = surveyId };
                    var questionId = await _questionService.CreateQuestionAsync(question);
                    foreach (var optionVM in questionVM.Options)
                    {
                        var option = new CreateNewOptionRequest { Title = optionVM.Title, QuestionId = questionId };
                        await _optionService.CreateOptionAsync(option);
                    }
                }
                string link = "https://" + HttpContext.Request.Host.ToString() + Url.Action(nameof(FillSurvey), new { surveyId });
                return Ok(link);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Share(int surveyId)
        {
            if (await _surveyService.IsSurveyExistsAsync(surveyId))
            {
                var link = "https://" + HttpContext.Request.Host.ToString() + Url.Action(nameof(FillSurvey), new { surveyId });
                return Ok(link);
            }
            return NotFound();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FillSurvey(int surveyId)
        {
            if (await _surveyService.IsSurveyExistsAsync(surveyId))
            {
                var survey = await _surveyService.GetSurveyByIdAsync(surveyId);
                var questionListVM = new List<QuestionDisplayVM>();

                var questions = await _questionService.GetQuestionsBySurveyIdAsync(survey.Id);
                foreach (var question in questions)
                {
                    var options = await _optionService.GetOptionsByQuestionIdAsync(question.Id);
                    var optionListVM = new List<OptionDisplayVM>();
                    foreach (var option in options)
                    {
                        var optionVM = new OptionDisplayVM { Title = option.Title };
                        optionListVM.Add(optionVM);
                    }
                    var questionVM = new QuestionDisplayVM { Title = question.Title, Options = optionListVM };
                    questionListVM.Add(questionVM);
                }
                var model = new SurveyDisplayVM
                {
                    SurveyTitle = survey.Title,
                    Questions = questionListVM,
                };
                return Ok(model);
            }
            return NotFound();
        }


    }

}
