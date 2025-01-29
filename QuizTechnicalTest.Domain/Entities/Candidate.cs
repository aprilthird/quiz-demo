using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTechnicalTest.Domain.Entities
{
    public class Candidate
    {
        public int Id { get; set; }
        
        public string? Name { get; set; }

        public string? Group { get; set; }

        public int Age { get; set; }

        public string? Profession { get; set; }

        public string? Position { get; set; }
    }
}
