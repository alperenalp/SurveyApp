using Microsoft.AspNetCore.Mvc;
using SurveyApp.DTOs.Responses;
using SurveyApp.Entities;
using SurveyApp.MVC.Models;
using SurveyApp.Services;

namespace SurveyApp.MVC.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;
        private readonly IQuestionService _questionService;
        private readonly IOptionService _optionService;
        public SurveyController(ISurveyService surveyService, IOptionService optionService, IQuestionService questionService)
        {
            _surveyService = surveyService;
            _optionService = optionService;
            _questionService = questionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FillSurvey(int surveyId)
        {
            if (await _surveyService.IsSurveyExistsAsync(surveyId))
            {
                SurveyDisplayVM model = await getSurveyDisplayModelAsync(surveyId);
                return View(model);
            }
            return NotFound();
        }

        private async Task<SurveyDisplayVM> getSurveyDisplayModelAsync(int surveyId)
        {
            var survey = await _surveyService.GetSurveyByIdAsync(surveyId);
            List<QuestionDisplayVM> questionListVM = await getQuestionListVMAsync(surveyId);
            var model = new SurveyDisplayVM
            {
                SurveyTitle = survey.Title,
                Questions = questionListVM,
            };
            return model;
        }

        private async Task<List<QuestionDisplayVM>> getQuestionListVMAsync(int surveyId)
        {
            var questionListVM = new List<QuestionDisplayVM>();
            var questions = await _questionService.GetQuestionsBySurveyIdAsync(surveyId);
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

            return questionListVM;
        }

        [HttpPost]
        public async Task<IActionResult> FillSurvey()
        {
            return View();
        }
    }
}
