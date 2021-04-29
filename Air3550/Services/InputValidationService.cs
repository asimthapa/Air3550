using Air3550.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Air3550.Services
{
    public class InputValidationService
    {
        /// <summary>
        /// Validate inputs from input form
        /// Address1 is not verified
        /// </summary>
        /// <param name="inputDict"> Dictionary where key=inputKey, value=inputValue</param>
        /// <returns>True if valid input, False otherwise</returns>
        public bool ValidateInputs(Dictionary<string, object> inputDict)
        {
            foreach(KeyValuePair<string, object> input in inputDict)
            {
                var i = input.Value;
                if(i is string && !"address2".Equals(input.Key) && String.IsNullOrWhiteSpace(i.ToString().ToString()))
                {
                    return false;
                }

                switch (input.Key)
                {
                    case "firstName":
                    case "lastName":
                    case "city":
                        if (!i.ToString().All(c => char.IsLetter(c))) { return false; }
                        break;

                    case "password":
                        if (i.ToString().Length < 7 || i.ToString().Contains(" ")) { return false; }
                        break;

                    case "email":
                        if (i.ToString().Contains(" ") || !i.ToString().Contains("@")) { return false; }
                        break;

                    case "phoneNumber":
                        if (i.ToString().Length != 10 || !i.ToString().All(c => char.IsDigit(c))) { return false; }
                        break;

                    case "zipCode":
                        if (i.ToString().Length != 5 || !i.ToString().All(c => char.IsDigit(c))) { return false; }
                        break;

                    case "creditCardNumber":
                        if (!i.ToString().All(c => char.IsDigit(c))) { return false; }
                        break;

                    case "state":
                        bool isValidState = false;

                        foreach(KeyValuePair<string, string> stateMap in AppConstants.STATE_MAP)
                        {
                            if (stateMap.Key == i.ToString() || stateMap.Value == i.ToString()) 
                            {
                                isValidState = true;
                                break; 
                            }
                        }

                        if (!isValidState) { return isValidState; }
                        break;

                    case "birthDate":
                        if (i == null) { return false; }
                        break;

                    case "id":
                        if (i.ToString().Length != 6 || !i.ToString().All(c => char.IsDigit(c))) { return false; }
                        break;

                    default:
                        break;
                }
            }
            return true;
        }
    }
}
