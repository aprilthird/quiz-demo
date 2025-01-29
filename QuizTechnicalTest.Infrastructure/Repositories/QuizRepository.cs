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
    public class QuizRepository : IQuizRepository
    {
        private static readonly string TABLE_NAME = "quiz";
        private readonly string? _connectionString;

        public QuizRepository(IOptions<AppSettings> options)
        {
            _connectionString = options.Value.ConnectionStrings?.DefaultConnection;
        }

        public async Task<int> Create(Quiz quiz)
        {
            var sql = $"INSERT INTO {TABLE_NAME} (quiz_code, quiz_date) " +
                $"VALUES (@quiz_code, @quiz_date)";
            var parameters = new ArrayList(new SqlParameter[]
            {
                new ("@quiz_code", SqlDbType.VarChar) { Value = quiz.Code },
                new ("@quiz_date", SqlDbType.Date) { Value = quiz.Date },
            });

            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            if (parameters != null)
            {
                if (parameters.Count > 0)
                {
                    var sqlParams = parameters.GetEnumerator();
                    while (sqlParams.MoveNext())
                    {
                        cmd.Parameters.Add((SqlParameter)sqlParams.Current);
                    }
                }
            }
            var result = await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            return result;
        }
    }
}
