using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;
using Microsoft.Extensions.Options;
using QuizTechnicalTest.CrossCutting.Settings;
using QuizTechnicalTest.Domain.Entities;
using QuizTechnicalTest.Infrastructure.Adapters;
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
            var parameters = new List<SqlParameter>
            {
                new ("@quiz_code", SqlDbType.VarChar) { Value = quiz.Code },
                new ("@quiz_date", SqlDbType.DateTime2) { Value = quiz.Date },
            };

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

        public async Task<Quiz?> GetById(int id)
        {
            var sql = $"SELECT * FROM {TABLE_NAME} WHERE quiz_id = @quiz_id";
            var parameters = new List<SqlParameter>
            {
                new ("@quiz_id", SqlDbType.Int) { Value = id },
            };

            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            foreach (var param in parameters)
            {
                cmd.Parameters.Add(param);
            }

            var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            foreach (var row in dt.Rows)
            {
                var dataRow = (DataRow)row;
                var quiz = QuizAdapter.ToEntity(dataRow);
                return quiz;
            }

            return null;
        }

        public async Task<Quiz?> GetByDate(DateTime dateTime)
        {
            var sql = $"SELECT * FROM {TABLE_NAME} WHERE quiz_date = @quiz_date";
            var parameters = new List<SqlParameter>
            {
                new ("@quiz_date", SqlDbType.DateTime2) { Value = dateTime },
            };

            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            foreach (var param in parameters)
            {
                cmd.Parameters.Add(param);
            }

            var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            foreach (var row in dt.Rows)
            {
                var dataRow = (DataRow)row;
                var quiz = QuizAdapter.ToEntity(dataRow);
                return quiz;
            }

            return null;
        }

        public async Task<Quiz?> GetByCode(string code)
        {
            var sql = $"SELECT * FROM {TABLE_NAME} WHERE quiz_code = @quiz_code";
            var parameters = new List<SqlParameter>
            {
                new ("@quiz_code", SqlDbType.VarChar) { Value = code },
            };

            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            foreach (var param in parameters)
            {
                cmd.Parameters.Add(param);
            }

            var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            foreach (var row in dt.Rows)
            {
                var dataRow = (DataRow)row;
                var quiz = QuizAdapter.ToEntity(dataRow);
                return quiz;
            }

            return null;
        }

        public async Task<int> CalculateMatchingCandidates(int quizId)
        {
            using var con = new SqlConnection(_connectionString);
            await con.OpenAsync();
            using var cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "upCalculateMatchingCandidates";
            cmd.Parameters.Add(new("@pquiz_id", SqlDbType.Int) { Value = quizId });

            var result = await cmd.ExecuteNonQueryAsync();
            await con.CloseAsync();
            return result;
        }
    }
}
