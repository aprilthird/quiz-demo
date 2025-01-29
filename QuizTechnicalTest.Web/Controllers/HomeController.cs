using Microsoft.AspNetCore.Mvc;
using QuizTechnicalTest.Domain.Entities;
using QuizTechnicalTest.Infrastructure.Repositories;
using QuizTechnicalTest.Web.Models;
using QuizTechnicalTest.Web.ViewModels;
using System.Diagnostics;

namespace QuizTechnicalTest.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQuestionAnswerRepository _questionAnswerRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IQuizAnswerRepository _quizAnswerRepository;
        private readonly ICandidateRepository _candidateRepository;

        public HomeController(ILogger<HomeController> logger, 
            IQuestionAnswerRepository questionAnswerRepository,
            IQuizRepository quizRepository,
            IQuizAnswerRepository quizAnswerRepository,
            ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _questionAnswerRepository = questionAnswerRepository;
            _quizRepository = quizRepository;
            _quizAnswerRepository = quizAnswerRepository;
            _candidateRepository = candidateRepository;
        }

        public async Task<IActionResult> Index()
        {
            var questionAnswers = await _questionAnswerRepository.GetAll();
            var model = questionAnswers
                .GroupBy(x => new { x.QuestionId, x.QuestionDescription, x.QuestionPicture })
                .Select(x => new QuestionViewModel
                {
                    Id = x.Key.QuestionId,
                    Description = x.Key.QuestionDescription,
                    Picture = x.Key.QuestionPicture,
                    Answers = x.Select(y => new AnswerViewModel
                    {
                        Id = y.AnswerId,
                        Description = y.AnswerDescription,
                    }).ToList()
                }).ToList();
            return View(model);
        }

        public async Task<IActionResult> IndexPost(QuizAnswersViewModel model)
        {
            var quiz = new Quiz
            {
                Code = Path.GetRandomFileName().Replace(".", string.Empty)[..10],
                Date = DateTime.UtcNow
            };
            
            await _quizRepository.Create(quiz);
            
            var createdQuiz = await _quizRepository.GetByCode(quiz.Code);

            var quizAnswers = model.QuizAnswers?.Select(x => new QuizAnswer
            {
                QuizId = createdQuiz!.Id,
                AnswerId = x.AnswerId,
                QuestionId = x.QuestionId,
            }).ToList();

            await _quizAnswerRepository.CreateRange(quizAnswers!);

            await _quizRepository.CalculateMatchingCandidates(createdQuiz!.Id);

            return RedirectToAction("Result", new { quizId = createdQuiz!.Id });
        }

        public async Task<IActionResult> Result(int quizId)
        {
            var quiz = await _quizRepository.GetById(quizId);
            var candidates = new List<QuizResultCandidateViewModel>();

            if (quiz!.Candidate1Id.HasValue)
            {
                var candidate1 = await GetQuizResultCandidateModel(quiz.Candidate1Id.Value, quiz.Candidate1Percent ?? 0);
                if (candidate1 != null)
                {
                    candidates.Add(candidate1);
                }
            }

            if (quiz!.Candidate2Id.HasValue)
            {
                var candidate2 = await GetQuizResultCandidateModel(quiz.Candidate2Id.Value, quiz.Candidate2Percent ?? 0);
                if (candidate2 != null)
                {
                    candidates.Add(candidate2);
                }
            }

            if (quiz!.Candidate3Id.HasValue)
            {
                var candidate3 = await GetQuizResultCandidateModel(quiz.Candidate3Id.Value, quiz.Candidate3Percent ?? 0);
                if (candidate3 != null)
                { 
                    candidates.Add(candidate3);
                }
            }

            if (quiz!.Candidate4Id.HasValue)
            {
                var candidate4 = await GetQuizResultCandidateModel(quiz.Candidate4Id.Value, quiz.Candidate4Percent ?? 0);
                if (candidate4 != null)
                {
                    candidates.Add(candidate4);
                }
            }

            if (quiz!.Candidate5Id.HasValue)
            {
                var candidate5 = await GetQuizResultCandidateModel(quiz.Candidate5Id.Value, quiz.Candidate5Percent ?? 0);
                if (candidate5 != null)
                {
                    candidates.Add(candidate5);
                }
            }

            var model = new QuizResultViewModel
            {
                QuizResultCandidates = candidates,
                Id = quiz.Id,
                Code = quiz.Code,
                Date = quiz.Date,
            };

            return View(model);
        }

        private async Task<QuizResultCandidateViewModel?> GetQuizResultCandidateModel(int quizId, int percent)
        {
            var candidate = await _candidateRepository.GetById(quizId);
            if (candidate == null) 
            {
                return null;
            }
            var model = new QuizResultCandidateViewModel
            {
                Percent = percent,
                Candidate = new CandidateViewModel
                {
                    Id = candidate.Id,
                    Name = candidate.Name,
                    Group = candidate.Group,
                    Age = candidate.Age,
                    Profession = candidate.Profession,
                    Position = candidate.Position,
                    GovPlan = candidate.GovPlan,
                    Proposal = candidate.Proposal,
                    Picture = candidate.Picture,
                },
            };
            return model;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
