using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.VisualBasic.FileIO;
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
        private readonly IUserService _userService;
        public SurveyController(ISurveyService surveyService, IOptionService optionService, IQuestionService questionService, IFilledSurveyService filledSurveyService, IUserService userService)
        {
            _surveyService = surveyService;
            _optionService = optionService;
            _questionService = questionService;
            _filledSurveyService = filledSurveyService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var surveys = await _surveyService.GetAllAsync();
            return View(surveys);
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


        [HttpGet("[action]")]
        public async Task<IActionResult> Statistics(int surveyId)
        {
            var survey = await _surveyService.GetSurveyByIdAsync(surveyId);

            var questionListVM = await getQuestionDisplayVMListAsync(surveyId);

            var filledSurveys = await _surveyService.GetFilledSurveyListBySurveyIdAsync(surveyId);

            var filledSurveyListVM = await getFilledSurveyDisplayVMListAsync(surveyId, filledSurveys, questionListVM);

            var model = getStatisticsDisplayVM(survey, questionListVM, filledSurveyListVM);

            return View(model);
        }


        private async Task<List<FilledSurveyDisplayVM>> getFilledSurveyDisplayVMListAsync(int surveyId, IEnumerable<FilledSurveyDisplayResponse> filledSurveys, List<QuestionDisplayVM> questionListVM)
        {
            var filledSurveyListVM = new List<FilledSurveyDisplayVM>();
            foreach (var filledSurvey in filledSurveys)
            {
                var filledSurveyVM = await getFilledSurveyDisplayVMAsync(surveyId, filledSurvey);

                filledSurveyListVM.Add(filledSurveyVM);
            }

            return filledSurveyListVM;
        }

        private async Task<FilledSurveyDisplayVM> getFilledSurveyDisplayVMAsync(int surveyId, FilledSurveyDisplayResponse filledSurvey)
        {
            var filledSurveyOptions = await _surveyService.GetFilledSurveyOptionsByFSId(filledSurvey.Id);
            var filledSurveyOptionListVM = await getFilledSurveyOptionListVMAsync(filledSurveyOptions, surveyId);
            var filledSurveyVM = new FilledSurveyDisplayVM
            {
                CreatedAt = filledSurvey.CreatedAt,
                Email = filledSurvey.Email,
                Id = filledSurvey.Id,
                SurveyId = surveyId,
                FilledSurveyOptions = filledSurveyOptionListVM,
            };
            return filledSurveyVM;
        }

        private async Task<List<FilledSurveyOptionVM>> getFilledSurveyOptionListVMAsync(IEnumerable<FilledSurveyOptionDisplayResponse> filledSurveyOptions, int surveyId)
        {
            var filledSurveyOptionsVM = new List<FilledSurveyOptionVM>();
            foreach (var filledSurveyOption in filledSurveyOptions)
            {
                var optionVM = await getOptionDisplayVMAsync(filledSurveyOption, surveyId);
                var filledSurveyOptionVM = new FilledSurveyOptionVM
                {
                    OptionId = filledSurveyOption.OptionId,
                    Text = filledSurveyOption.Text,
                    Option = optionVM,
                };

                filledSurveyOptionsVM.Add(filledSurveyOptionVM);
            }

            return filledSurveyOptionsVM;
        }

        private async Task<OptionDisplayVM> getOptionDisplayVMAsync(FilledSurveyOptionDisplayResponse filledSurveyOption, int surveyId)
        {
            var questionListVM = await getQuestionDisplayVMListAsync(surveyId);
            var optionVM = new OptionDisplayVM();
            foreach (var question in questionListVM)
            {
                foreach (var option in question.Options)
                {
                    if (filledSurveyOption.OptionId == option.Id)
                    {
                        optionVM.Id = option.Id;
                        optionVM.Title = option.Title;
                        optionVM.Question = question;
                    }
                }
            }

            return optionVM;
        }

        private StatisticsCollection getStatisticsDisplayVM(SurveyDisplayResponse survey, IList<QuestionDisplayVM> questionListVM, IList<FilledSurveyDisplayVM> filledSurveyListVM)
        {
            return new StatisticsCollection
            {
                SurveyId = survey.Id,
                SurveyTitle = survey.Title,
                CreatedAt = survey.CreatedAt,
                Questions = questionListVM,
                FilledSurveys = filledSurveyListVM,
            };
        }

        private async Task<List<FilledSurveyOptionVM>> getNotCheckedCheckboxExtractedFSOptionsAsync(SurveyAnswerRequestVM model)
        {
            var questions = await getQuestionDisplayVMListAsync(model.Survey.SurveyId);
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
            List<QuestionDisplayVM> questionListVM = await getQuestionDisplayVMListAsync(surveyId);
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

        private async Task<List<QuestionDisplayVM>> getQuestionDisplayVMListAsync(int surveyId)
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
