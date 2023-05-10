namespace CoronaSystemApp.Models
{
    public class Disease
    {
        public int Code { get; set; }
        private string clientId;
        public string ClientId
        {   get
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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }       
    }
}
