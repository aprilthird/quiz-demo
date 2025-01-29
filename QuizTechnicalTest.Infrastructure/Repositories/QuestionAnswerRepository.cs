using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using QuizTechnicalTest.CrossCutting.Settings;
using QuizTechnicalTest.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuizTechnicalTest.Infrastructure.Repositories
{
    public class QuestionAnswerRepository : IQuestionAnswerRepository
    {
        private static readonly string TABLE_NAME = "vQuestionAnswer";
        private readonly string? _connectionString;

        public QuestionAnswerRepository(IOptions<AppSettings> options)
        {
            _connectionString = options.Value.ConnectionStrings?.DefaultConnection;
        }

        public async Task<ICollection<QuestionAnswer>> GetAll()
        {
            var sql = $"SELECT * FROM {TABLE_NAME}";
            var results = new List<QuestionAnswer>();

            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            foreach (var row in dt.Rows)
            {
                var dataRow = (DataRow)row;
                
                var questionAnswer = new QuestionAnswer();
                questionAnswer.QuestionId = (int)dataRow["question_id"];
                questionAnswer.QuestionDescription = (string)dataRow["question_desc"];
                questionAnswer.QuestionPicture = (string)dataRow["question_picture"];
                questionAnswer.AnswerId = (int)dataRow["answer_id"];
                questionAnswer.AnswerDescription = (string)dataRow["answer_desc"];
                
                results.Add(questionAnswer);
            }

            return results;
        }
    }
}
