namespace QuizTechnicalTest.Web.ViewModels
{
    public class QuizResultViewModel
    {
        public List<QuizResultCandidateViewModel>? QuizResultCandidates { get; set; }

        public int Id { get; set; }

        public string? Code { get; set; }

        public DateTime Date { get; set; }
    }
}
