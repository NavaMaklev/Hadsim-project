using System.IO;
namespace CoronaSystemApp.Models
{
    public class Client
    {
        private string id;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                if (!Validation.ValidateIsraeliID(value))
                    throw new ArgumentException("Invalid id");
                id = value;
            }
        }
        private string firstName;
        public string FirstName 
        {
            get 
            {
                return firstName;
            }
            set
            { 
               if(!Validation.ValidateName(value))
                    throw new ArgumentException("The clientws name contains non-letter characters");
               firstName = value;
            } 
        }
        private string lastName;    
        public string LastName
        {
            get 
            { 
                return lastName;
            } 
            set
            {
                if (!Validation.ValidateName(value))
                    throw new ArgumentException("The client's family name contains non-letter characters");
                lastName = value;
            }
        }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? HouseNumber { get; set; }
        private DateTime dateOfBirth;
        public DateTime DateOfBirth 
        { 
            get
            { 
                return dateOfBirth; 
            } 
            set 
            {
                dateOfBirth = value; 
            }
        }    
        private string? telephone;
        public string? Telephone
        { 
            get 
            { 
                return telephone; 
            }
            set 
            {
                if (!Validation.ValidateIsraeliLandlineNumber(value))
                    throw new ArgumentException("Invalid phone number");
                telephone = value;
            } 
        }
        public string? mobilePhone;
        public string? MobilePhone
        { 
           get 
            { 
                return mobilePhone; 
            }
           set
            {
                if (!Validation.ValidateIsraeliPhoneNumber(value))
                    throw new ArgumentException("Invalid mobile phone number");
                mobilePhone = value;
            }
        }
        public string? ClientImage { get; set; }
    }
}
