using QuizTechnicalTest.Domain.Entities;

namespace QuizTechnicalTest.Web.ViewModels
{
    public class QuizAnswersViewModel
    {
        public ICollection<QuizAnswerViewModel>? QuizAnswers { get; set; }
    }
}
