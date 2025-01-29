using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTechnicalTest.Domain.Entities
{
    public class QuizAnswer
    {
        public int QuizId { get; set; }
        public int QuestionId { get; set; }

        public int AnswerId { get; set; }
    }
}
