using QuizTechnicalTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTechnicalTest.Infrastructure.Adapters
{
    public class QuizAdapter
    {
        public static Quiz ToEntity(DataRow dataRow)
        {
            return new()
            {
                Id = (int)dataRow["quiz_id"],
                Code = (string)dataRow["quiz_code"],
                Date = (DateTime)dataRow["quiz_date"],
                Candidate1Id = dataRow["candidate1_id"] == DBNull.Value ? null : (int)dataRow["candidate1_id"],
                Candidate1Percent = dataRow["candidate1_percent"] == DBNull.Value ? null : (short)dataRow["candidate1_percent"],
                Candidate2Id = dataRow["candidate2_id"] == DBNull.Value ? null : (int)dataRow["candidate2_id"],
                Candidate2Percent = dataRow["candidate2_percent"] == DBNull.Value ? null : (short)dataRow["candidate2_percent"],
                Candidate3Id = dataRow["candidate3_id"] == DBNull.Value ? null : (int)dataRow["candidate3_id"],
                Candidate3Percent = dataRow["candidate3_percent"] == DBNull.Value ? null : (short)dataRow["candidate3_percent"],
                Candidate4Id = dataRow["candidate4_id"] == DBNull.Value ? null : (int)dataRow["candidate4_id"],
                Candidate4Percent = dataRow["candidate4_percent"] == DBNull.Value ? null : (short)dataRow["candidate4_percent"],
                Candidate5Id = dataRow["candidate5_id"] == DBNull.Value ? null : (int)dataRow["candidate5_id"],
                Candidate5Percent = dataRow["candidate5_percent"] == DBNull.Value ? null : (short)dataRow["candidate5_percent"],
            };
        }
    }
}
