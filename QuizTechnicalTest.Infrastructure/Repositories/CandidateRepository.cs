using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using QuizTechnicalTest.CrossCutting.Settings;
using QuizTechnicalTest.Domain.Entities;
using QuizTechnicalTest.Infrastructure.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTechnicalTest.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {

        private static readonly string TABLE_NAME = "candidate";
        private readonly string? _connectionString;

        public CandidateRepository(IOptions<AppSettings> options)
        {
            _connectionString = options.Value.ConnectionStrings?.DefaultConnection;
        }

        public async Task<ICollection<Candidate>> GetAll()
        {
            var sql = $"SELECT * FROM {TABLE_NAME}";
            var results = new List<Candidate>();

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
                var candidate = CandidateAdapter.ToEntity(dataRow);
                results.Add(candidate);
            }

            return results;
        }

        public async Task<Candidate?> GetById(int id)
        {
            var sql = $"SELECT * FROM {TABLE_NAME} WHERE candidate_id = @candidate_id";
            var parameters = new List<SqlParameter>
            {
                new ("@candidate_id", SqlDbType.Int) { Value = id },
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
                var candidate = CandidateAdapter.ToEntity(dataRow);
                return candidate;
            }

            return null;
        }
    }
}
