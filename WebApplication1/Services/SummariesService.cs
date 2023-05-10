using CoronaSystemApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Linq;


namespace CoronaSystemApp.Services
{
    public class SummariesService
    {
        public readonly IConfiguration _configuration;
        public SummariesService(IConfiguration configuration) { _configuration = configuration; }
        public int GetUnvaccinatedMembersCount()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-ID9PV4H\\MSSQLSERVER01;Initial Catalog=corona;Integrated Security=true"))
            {
                connection.Open();

                string query = @"
                SELECT COUNT(*) AS UnvaccinatedCopaMembersCount
                FROM client
                WHERE Id NOT IN (SELECT ClientId FROM vaccination)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    return count;
                }
            }
        }
        public List<DateAndNumberOfActiveSicks> GetDateAndNumberOfActiveSicks()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbcon").ToString());
            string query = @"
                WITH Dates AS (
                SELECT DATEADD(DAY, -1, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0)) AS CurrentDate
                UNION ALL
                SELECT DATEADD(DAY, 1, CurrentDate)
                FROM Dates
                WHERE CurrentDate < DATEADD(DAY, -1, DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0))
                )
                SELECT Dates.CurrentDate AS Date, COUNT(disease.Code) AS ActivePatientsCount
                FROM Dates
                LEFT JOIN disease ON Dates.CurrentDate >= disease.StartDate AND (Dates.CurrentDate <= disease.EndDate OR disease.EndDate IS NULL)
                GROUP BY Dates.CurrentDate
                ORDER BY Dates.CurrentDate
                OPTION (MAXRECURSION 0)";
            List<DateAndNumberOfActiveSicks> lst=new List<DateAndNumberOfActiveSicks>();
            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DateAndNumberOfActiveSicks d = new DateAndNumberOfActiveSicks();
                    d.Count = Convert.ToInt32(dt.Rows[j]["ActivePatientsCount"]);
                    d.DateInMonth = Convert.ToDateTime(dt.Rows[j]["Date"]);
                    lst.Add(d);
                }
            }
            return lst;
        }   
    }
}
