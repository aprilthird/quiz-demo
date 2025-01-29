using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTechnicalTest.Domain.Entities
{
    public class Quiz
    {
        public int Id { get; set; }

        public string? Code { get; set; }

        public DateTime Date { get; set; }

        public int? Candidate1Id { get; set; }
        
        public short? Candidate1Percent { get; set; }

        public int? Candidate2Id { get; set; }

        public short? Candidate2Percent { get; set; }

        public int? Candidate3Id { get; set; }

        public short? Candidate3Percent { get; set; }

        public int? Candidate4Id { get; set; }

        public short? Candidate4Percent { get; set; }

        public int? Candidate5Id { get; set; }

        public short? Candidate5Percent { get; set; }
    }
}
