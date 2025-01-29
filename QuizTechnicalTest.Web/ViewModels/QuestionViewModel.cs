namespace QuizTechnicalTest.Web.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public string? Picture { get; set; }

        public List<AnswerViewModel>? Answers { get; set; }
    }
}
