

namespace CoronaSystemApp
{
    public static class Validation
    {
        public static bool ValidateIsraeliID(string idNumber)
        {
            // Remove any non-digit characters from the ID number
            idNumber = new string(idNumber.Where(char.IsDigit).ToArray());

            // Check the length of the ID number
            if (idNumber.Length != 9)
            {
                return false;
            }

            int sum = 0;
            int digit;
            int temp;

            // Iterate over each digit of the ID number
            for (int i = 0; i < 9; i++)
            {
                digit = int.Parse(idNumber[i].ToString());
                temp = digit * ((i % 2) + 1);
                sum += (temp > 9) ? temp - 9 : temp;
            }

            // The ID number is valid if the sum is divisible by 10
            return sum % 10 == 0;
        }
        public static bool ValidateIsraeliPhoneNumber(string phoneNumber)
        {
            // Remove any non-digit characters from the phone number
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // Check the length of the phone number
            if (phoneNumber.Length != 10)
            {
                return false;
            }

            // Check the first digit of the phone number
            if (phoneNumber[0] != '0')
            {
                return false;
            }

            // Check the second digit of the phone number
            if (phoneNumber[1] != '5')
            {
                return false;
            }
                       
            // The phone number is valid
            return true;
        }
        public static bool ValidateIsraeliLandlineNumber(string phoneNumber)
        {
            // Remove any non-digit characters from the phone number
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // Check the length of the phone number
            if (phoneNumber.Length >10)
            {
                return false;
            }

            // Check the first digit of the phone number
            if (phoneNumber[0] != '0')
            {
                return false;
            }

            // Check the second digit of the phone number
            if (phoneNumber[1] != '2' && phoneNumber[1] != '3' && phoneNumber[1] != '4' && phoneNumber[1] != '7' && phoneNumber[1] != '8' && phoneNumber[1] != '9')
            {
                return false;
            }

            // The phone number is valid
            return true;
        }
        public static bool ValidateName(string name)
        {

            // Check if the first name contains only letters
            if (!name.All(char.IsLetter))
            {
                return false;
            }

            // The name is considered valid
            return true;
        }
        public static bool CheckManufacturer(string manufacturer)
        {
            if (manufacturer != "fizer" && manufacturer != "moderna")
            {
                return false;
            }
            return true;
        }
    }
}
