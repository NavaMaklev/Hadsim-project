using CoronaSystemApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace CoronaSystemApp.Services
{
    public class DiseaseService
    {
        public readonly IConfiguration _configuration;
        public DiseaseService(IConfiguration configuration) { _configuration = configuration; }
        public List<Disease> GetDiseases()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbcon").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT*FROM disease", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            string jsonData = JsonConvert.SerializeObject(dt);
            if (dt.Rows.Count > 0)
            {
                return JsonConvert.DeserializeObject<List<Disease>>(jsonData);
            }
            else
            {
                return JsonConvert.DeserializeObject<List<Disease>>(jsonData);
            }
        }
        public string AddDisease(Disease disease)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbcon").ToString());
            //Checking the object
            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT*FROM disease WHERE disease.ClientId={disease.ClientId};", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);           
            //Has the client been ill in the past?
            if (dt.Rows.Count ==1)
            {
                throw new ArgumentException("This client has been sick in the past");
            }
            TimeSpan duration = disease.EndDate.Subtract(disease.StartDate);
            int numberOfDays = (int)duration.TotalDays;
            //Length of illness period is normal
            if (numberOfDays < 21)
            {
                throw new ArgumentException("The length of the illness period is a minimum of 3 weeks, check the dates.");
            }
            //End checking the object
            string startDate = disease.StartDate.ToString("yyyy-MM-dd");
            string endDate = disease.EndDate.ToString("yyyy-MM-dd");
            SqlCommand cmd = new SqlCommand($"INSERT INTO disease(ClientId,StartDate,EndDate)VALUES('{disease.ClientId}','{startDate}','{endDate}')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0) { return "Date inseart"; }
            else { return "Error"; }
        }
    }
}

