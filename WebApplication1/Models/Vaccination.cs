using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using CoronaSystemApp.Controllers;
using CoronaSystemApp.Services;

namespace CoronaSystemApp.Models
{
    public class Vaccination
    {
        
        public int Code { get; set; }
        private string clientId;
        public string? ClientId 
        {
            get
            { 
                return clientId; 
            }
            set
            {
                if (!Validation.ValidateIsraeliID(value))
                    throw new ArgumentException("Invalid id");
                clientId = value;
            }
        }
        private string manufacturer;
        public string? Manufacturer 
        { 
            get
            { 
                return manufacturer;
            }
          set
            {
                if (!Validation.CheckManufacturer(value))
                    throw new ArgumentException("Unknown manufacturer, the options are fizer or moderna");
                manufacturer = value;
            }
        }
        public DateTime VaccinationDate { get; set; }    
    }
}
