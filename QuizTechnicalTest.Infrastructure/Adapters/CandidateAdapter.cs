using QuizTechnicalTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTechnicalTest.Infrastructure.Adapters
{
    public class CandidateAdapter
    {
        public static Candidate ToEntity(DataRow dataRow)
        {
            return new()
            {
                Id = (int)dataRow["candidate_id"],
                Name = (string)dataRow["candidate_name"],
                Group = (string)dataRow["candidate_group"],
                Age = (int)dataRow["candidate_age"],
                Profession = dataRow["candidate_profession"] == DBNull.Value ? null : (string)dataRow["candidate_profession"],
                Position = dataRow["candidate_position"] == DBNull.Value ? null : (string)dataRow["candidate_position"],
                GovPlan = dataRow["candidate_gov_plan"] == DBNull.Value ? null : (string)dataRow["candidate_gov_plan"],
                Proposal = dataRow["candidate_proposal"] == DBNull.Value ? null : (string)dataRow["candidate_proposal"],
                Picture = dataRow["candidate_picture"] == DBNull.Value ? null : (string)dataRow["candidate_picture"],
            };
        }
    }
}
