using QuizTechnicalTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTechnicalTest.Infrastructure.Repositories
{
    public interface IQuizRepository
    {
        Task<int> Create(Quiz quiz);

        Task<Quiz?> GetById(int id);

        Task<Quiz?> GetByDate(DateTime dateTime);
        
        Task<Quiz?> GetByCode(string code);

        Task<int> CalculateMatchingCandidates(int quizId);
    }
}
