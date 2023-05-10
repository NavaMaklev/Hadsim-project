using CoronaSystemApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace CoronaSystemApp.Services
{
    public class VaccinationService
    {
        public readonly IConfiguration _configuration;
        public VaccinationService(IConfiguration configuration) { _configuration = configuration; }
        public List<Vaccination> GetVaccinations()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbcon").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT*FROM vaccination", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            string jsonData = JsonConvert.SerializeObject(dt);
            if (dt.Rows.Count > 0)
            {  
                return JsonConvert.DeserializeObject<List<Vaccination>>(jsonData);
            }
            else
            {
                return JsonConvert.DeserializeObject<List<Vaccination>>(jsonData);
            }
        }
        public string AddVaccination(Vaccination vaccination)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbcon").ToString());
            //Checking the object
            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT*FROM vaccination WHERE vaccination.ClientId='{vaccination.ClientId}';", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count == 4)
            {
                throw new ArgumentException("This client has been vaccinated 4 times.");
            }
            else if (dt.Rows.Count > 0)
            {
                List<Vaccination> vactList = new List<Vaccination>();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DateTime date= Convert.ToDateTime(dt.Rows[j]["VaccinationDate"]);
                    if (date.Date.Equals(vaccination.VaccinationDate.Date))
                        throw new ArgumentException("Do not vaccinate twice on the same day");     
                }
            }
            string vaccinationDate = vaccination.VaccinationDate.ToString("yyyy-MM-dd");
            SqlCommand cmd = new SqlCommand($"INSERT INTO vaccination(ClientId,Manufacturer,VaccinationDate)VALUES('{vaccination.ClientId}','{vaccination.Manufacturer}','{vaccinationDate}')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0) { return "Date inseart"; }
            else { return "Error"; }
        }
    }
}
