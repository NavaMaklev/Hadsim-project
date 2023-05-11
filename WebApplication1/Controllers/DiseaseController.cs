using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using CoronaSystemApp.Models;
using CoronaSystemApp.Services;

namespace CoronaSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        public readonly DiseaseService _diseaseService;
        public DiseaseController(DiseaseService diseaseService) { _diseaseService = diseaseService; }
        [HttpGet("GetDiseases")]
        public List<Disease> GetDiseases()
        {
         return _diseaseService.GetDiseases();
        }
        [HttpPost("InsertDisease")]
        public string AddDisease(Disease disease)
        {
            return _diseaseService.AddDisease(disease);
        }
    }
}
