using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SurveyApp.API.Models;
using SurveyApp.DTOs.Requests;
using SurveyApp.Entities;
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
                string link = HttpContext.Request.Host.ToString() + Url.Action("Test", new { id = surveyId });
                return Ok(link);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> Test(int id)
        {
            return Ok();
        }

        [HttpGet("{surveyId}")]
        public async Task<IActionResult> Share(int surveyId)
        {
            if (await _surveyService.IsSurveyExistsAsync(surveyId))
            {
                var link = HttpContext.Request.Host.ToString() + Url.Action("Test", new { id = surveyId });
                return Ok(link);
            }
            return NotFound();
        }
    }

}
