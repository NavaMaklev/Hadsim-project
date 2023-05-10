using CoronaSystemApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Linq;

namespace CoronaSystemApp.Services
{
    public class ClientService
    {
        public readonly IConfiguration _configuration;
        public ClientService(IConfiguration configuration) { _configuration = configuration; }
        public List<Client> GetClients()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbcon").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT*FROM client", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            string jsonData = JsonConvert.SerializeObject(dt);
            if (dt.Rows.Count > 0)
            {     
                return JsonConvert.DeserializeObject<List<Client>>(jsonData);
            }
            else
            {
                return JsonConvert.DeserializeObject<List<Client>>(jsonData);
            }
        }
        public Client GetClientDetails(string id)
        {
            if (!Validation.ValidateIsraeliID(id))
            {
                throw new ArgumentException("Invalid id");
            }
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbcon").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter
            ($"SELECT * FROM client WHERE client.Id='{id}';", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            string jsonData = JsonConvert.SerializeObject(dt);
            if (dt.Rows.Count > 0)
            {              
                return JsonConvert.DeserializeObject<List<Client>>(jsonData)[0];
            }
            else
            {
                return JsonConvert.DeserializeObject<List<Client>>(jsonData)[0];
            }
        }
        public Disease GetClientIllnessDetails(string id)
        {
            if (!Validation.ValidateIsraeliID(id))
            {
                throw new ArgumentException("Invalid id");
            }
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbcon").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter
            ($"SELECT disease.StartDate,disease.EndDate FROM disease WHERE disease.ClientId='{id}';", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            string jsonData = JsonConvert.SerializeObject(dt);
            if (dt.Rows.Count > 0)
            {  
                return JsonConvert.DeserializeObject<List<Disease>>(jsonData)[0];
            }
            else
            {
                return JsonConvert.DeserializeObject<List<Disease>>(jsonData)[0];
            }
        }
        public List<Vaccination> GetClientVaccinationDetails(string id)
        {
            if (!Validation.ValidateIsraeliID(id))
            {
                throw new ArgumentException("Invalid id");
            }
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbcon").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter
            ($"SELECT vaccination.Manufacturer,vaccination.VaccinationDate FROM vaccination WHERE vaccination.ClientId='{id}';", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            string jsonData = JsonConvert.SerializeObject(dt);
            if (dt.Rows.Count > 0)
            {              
                return JsonConvert.DeserializeObject<List<Vaccination>>(jsonData);
            }
            else
            {
               // If you got here will be returned []
                return JsonConvert.DeserializeObject<List<Vaccination>>(jsonData);                
            }
        }
        public string AddClient(Client client)
        {
            string dateOfBirth = client.DateOfBirth.ToString("yyyy-MM-dd");
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbcon").ToString());
            SqlCommand cmd = new SqlCommand($"INSERT INTO client(Id,FirstName,LastName,City,Street,HouseNumber,DateOfBirth,Telephone,MobilePhone)VALUES('{client.Id}','{client.FirstName}','{client.LastName}','{client.City}','{client.Street}','{client.HouseNumber}','{dateOfBirth}','{client.Telephone}','{client.MobilePhone}')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0) { return "Date inseart"; }
            else { return "Error"; }
        }
        public string UpdateImageToClient(string id, string path)
        {
            byte[] fileBytes = File.ReadAllBytes(path);
            string base64String = Convert.ToBase64String(fileBytes);
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("dbcon").ToString());
            string command = $"UPDATE client SET ClientImage = '{base64String}' WHERE Id = '{id}'";
            SqlCommand cmd = new SqlCommand(command, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0) { return "Date inseart"; }
            else { return "Error"; }
        }
    }
}
