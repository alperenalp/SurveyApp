using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyApp.API.Models;
using SurveyApp.DTOs.Requests;
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
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
