using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using CoronaSystemApp.Models;
using System.Collections.Generic;
using System.IO;
using CoronaSystemApp.Services;

namespace CoronaSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationController : ControllerBase
    {
        public readonly VaccinationService _vaccinationService;
        public VaccinationController(VaccinationService vaccinationService) { _vaccinationService = vaccinationService; }
        [HttpGet("GetVaccinations")]
        public List<Vaccination> GetVaccinations()
        {
            return _vaccinationService.GetVaccinations();
        }      
        [HttpPost("InsertVaccination")]
        public string AddVaccination( Vaccination vaccination)
        {
            return _vaccinationService.AddVaccination(vaccination);
        }
    }
}
