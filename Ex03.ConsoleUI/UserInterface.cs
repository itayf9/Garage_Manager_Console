namespace Ex03.ConsoleUI
{
    using System;
    using System.Collections.Generic;
    using Ex03.GarageLogic;
    using static Ex03.GarageLogic.VehicleFactory;

    public class UserInterface
    {
        private readonly GarageManager r_GarageManager;

        public UserInterface()
        {
            this.r_GarageManager = new GarageManager();
        }

        public void RunGarage()
        {
            eMenuOption userChoice = eMenuOption.NoChoice;

            do
            {
                printMenu();
                try
                {
                    userChoice = getChoice();
                    Console.Clear();
                    applyUserChoice(userChoice);
                }
                catch (FormatException formatException)
                {
                    displayFormatExceptionMessage();
                }
                catch (ArgumentException argumentException)
                {
                    displayArgumentExceptionMessage(argumentException);
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    displayValueOutOfRangeExceptionMessage(valueOutOfRangeException);
                }
                catch (Exception)
                {
                    displayGeneralExceptionMessage();
                }
            }
            while (userChoice != eMenuOption.Exit);
        }

        private void printMenu()
        {
            Console.WriteLine(@"
Welcome To My GARAGE!!
Please Choose (Enter the number of your option):
1. Insert a new vehicle.
2. Show a list of vehicles' license numbers.
3. Change vehicle's fixing state.
4. Inflate air in a vehicle's wheels to the maximum.
5. Fuel a fuel-based vehicle.
6. Charge an elecric-based vehicle.
7. Show a vehicle's information.
8. Exit");
        }

        private eMenuOption getChoice()
        {
            bool isValidInput;
            int userChoiceAsInteger;
            eMenuOption userChoice = eMenuOption.NoChoice;

            isValidInput = int.TryParse(Console.ReadLine(), out userChoiceAsInteger);
            if (isValidInput && userChoiceAsInteger > 0 && userChoiceAsInteger <= 8)
            {
                userChoice = (eMenuOption)userChoiceAsInteger;
            }
            else
            {
                isValidInput = false;
            }

            while (!isValidInput)
            {
                Console.WriteLine("Invalid input. Please enter your option between 1 - 8");
                isValidInput = int.TryParse(Console.ReadLine(), out userChoiceAsInteger);
                if (isValidInput && userChoiceAsInteger > 0 && userChoiceAsInteger <= 8)
                {
                    userChoice = (eMenuOption)userChoiceAsInteger;
                    continue;
                }
                else
                {
                    isValidInput = false;
                }
            }

            return userChoice;
        }

        private eAvailableVehicleTypes getVehicleTypeFromUser()
        {
            bool isValidInput = false;
            eAvailableVehicleTypes vehicleTypeFromUser = eAvailableVehicleTypes.FuelBasedCar;
            string inputFromUserAsString;
            int userChioce;

            do
            {
                try
                {
                    displayAvailableTypesOfVehicles();
                    inputFromUserAsString = Console.ReadLine();
                    userChioce = int.Parse(inputFromUserAsString);
                    if (userChioce > 0 && userChioce <= Enum.GetNames(typeof(eAvailableVehicleTypes)).Length)
                    {
                        vehicleTypeFromUser = (eAvailableVehicleTypes)Enum.Parse(typeof(eAvailableVehicleTypes), inputFromUserAsString);
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
                catch (FormatException formatException)
                {
                    displayFormatExceptionMessage();
                }
                catch (ArgumentException argumentException)
                {
                    displayArgumentExceptionMessage(argumentException);
                }
                catch (Exception)
                {
                    displayGeneralExceptionMessage();
                }
            }
            while (!isValidInput);

            return vehicleTypeFromUser;
        }

        private float getAmountFromUser(string i_AmountDescription)
        {
            bool isValidAmount = false;
            float amountFromUser = 0;

            Console.WriteLine("Please enter {0}: ", i_AmountDescription);

            while (!isValidAmount)
            {
                try
                {
                    amountFromUser = float.Parse(Console.ReadLine());
                    if (amountFromUser < 0)
                    {
                        isValidAmount = false;
                        Console.WriteLine("The amount can`t be a negative number. Please try again: ");
                    }
                    else
                    {
                        isValidAmount = true;
                    }
                }
                catch (FormatException formatException)
                {
                    displayGeneralExceptionMessage();
                }
            }

            return amountFromUser;
        }

        private string getSpecificNameFromUser(string i_DescriptionOfTheDesiredName)
        {
            bool isValidName = false;
            Console.WriteLine("Please enter the {0} name: ", i_DescriptionOfTheDesiredName);
            string nameInputFromUser = Console.ReadLine();
            while (!isValidName)
            {
                if (nameInputFromUser.Length == 0)
                {
                    Console.WriteLine("You inserted an empty name. Please try again.");
                    Console.WriteLine("Please enter the {0} name: ", i_DescriptionOfTheDesiredName);
                    nameInputFromUser = Console.ReadLine();
                    continue;
                }
                else
                {
                    isValidName = true;
                }
            }

            return nameInputFromUser;
        }

        private string getPhoneNumberFromUser()
        {
            bool isValidPhoneNumber = false;
            Console.WriteLine("Please enter your phone number (10 digits): ");
            string phoneNumberFromUser = Console.ReadLine();

            while (!isValidPhoneNumber)
            {
                if (phoneNumberFromUser.Length != 10)
                {
                    Console.WriteLine("Please enter exactly 10 digits. Please try again.");
                    Console.WriteLine("Please enter your phone number (10 digits): ");
                    phoneNumberFromUser = Console.ReadLine();
                    continue;
                }
                else if (!isCharacterSequenceContainsOnlyDigits(phoneNumberFromUser))
                {
                    Console.WriteLine("Invalid input. Please enter only digits (0-9): ");
                    phoneNumberFromUser = Console.ReadLine();
                    continue;
                }
                else
                {
                    isValidPhoneNumber = true;
                }
            }

            return phoneNumberFromUser;
        }

        private bool isCharacterSequenceContainsOnlyDigits(string i_phoneNumberFromUser)
        {
            const bool v_IsSequnceContainsOnlyDigits = true;

            foreach (char characterInPhoneNumber in i_phoneNumberFromUser)
            {
                if (!char.IsDigit(characterInPhoneNumber))
                {
                    return !v_IsSequnceContainsOnlyDigits;
                }
            }

            return v_IsSequnceContainsOnlyDigits;
        }

        private void getPersonalDetailsOfOwnerFromUser(out string o_OwnerName, out string o_OwnerPhoneNumber)
        {
            string ownerNameFromUser = getSpecificNameFromUser("vehicle's owner");
            string ownerPhoneNumberFromUser = getPhoneNumberFromUser();

            o_OwnerName = ownerNameFromUser;
            o_OwnerPhoneNumber = ownerPhoneNumberFromUser;
        }

        private eVehicleFixingState getNewVehicleStateFromUser()
        {
            bool isValidInput = false;
            eVehicleFixingState newState = eVehicleFixingState.InProgress;
            do
            {
                try
                {
                    displayTypesOfVehicleFixingState();
                    isValidInput = parseFixingStateFromInput(Console.ReadLine(), out newState);
                }
                catch (FormatException formatException)
                {
                    displayFormatExceptionMessage();
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    displayValueOutOfRangeExceptionMessage(valueOutOfRangeException);
                }
            }
            while (!isValidInput);

            return newState;
        }

        private bool parseFixingStateFromInput(string i_UserInput, out eVehicleFixingState o_DesiredState)
        {
            bool isValid;
            o_DesiredState = eVehicleFixingState.InProgress;

            if (int.TryParse(i_UserInput, out int vehicleState))
            {
                if (vehicleState == (int)eVehicleFixingState.InProgress || vehicleState == (int)eVehicleFixingState.Fixed || vehicleState == (int)eVehicleFixingState.Paid)
                {
                    o_DesiredState = (eVehicleFixingState)vehicleState;
                    isValid = true;
                }
                else
                {
                    throw new ArgumentException("Invalid input.");
                }
            }
            else
            {
                throw new FormatException();
            }

            return isValid;
        }

        private void applyUserChoice(eMenuOption i_UserChoice)
        {
            if (i_UserChoice == eMenuOption.DisplayListOfLicenseNumbers)
            {
                displayAllLicenseNumbersInTheGarage();
            }
            else if (i_UserChoice != eMenuOption.Exit)
            {
                string lisenceNumber = getLisenceNumberFromUser();
                bool isVehicleExist = checkIfVehicleExistsInGarageByLisenceNumber(lisenceNumber);

                if (i_UserChoice == eMenuOption.InsertNewVehicle && !isVehicleExist)
                {
                    eAvailableVehicleTypes userVehicle = getVehicleTypeFromUser();

                    bool isValidInputOfNewVehicleProperties;
                    do
                    {
                         isValidInputOfNewVehicleProperties = addNewVehicleToTheGarage(lisenceNumber, userVehicle);
                    }while (!isValidInputOfNewVehicleProperties);
                   
                }
                else if (isVehicleExist)
                {
                    switch (i_UserChoice)
                    {
                        case eMenuOption.InsertNewVehicle:
                            Console.WriteLine("This vehicle is already in the garage!");
                            r_GarageManager.ChangeVehicleState(lisenceNumber, eVehicleFixingState.InProgress);
                            Console.WriteLine("The state of this vehicle has changed to \"In Progress\".");
                            break;
                        case eMenuOption.ChangeVehicleState:
                            eVehicleFixingState newState = getNewVehicleStateFromUser();
                            r_GarageManager.ChangeVehicleState(lisenceNumber, newState);
                            Console.WriteLine("The vehicle's state has changed to {0}.", newState);
                            break;
                        case eMenuOption.InflateWheelAirToMax:
                            r_GarageManager.InflateWheelsToMaxAirPressure(lisenceNumber);
                            Console.WriteLine("All wheels are succesfully inflated to the maximum.");
                            break;
                        case eMenuOption.FuelVehicle:
                            bool isSecceededFueling = fuelVehicle(lisenceNumber);
                            if (isSecceededFueling == true)
                            {
                                Console.WriteLine("The vehicle has successfully refueled.");
                            }

                            break;
                        case eMenuOption.ChargingVehicle:
                            bool isSecceededCharging = chargeVehicle(lisenceNumber);
                            if (isSecceededCharging == true) 
                            {
                                Console.WriteLine("The vehicle has successfully charged.");
                            }
                            break;
                        case eMenuOption.DisplayFullDetailsOnVehicle:
                            displayDataOfVehicle(lisenceNumber);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("You've inserted a license number that does'nt exist in the garage.");
                }
            }
        }

        private bool addNewVehicleToTheGarage(string i_lisenceNumber, eAvailableVehicleTypes i_VehicleType)
        {
            Dictionary<string, Type> additionalSpecificPropertiesNameAndType;
            bool isValidTypeOfInput = false;
            bool isValidInputToAddVehicle = false;
            object userInput = null;
            Dictionary<string, object> additionalSpecificProperties = new Dictionary<string, object>();

            string modelName = getSpecificNameFromUser("vehicle's model");
            float amountOfEnergy = getAmountFromUser("vehicle's energy percentege (%)");
            string manufactureName = getSpecificNameFromUser("wheels' Manufacturer");
            float currentAirPressure = getAmountFromUser("current wheels' air pressure");
            getPersonalDetailsOfOwnerFromUser(out string ownerName, out string ownerPhoneNumber);

            additionalSpecificPropertiesNameAndType = VehicleFactory.GetAddidtionalSpecificPropertiesNameAndTypesForAVehicle(i_VehicleType);
            foreach (KeyValuePair<string, Type> propertyNameToPropertyTypePair in additionalSpecificPropertiesNameAndType)
            {
                Console.WriteLine($"Please enter {propertyNameToPropertyTypePair.Key}");
                if (propertyNameToPropertyTypePair.Value.IsEnum)
                {
                    System.Text.StringBuilder enumStringBuilder = new System.Text.StringBuilder();
                    enumStringBuilder.Append("(enter one of the following: ");
                    foreach (string nameOfEnum in propertyNameToPropertyTypePair.Value.GetEnumNames())
                    {
                        enumStringBuilder.Append("\"").Append(nameOfEnum).Append("\" ");
                    }

                    enumStringBuilder.Append(") ");
                    Console.Write(enumStringBuilder.ToString());
                }

                Console.WriteLine(": ");
                string userInputAsString = Console.ReadLine();
                while (!isValidTypeOfInput)
                {
                    try
                    {
                        if (propertyNameToPropertyTypePair.Value == typeof(bool))
                        {
                            if (userInputAsString != "yes" && userInputAsString != "no")
                            {
                                Console.WriteLine("You must enter Yes/No");
                                userInputAsString = Console.ReadLine();
                                continue;
                            }
                            else if (userInputAsString == "yes")
                            {
                                userInput = Convert.ChangeType(true, typeof(bool));
                            }
                            else if (userInputAsString == "no")
                            {
                                userInput = Convert.ChangeType(false, typeof(bool));
                            }
                        }
                        else if (propertyNameToPropertyTypePair.Value.IsEnum)
                        {
                            userInput = Enum.Parse(propertyNameToPropertyTypePair.Value, userInputAsString);
                        }
                        else
                        {
                            userInput = Convert.ChangeType(userInputAsString, propertyNameToPropertyTypePair.Value);
                        }

                        isValidTypeOfInput = true;
                        additionalSpecificProperties.Add(propertyNameToPropertyTypePair.Key, userInput);
                    }
                    catch (FormatException formatException)
                    {
                        displayFormatExceptionMessage();
                        userInputAsString = Console.ReadLine();
                        isValidTypeOfInput = false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid input! Please Try Again: ");
                        userInputAsString = Console.ReadLine();
                        isValidTypeOfInput = false;
                    }
                }

                isValidTypeOfInput = false;
            }

            try
            {
                r_GarageManager.AddNewVehicle(i_VehicleType, modelName, i_lisenceNumber, ownerName, ownerPhoneNumber, amountOfEnergy, manufactureName, currentAirPressure, additionalSpecificProperties);
                Console.WriteLine("The vehicle was created succefully.");
                isValidInputToAddVehicle = true;
            }
            catch (ArgumentException argumentException)
            {
                displayArgumentExceptionMessage(argumentException);
                isValidInputToAddVehicle = false;
            }

            return isValidInputToAddVehicle;
        }

        private bool checkIfVehicleExistsInGarageByLisenceNumber(string i_LisenceNumber)
        {
            return r_GarageManager.IsLisenceNumberExistsInGarage(i_LisenceNumber);
        }

        private string getLisenceNumberFromUser()
        {
            string lisenceNumberFromUser;

            Console.WriteLine("Please enter your lisence number:");
            lisenceNumberFromUser = Console.ReadLine();
            while (lisenceNumberFromUser.Length == 0)
            {
                Console.WriteLine("A lisence number must contain at least one number.");
                Console.WriteLine("Please enter your lisence number: ");
                lisenceNumberFromUser = Console.ReadLine();
            }

            return lisenceNumberFromUser;
        }

        private bool fuelVehicle(string i_LisenceNumber)
        {
            float amountOfFuelFromUser;
            eFuelType fuelTypeFromUser;
            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    Console.WriteLine("Enter how much liters of fuel would you like to refuel:");
                    amountOfFuelFromUser = float.Parse(Console.ReadLine());

                    displayFuelTypes();
                    fuelTypeFromUser = (eFuelType)int.Parse(Console.ReadLine());

                    r_GarageManager.FuelVehicle(i_LisenceNumber, amountOfFuelFromUser, fuelTypeFromUser);
                    isValidInput = true;
                }
                catch (FormatException formatException)
                {
                    isValidInput = false;
                    displayFormatExceptionMessage();
                }
                catch (ArgumentException argumentException)
                {
                    isValidInput = false;
                    displayArgumentExceptionMessage(argumentException);
                    break;
                }
                catch (ValueOutOfRangeException rangeException)
                {
                    isValidInput = false;
                    displayValueOutOfRangeExceptionMessage(rangeException);
                }
            }
            return isValidInput;
        }

        private bool chargeVehicle(string i_LisenceNumber)
        {
            float amountOfMinutesToCharge;
            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    Console.WriteLine("Enter the amount of minutes to charge: ");
                    amountOfMinutesToCharge = float.Parse(Console.ReadLine());
                    r_GarageManager.ChargeVehicle(i_LisenceNumber, amountOfMinutesToCharge);
                    isValidInput = true;
                }
                catch (FormatException formatException)
                {
                    isValidInput = false;
                    displayFormatExceptionMessage();
                }
                catch (ArgumentException argumentException)
                {
                    isValidInput = false;
                    displayArgumentExceptionMessage(argumentException);
                    break;
                }
                catch (ValueOutOfRangeException rangeException)
                {
                    isValidInput = false;
                    displayValueOutOfRangeExceptionMessage(rangeException);
                }
            }
            return isValidInput;
        }

        private void displayTypesOfVehicleFixingState()
        {
            string fixingStateName;

            Console.WriteLine("Please select you desired fixing state to the vehicle: ");
            for (int i = 0; i < Enum.GetNames(typeof(eVehicleFixingState)).Length; i++)
            {
                fixingStateName = ((eVehicleFixingState)i).ToString();
                Console.WriteLine("To select {0} press {1}.", fixingStateName, i);
            }
        }

        private void displayFuelTypes()
        {
            string fuelName;
            Console.WriteLine("Select the fuel type to fuel with: ");
            for (int i = 0; i < Enum.GetNames(typeof(eFuelType)).Length - 1; i++)
            {
                fuelName = ((eFuelType)(i + 1)).ToString();
                Console.WriteLine("To select {0} enter {1}.", fuelName, i + 1);
            }
        }

        private void displayAvailableTypesOfVehicles()
        {
            string vehicleTypeAsString;

            Console.WriteLine("Select you desired type of vehicle: ");
            for (int i = 1; i <= Enum.GetNames(typeof(eAvailableVehicleTypes)).Length; i++)
            {
                vehicleTypeAsString = ((eAvailableVehicleTypes)i).ToString();
                Console.WriteLine("To select {0} press {1}.", vehicleTypeAsString, i);
            }
        }

        private void displayDataOfVehicle(string i_LisenceNumber)
        {
            Console.WriteLine(r_GarageManager.GetDataOfVehicle(i_LisenceNumber));
        }

        private void displayAllLicenseNumbersInTheGarage()
        {
            string userChoice = string.Empty;
            bool isValidInput = false;

            while (!isValidInput)
            {
                try
                {
                    Console.WriteLine("Would you like to filter by the fixing state of the vehicle? (Y / N)");
                    userChoice = Console.ReadLine().ToUpper();
                    if (userChoice.Length == 1 && (userChoice == "N" || userChoice == "Y"))
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input. Please enter only \"Y\" or \"N\". Please try again.");
                    }
                }
                catch (Exception)
                {
                    displayGeneralExceptionMessage();
                }
            }

            List<string> vehicleLicenses = new List<string>();
            if (userChoice == "Y")
            {
                eVehicleFixingState userDesiredState = getNewVehicleStateFromUser();
                r_GarageManager.GetAllLicenseNumbersByState(ref vehicleLicenses, userDesiredState);
            }
            else
            {
                r_GarageManager.GetAllLicenseNumbers(ref vehicleLicenses);
            }

            if (vehicleLicenses.Count == 0)
            {
                Console.WriteLine("There are no vehicles in the garage that matches your request.");
            }
            else
            {
                foreach (string licenseNumber in vehicleLicenses)
                {
                    Console.WriteLine(licenseNumber);
                }
            }
        }

        private void displayFormatExceptionMessage()
        {
            Console.WriteLine("Your input is not in the right format. Please try again.");
        }

        private void displayValueOutOfRangeExceptionMessage(ValueOutOfRangeException i_ValueOutOfRangeException)
        {
            Console.WriteLine("Your input is out of range. The allowed range is from {0} to {1}. Please try again.", i_ValueOutOfRangeException.MinValue, i_ValueOutOfRangeException.MaxValue);
        }

        private void displayArgumentExceptionMessage(ArgumentException i_ArgumentException)
        {
            Console.WriteLine("{0}. Please try again.", i_ArgumentException.Message);
        }

        private void displayGeneralExceptionMessage()
        {
            Console.WriteLine("Something went wrong. Please try again.");
        }
    }
}