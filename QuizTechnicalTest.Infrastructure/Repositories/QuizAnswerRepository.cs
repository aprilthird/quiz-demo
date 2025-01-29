using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using QuizTechnicalTest.CrossCutting.Settings;
using QuizTechnicalTest.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTechnicalTest.Infrastructure.Repositories
{
    public class QuizAnswerRepository : IQuizAnswerRepository
    {
        private static readonly string TABLE_NAME = "quiz_answer";
        private readonly string? _connectionString;

        public QuizAnswerRepository(IOptions<AppSettings> options)
        {
            _connectionString = options.Value.ConnectionStrings?.DefaultConnection;
        }

        public async Task<int> CreateRange(List<QuizAnswer> quizAnswers)
        {
            var sql = $"INSERT INTO {TABLE_NAME} (quiz_id, question_id, answer_id) VALUES ";
            var parameters = new List<SqlParameter>();
            for (var i = 0; i < quizAnswers.Count(); ++i)
            {
                sql += $"{(i > 0 ? "," : string.Empty)} (@quiz_id_{i}, @question_id_{i}, @answer_id_{i}) ";
                parameters.Add(new($"@quiz_id_{i}", SqlDbType.Int) { Value = quizAnswers[i].QuizId });
                parameters.Add(new($"@question_id_{i}", SqlDbType.Int) { Value = quizAnswers[i].QuestionId });
                parameters.Add(new($"@answer_id_{i}", SqlDbType.Int) { Value = quizAnswers[i].AnswerId });
            }

            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            foreach (var param in parameters) 
            {
                cmd.Parameters.Add(param);
            }

            var result = await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            return result;
        }
    }
}
