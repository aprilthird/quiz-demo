using QuizTechnicalTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTechnicalTest.Infrastructure.Adapters
{
    public class QuestionAnswerAdater
    {
        public static QuestionAnswer ToEntity(DataRow dataRow)
        {
            return new()
            {
                QuestionId = (int)dataRow["question_id"],
                QuestionDescription = (string)dataRow["question_desc"],
                QuestionPicture = (string)dataRow["question_picture"],
                AnswerId = (int)dataRow["answer_id"],
                AnswerDescription = (string)dataRow["answer_desc"]
            };
        }
    }
}
