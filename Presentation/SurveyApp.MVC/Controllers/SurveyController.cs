using Microsoft.AspNetCore.Mvc;
using SurveyApp.DTOs.Requests;
using SurveyApp.DTOs.Responses;
using SurveyApp.Entities;
using SurveyApp.MVC.Models;
using SurveyApp.Services;

namespace SurveyApp.MVC.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;
        private readonly IFilledSurveyService _filledSurveyService;
        private readonly IQuestionService _questionService;
        private readonly IOptionService _optionService;
        public SurveyController(ISurveyService surveyService, IOptionService optionService, IQuestionService questionService, IFilledSurveyService filledSurveyService)
        {
            _surveyService = surveyService;
            _optionService = optionService;
            _questionService = questionService;
            _filledSurveyService = filledSurveyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FillSurvey(int surveyId)
        {
            if (await _surveyService.IsSurveyExistsAsync(surveyId))
            {
                SurveyAnswerDisplayVM model = await getSurveyAnswerRequestVMAsync(surveyId);
                return View(model);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> FillSurvey(SurveyAnswerRequestVM model)
        {
            if (ModelState.IsValid)
            {
                var filledSurvey = getFilledSurveyRequest(model);
                var filledSurveyId = await _filledSurveyService.CreateFilledSurveyAsync(filledSurvey);

                if (await _filledSurveyService.IsFilledSurveyExistsAsync(filledSurveyId))
                {
                    var extractedOptions = await getNotCheckedCheckboxExtractedFSOptionsAsync(model);
                    await createFilledSurveyOptionsAsync(filledSurveyId, extractedOptions);

                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction(nameof(FillSurvey));
            }
            return View(ModelState);
        }

        private async Task<List<FilledSurveyOptionVM>> getNotCheckedCheckboxExtractedFSOptionsAsync(SurveyAnswerRequestVM model)
        {
            var questions = await getQuestionListVMAsync(model.Survey.SurveyId);
            var checkedOptions = getCheckedFilledSurveyOptions(model.FilledSurveyOptions);
            var extractedOptions = new List<FilledSurveyOptionVM>();
            foreach (var question in questions)
            {
                if (!question.Type.Equals(3)) //checkbox
                {
                    foreach (var option in question.Options)
                    {
                        foreach (var fsOption in model.FilledSurveyOptions)
                        {
                            if (fsOption.OptionId == option.Id)
                            {
                                extractedOptions.Add(fsOption);
                            }
                        }
                    }
                }
            }
            foreach (var checkedOption in checkedOptions)
            {
                extractedOptions.Add(checkedOption);
            }

            return extractedOptions;
        }

        private IList<FilledSurveyOptionVM> getCheckedFilledSurveyOptions(IList<FilledSurveyOptionVM> filledSurveyOptionsVM)
        {
            var checkedOptions = new List<FilledSurveyOptionVM>();
            foreach (var checkboxOption in filledSurveyOptionsVM)
            {
                if (checkboxOption.IsChecked)
                {
                    checkedOptions.Add(checkboxOption);
                }
            }
            return checkedOptions;
        }

        private async Task<SurveyAnswerDisplayVM> getSurveyAnswerRequestVMAsync(int surveyId)
        {
            var survey = await _surveyService.GetSurveyByIdAsync(surveyId);
            List<QuestionDisplayVM> questionListVM = await getQuestionListVMAsync(surveyId);
            var surveyDisplayVM = getSurveyDisplayVM(survey, questionListVM);
            return new SurveyAnswerDisplayVM
            {
                Survey = surveyDisplayVM
            };
        }

        private static SurveyDisplayVM getSurveyDisplayVM(SurveyDisplayResponse survey, List<QuestionDisplayVM> questionListVM)
        {
            return new SurveyDisplayVM
            {
                SurveyId = survey.Id,
                SurveyTitle = survey.Title,
                Questions = questionListVM,
            };
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
                    var optionVM = getOptionDisplayVM(option);
                    optionListVM.Add(optionVM);
                }
                var questionVM = getQuestionVM(question, optionListVM);
                questionListVM.Add(questionVM);
            }

            return questionListVM;
        }

        private static OptionDisplayVM getOptionDisplayVM(OptionDisplayResponse option)
        {
            return new OptionDisplayVM
            {
                Id = option.Id,
                Title = option.Title
            };
        }

        private static QuestionDisplayVM getQuestionVM(QuestionDisplayResponse question, List<OptionDisplayVM> optionListVM)
        {
            return new QuestionDisplayVM
            {
                Id = question.Id,
                Title = question.Title,
                Options = optionListVM,
                Type = question.Type
            };
        }



        private CreateNewFilledSurveyRequest getFilledSurveyRequest(SurveyAnswerRequestVM model)
        {
            return new CreateNewFilledSurveyRequest
            {
                CreatedAt = DateTime.Now,
                Email = model.Email,
                SurveyId = model.Survey.SurveyId
            };
        }

        private async Task createFilledSurveyOptionsAsync(int filledSurveyId, IList<FilledSurveyOptionVM> filledSurveyOptionsVM)
        {
            foreach (var filledSurveyOptionVM in filledSurveyOptionsVM)
            {
                var filledSurveyOption = getFilledSurveyOptionRequest(filledSurveyId, filledSurveyOptionVM);
                await _filledSurveyService.CreateFilledSurveyOptionAsync(filledSurveyOption);
            }
        }

        private static CreateNewFilledSurveyOptionRequest getFilledSurveyOptionRequest(int filledSurveyId, FilledSurveyOptionVM filledSurveyOptionVM)
        {
            return new CreateNewFilledSurveyOptionRequest
            {
                FilledSurveyId = filledSurveyId,
                OptionId = filledSurveyOptionVM.OptionId,
                Text = filledSurveyOptionVM.Text,
            };
        }
    }
}
