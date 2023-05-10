using CoronaSystemApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoronaSystemApp.Models;
namespace CoronaSystemApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummariesController : ControllerBase
    {
        public readonly SummariesService _summariesService;
        public SummariesController(SummariesService summariesService) { _summariesService = summariesService; }
        [HttpGet("UnvaccinatedMembers")]
        public int GetUnvaccinatedMembersCount()
        {
            return _summariesService.GetUnvaccinatedMembersCount();
        }
        [HttpGet("TheNumberOfPatientsOnEachDateInTheLastMonth")]
        public List<DateAndNumberOfActiveSicks> GetDateAndNumberOfActiveSicks()
        {
            return _summariesService.GetDateAndNumberOfActiveSicks();
        }
    }
}
