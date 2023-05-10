using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using CoronaSystemApp.Models;
using Newtonsoft.Json;
using CoronaSystemApp.Services;

namespace CoronaSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        public readonly ClientService _clientService;
        public ClientController (ClientService clientService) { _clientService = clientService; }
        [HttpGet("GetClients")]
        public List<Client> GetClients()
        { 
            return _clientService.GetClients();
        }
        [HttpGet("GetClientDetailsById")]
        public Client GetClientDetails(string id)
        {
          return _clientService.GetClientDetails(id);
        }
        [HttpGet("GetClientIllnessDetailsById")]
        public Disease GetClientIllnessDetails(string id)
        {
            return _clientService.GetClientIllnessDetails(id);
        }
        [HttpGet("GetClientVaccinationDetailsById")]
        public List<Vaccination> GetClientVaccinationDetails(string id)
        {
            return _clientService.GetClientVaccinationDetails(id);
        }
        [HttpPost("InseartClient")]
        public string AddClient(Client client)
        {
            return _clientService.AddClient(client);
        }
        [HttpPut("UpdateImageToClient")]
        public string UpdateImageToClient(string id,string path)
        {
            return _clientService.UpdateImageToClient(id,path);
        }
    }
}
