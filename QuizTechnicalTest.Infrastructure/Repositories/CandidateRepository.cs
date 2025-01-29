using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using QuizTechnicalTest.CrossCutting.Settings;
using QuizTechnicalTest.Domain.Entities;
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

                var candidate = new Candidate();
                candidate.Id = (int)dataRow["candidate_id"];
                candidate.Name = (string)dataRow["candidate_name"];
                candidate.Group = (string)dataRow["candidate_group"];
                candidate.Age = (int)dataRow["candidate_age"];
                candidate.Profession = (string)dataRow["candidate_profession"];
                candidate.Position = (string)dataRow["candidate_position"];

                results.Add(candidate);
            }

            return results;
        }
    }
}
