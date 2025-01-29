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
        private readonly ICandidateRepository _candidateRepository;

        public HomeController(ILogger<HomeController> logger, 
            IQuestionAnswerRepository questionAnswerRepository,
            IQuizRepository quizRepository,
            ICandidateRepository candidateRepository)
        {
            _logger = logger;
            _questionAnswerRepository = questionAnswerRepository;
            _quizRepository = quizRepository;
            _candidateRepository = candidateRepository;
        }

        public async Task<IActionResult> Index()
        {
            var dbResults = await _questionAnswerRepository.GetAll();
            var questions = dbResults
                .GroupBy(x => new { x.QuestionId, x.QuestionDescription })
                .Select(x => new QuestionViewModel
                {
                    Id = x.Key.QuestionId,
                    Description = x.Key.QuestionDescription,
                    Answers = x.Select(y => new AnswerViewModel
                    {
                        Id = y.AnswerId,
                        Description = y.AnswerDescription,
                    }).ToList()
                }).ToList();
            return View(questions);
        }

        [HttpPost]
        public async Task<IActionResult> IndexPost(FormResponseViewModel model)
        {
            var quizCode = new DateTime().ToString();
            var quiz = new Quiz
            {
                Code = quizCode,
                Date = DateTime.Now
            };
            var result = await _quizRepository.Create(quiz);
            return RedirectToAction("Result");
        }

        public async Task<IActionResult> Result()
        {
            var dbResults = await _candidateRepository.GetAll();
            var candidates = dbResults.Select(x => new CandidateViewModel
            {
                Id = x.Id,
                Age = x.Age,
                Group = x.Group,
                Name = x.Name,
                Position = x.Position,
                Profession = x.Profession,
            });
            return View(candidates);
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
