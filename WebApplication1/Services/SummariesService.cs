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
            /* 
            Explanation of the query:
            1. Common Table Expression (CTE) Definition:
            - The query begins with a CTE definition named "Dates". CTEs are temporary result sets that can be referenced within the main query.
            - The "Dates" CTE generates a sequence of dates starting from the first day of the previous month and ending on the last day of the current month.
            - It uses recursive logic to generate a series of dates incrementally.
            2. Recursive Part of the CTE:
            - The initial part of the CTE specifies the starting date as the last day of the previous month using the `DATEADD` and `DATEDIFF` functions.
            - The recursive part selects the next date by adding one day to the previous date.
            - The recursive part is controlled by the condition in the WHERE clause, which ensures that the loop continues until the current date reaches the last day of the current month.
            3. Main Query:
            - The main query selects data from the "Dates" CTE and performs additional operations.
            - It selects the "CurrentDate" column from the "Dates" CTE and aliases it as "Date".
            - It counts the number of rows in the "disease" table where the "CurrentDate" falls within the range of the "StartDate" and "EndDate" columns.
            - The `LEFT JOIN` ensures that all dates from the "Dates" CTE are included in the result, even if there are no matching rows in the "disease" table.
            - The result is grouped by "CurrentDate" and the count of diseases is calculated for each date.
            - The result is ordered by "CurrentDate" in ascending order.
            4. `OPTION (MAXRECURSION 0)`:
            - This option is used to specify the maximum recursion level for the recursive CTE.
            - Setting it to 0 means there is no limit on the number of recursive iterations.
            In summary, this query generates a sequence of dates within a given range,
            starting from the first day of the previous month and ending on the last day of the current month. 
            It then counts the number of active patients for each date by checking if the date falls within the 
            range defined by the "StartDate" and "EndDate" columns in the "disease" table. The result is a list of
            dates and the corresponding count of active patients for each date, ordered by date.
            */
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
