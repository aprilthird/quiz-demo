using QuizTechnicalTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTechnicalTest.Infrastructure.Repositories
{
    public interface ICandidateRepository
    {
        public Task<ICollection<Candidate>> GetAll();
    }
}
