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
                    getChoice(ref userChoice);
                    applyUserChoice(userChoice);
                }
                catch (FormatException formatException)
                {
                    displayFormatExceptionMessage(formatException);
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
            Console.Clear();
            Console.WriteLine(@"Welcome To My GARAGE!!
Please Choose:
1. Insert A new vehicle
2. Show the garage's vehicle license numbers list.
3. Change vehicle state.
4. Inflate air in the vehicle wheels.
5. Fuel a regular vehicle.
6. Charge an elecric vehicle.
7. Show vehicle's full details.
8. Exit");
        }

        private void getChoice(ref eMenuOption io_UserChoice)
        {
            bool validInput;
            int userChoice;
            validInput = int.TryParse(Console.ReadLine(), out userChoice);
            if (validInput && userChoice > 0 && userChoice <= 8)
            {
                io_UserChoice = (eMenuOption)userChoice;
            }
            else
            {
                validInput = false;
            }

            while (!validInput)
            {
                Console.WriteLine("Invalid input, please enter input between 1 - 8");
                validInput = int.TryParse(Console.ReadLine(), out userChoice);
                if (validInput && userChoice > 0 && userChoice <= 8)
                {
                    io_UserChoice = (eMenuOption)userChoice;
                    continue;
                }
                else
                {
                    validInput = false;
                }
            }
        }

        private VehicleFactory.eAvailableVehicleTypes getVehicleTypeFromUser()
        {
            bool validInput = false;
            VehicleFactory.eAvailableVehicleTypes userVehicle = VehicleFactory.eAvailableVehicleTypes.FuelBasedCar;
            string inputFromUser;
            int userChioce;

            do
            {
                try
                {
                    displayAvailableTypesOfVehicles();
                    inputFromUser = Console.ReadLine();
                    userChioce = int.Parse(inputFromUser);
                    if (userChioce > 0 && userChioce <= Enum.GetNames(typeof(VehicleFactory.eAvailableVehicleTypes)).Length)
                    {
                        userVehicle = (VehicleFactory.eAvailableVehicleTypes)Enum.Parse(typeof(VehicleFactory.eAvailableVehicleTypes), inputFromUser);
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("invalid input. Try again.");
                    }
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
            while (!validInput);

            return userVehicle;
        }

        private float getAmountFromUser(string i_DescriptionAmount)
        {
            Console.WriteLine("Please enter the amount of {0}: ", i_DescriptionAmount);
            bool validAmount = false;
            float amountOf = 0;
            while (!validAmount)
            {
                try
                {
                    amountOf = float.Parse(Console.ReadLine());
                    if (amountOf < 0)
                    {
                        validAmount = false;
                        Console.WriteLine("The amount can`t be negative. Please try again: ");
                    }
                    else
                    {
                        validAmount = true;
                    }
                }
                catch (Exception)
                {
                    displayGeneralExceptionMessage();
                }
            }

            return amountOf;
        }

        private void getSpecificNameFromUser(out string o_Name, string i_NameDescription)
        {
            bool validName = false, isNameInputWithoutSigns = true;
            Console.WriteLine("Please enter the {0} name: ", i_NameDescription);
            o_Name = Console.ReadLine();
            while (!validName)
            {
                if (o_Name.Length == 0)
                {
                    Console.WriteLine("You inserted an empty name. Please try again: ");
                    o_Name = Console.ReadLine();
                    continue;
                }
                else
                {
                    foreach (char letter in o_Name)
                    {
                        if (!char.IsLetter(letter) && letter != ' ' && !char.IsDigit(letter))
                        {
                            isNameInputWithoutSigns = false;
                            Console.WriteLine("Wrong Input. Please enter only letters: ");
                            o_Name = Console.ReadLine();
                            break;
                        }
                    }
                }

                if (isNameInputWithoutSigns)
                {
                    validName = true;
                }
                else
                {
                    isNameInputWithoutSigns = true;
                }
            }
        }

        private void getPhoneNumberFromUser(out string o_PhoneNumber)
        {
            bool validPhoneNumber = false, isDigitsOnly = true;
            Console.WriteLine("Please enter your phone number (10 digits): ");
            o_PhoneNumber = Console.ReadLine();
            while (!validPhoneNumber)
            {
                if (o_PhoneNumber.Length != 10)
                {
                    Console.WriteLine("you must type exactly 10 digits. Try again: ");
                    o_PhoneNumber = Console.ReadLine();
                    continue;
                }
                else
                {
                    foreach (char digit in o_PhoneNumber)
                    {
                        if (!char.IsDigit(digit))
                        {
                            isDigitsOnly = false;
                            Console.WriteLine("Invalid Input. Please enter digits only: ");
                            o_PhoneNumber = Console.ReadLine();
                            break;
                        }
                    }
                }

                if (isDigitsOnly)
                {
                    validPhoneNumber = true;
                }
                else
                {
                    isDigitsOnly = true;
                }
            }
        }

        private void getPersonalDetailsFromUser(out string o_OwnerName, out string o_OwnerPhoneNumber)
        {
            getSpecificNameFromUser(out string ownerNameFromUser, "Vehicle Owner");
            getPhoneNumberFromUser(out string ownerPhoneNumberFromUser);
            o_OwnerName = ownerNameFromUser;
            o_OwnerPhoneNumber = ownerPhoneNumberFromUser;
        }

        private eVehicleFixingState chooseVehicleState()
        {
            bool validInput = false;
            eVehicleFixingState newState = eVehicleFixingState.InProgress;
            do
            {
                try
                {
                    displayTypesOfVehicleFixingState();
                    validInput = parseFixingStateFromInput(Console.ReadLine(), out newState);
                }
                catch (FormatException formatException)
                {
                    displayFormatExceptionMessage(formatException);
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    displayValueOutOfRangeExceptionMessage(valueOutOfRangeException);
                }
            }
            while (!validInput);

            return newState;
        }

        private bool parseFixingStateFromInput(string i_UserInput, out eVehicleFixingState o_DesiredState)
        {
            bool isValid = false;
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
                    throw new ValueOutOfRangeException(Enum.GetNames(typeof(eVehicleFixingState)).Length, 1);
                }
            }
            else
            {
                throw new FormatException("You didn't entered the right digit!");
            }

            return isValid;
        }

        private void applyUserChoice(eMenuOption i_UserChoice)
        {
            if (i_UserChoice == eMenuOption.DispayListOfLicenseNumbers)
            {
                displayLicenseNumbersInGarage();
            }
            else if (i_UserChoice != eMenuOption.Exit)
            {
                getLisencePlateNumber(out string lisencePlateNumber);
                bool isVehicleExist = checkIfVehicleExistsInGarageByLisenceNumber(lisencePlateNumber);

                if (i_UserChoice == eMenuOption.InsertNewVehicle && !isVehicleExist)
                {
                    VehicleFactory.eAvailableVehicleTypes userVehicle = getVehicleTypeFromUser();
                    addVehicle(lisencePlateNumber, userVehicle);
                }
                else if (isVehicleExist)
                {
                    switch (i_UserChoice)
                    {
                        case eMenuOption.InsertNewVehicle:
                            Console.WriteLine("Vehicle is already in the garage!");
                            Console.WriteLine("The state of this vehicle has changed to in-repair.");
                            System.Threading.Thread.Sleep(1500);
                            r_GarageManager.ChangeVehicleState(lisencePlateNumber, eVehicleFixingState.InProgress);
                            break;
                        case eMenuOption.ChangeVehicleState:
                            eVehicleFixingState newState = chooseVehicleState();
                            r_GarageManager.ChangeVehicleState(lisencePlateNumber, newState);
                            Console.WriteLine("The vehicle state has changed.");
                            System.Threading.Thread.Sleep(1000);
                            break;
                        case eMenuOption.InflateWheelAirToMax:
                            r_GarageManager.InflateWheelsToMaxAirPressure(lisencePlateNumber);
                            Console.WriteLine("All the wheels are inflated to max!");
                            System.Threading.Thread.Sleep(1000);
                            break;
                        case eMenuOption.FuelVehicle:
                            fuelVehicle(lisencePlateNumber);
                            Console.WriteLine("The vehicle is successfully refueled!");
                            System.Threading.Thread.Sleep(1000);
                            break;
                        case eMenuOption.ChargingVehicle:
                            chargeVehicle(lisencePlateNumber);
                            Console.WriteLine("The vehicle is successfully charged!");
                            System.Threading.Thread.Sleep(1000);
                            break;
                        case eMenuOption.DisplayFullDetailsOnVehicle:
                            displayDataOfVehicle(lisencePlateNumber);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("You inserted a license plate that does not exist in the system.");
                    System.Threading.Thread.Sleep(1500);
                }
            }
        }

        private void addVehicle(string i_lisencePlateNumber, VehicleFactory.eAvailableVehicleTypes i_VehicleType)
        {
            Dictionary<string, Type> additionalSpecificPropertiesNameAndType;
            bool isValidInput = false;
            object userInput = null;
            Dictionary<string, object> additionalSpecificProperties = new Dictionary<string, object>();

            getSpecificNameFromUser(out string modelName, "Model Vehicle");
            float amountOfEnergy = getAmountFromUser("Vehicle Energy");
            getSpecificNameFromUser(out string manufactureName, "Wheel Manufacturer");
            float currentAirPressure = getAmountFromUser("wheels Air Pressure");
            getPersonalDetailsFromUser(out string ownerName, out string ownerPhoneNumber);

            additionalSpecificPropertiesNameAndType = VehicleFactory.GetAddidtionalSpecificPropertiesNameAndTypesForAVehicle(i_VehicleType);
            foreach (KeyValuePair<string, Type> propertyNameToPropertyTypePair in additionalSpecificPropertiesNameAndType)
            {
                Console.WriteLine($"Please Enter {propertyNameToPropertyTypePair.Key}: ");
                string userInputAsString = Console.ReadLine();
                while (!isValidInput)
                {
                    try
                    {
                        if (propertyNameToPropertyTypePair.Value == typeof(bool))
                        {
                            if (userInputAsString != "yes" && userInputAsString != "no")
                            {
                                Console.WriteLine("You must type yes/no");
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
                        else
                        {
                            userInput = Convert.ChangeType(userInputAsString, propertyNameToPropertyTypePair.Value);
                        }

                        isValidInput = true;
                        additionalSpecificProperties.Add(propertyNameToPropertyTypePair.Key, userInput);
                    }
                    catch (FormatException formatException)
                    {
                        displayFormatExceptionMessage(formatException);
                        userInputAsString = Console.ReadLine();
                        isValidInput = false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid input! Please Try Again: ");
                        userInputAsString = Console.ReadLine();
                        isValidInput = false;
                    }
                }

                isValidInput = false;
            }

            r_GarageManager.AddNewVehicle(i_VehicleType, modelName, i_lisencePlateNumber, ownerName, ownerPhoneNumber, amountOfEnergy, manufactureName, currentAirPressure, additionalSpecificProperties);
            Console.WriteLine("vehicle is created succefully.");
            System.Threading.Thread.Sleep(2000);
        }

        private bool checkIfVehicleExistsInGarageByLisenceNumber(string i_LisencePlateNumber)
        {
            return r_GarageManager.IsLisenceNumberExistsInGarage(i_LisencePlateNumber);
        }

        private void getLisencePlateNumber(out string o_LisencePlateNumber)
        {
            Console.WriteLine("Please enter your lisence plate number: ");
            o_LisencePlateNumber = Console.ReadLine();
            while (o_LisencePlateNumber.Length == 0)
            {
                Console.WriteLine("lisence plate must contain at least 1 number.");
                Console.WriteLine("Please enter your lisence plate number: ");
                o_LisencePlateNumber = Console.ReadLine();
            }
        }

        private void fuelVehicle(string i_LisencePlateNumber)
        {
            float amountOfFuel;
            bool validInput = false;
            while (!validInput)
            {
                try
                {
                    Console.WriteLine("Enter how much liters of fuel you would like to refuel:");
                    amountOfFuel = float.Parse(Console.ReadLine());

                    displayTypesOfEnergySource();
                    validInput = eEnergySourceType.TryParse(Console.ReadLine(), out eEnergySourceType userFuelType);

                    r_GarageManager.LoadEnergySource(i_LisencePlateNumber, amountOfFuel, userFuelType);
                    validInput = true;
                }
                catch (FormatException formatException)
                {
                    validInput = false;
                    displayFormatExceptionMessage(formatException);
                }
                catch (ValueOutOfRangeException rangeException)
                {
                    validInput = false;
                    displayValueOutOfRangeExceptionMessage(rangeException);
                }
            }
        }

        private void chargeVehicle(string i_LisencePlateNumber)
        {
            float amountOfEnergy;
            bool validInput = false;
            while (!validInput)
            {
                try
                {
                    Console.WriteLine("Enter Amount of minutes to Charge: ");
                    amountOfEnergy = float.Parse(Console.ReadLine());
                    r_GarageManager.LoadEnergySource(i_LisencePlateNumber, amountOfEnergy, eEnergySourceType.Electric);
                    validInput = true;
                }
                catch (FormatException formatException)
                {
                    validInput = false;
                    displayFormatExceptionMessage(formatException);
                }
                catch (ValueOutOfRangeException rangeException)
                {
                    validInput = false;
                    displayValueOutOfRangeExceptionMessage(rangeException);
                }
            }
        }

        private void displayTypesOfVehicleFixingState()
        {
            string stateName;
            for (int i = 0; i < Enum.GetNames(typeof(eVehicleFixingState)).Length; i++)
            {
                stateName = ((eVehicleFixingState)i).ToString();
                Console.WriteLine("To select {0} press {1}.", stateName, i);
            }
        }

        private void displayTypesOfEnergySource()
        {
            string fuelName;
            for (int i = 2; i <= Enum.GetNames(typeof(eEnergySourceType)).Length; i++)
            {
                fuelName = ((eEnergySourceType)i).ToString();
                Console.WriteLine("To select {0} press {1}.", fuelName, i);
            }
        }

        private void displayAvailableTypesOfVehicles()
        {
            string vehicleType;
            for (int i = 0; i < Enum.GetNames(typeof(eAvailableVehicleTypes)).Length; i++)
            {
                vehicleType = ((eAvailableVehicleTypes)i).ToString();
                Console.WriteLine("To select {0} press {1}.", vehicleType, i + 1);
            }
        }

        private void displayDataOfVehicle(string i_LisencePlateNumber)
        {
            Console.WriteLine(r_GarageManager.GetDataOfVehicle(i_LisencePlateNumber));
            System.Threading.Thread.Sleep(10000);
        }

        private void displayLicenseNumbersInGarage()
        {
            string userChoice = string.Empty;
            bool validInput = false;
            while (!validInput)
            {
                try
                {
                    Console.WriteLine("Would you like to filter by the condition of the vehicle? (y/n)");
                    userChoice = Console.ReadLine().ToUpper();
                    if (userChoice.Length == 1 && (userChoice == "N" || userChoice == "Y"))
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input! Please try again.");
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
                eVehicleFixingState newState = chooseVehicleState();
                r_GarageManager.GetAllLicenseNumbersByState(ref vehicleLicenses, newState);
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

            System.Threading.Thread.Sleep(4000);
        }

        private void displayFormatExceptionMessage(FormatException i_FormatException)
        {
            System.Console.Clear();
            Console.WriteLine(i_FormatException.Message);
            System.Threading.Thread.Sleep(4000);
        }

        private void displayValueOutOfRangeExceptionMessage(ValueOutOfRangeException i_ValueOutOfRangeException)
        {
            System.Console.Clear();
            Console.WriteLine(i_ValueOutOfRangeException.Message);
            System.Threading.Thread.Sleep(4000);
        }

        private void displayArgumentExceptionMessage(ArgumentException i_ArgumentException)
        {
            System.Console.Clear();
            Console.WriteLine(i_ArgumentException.Message);
            System.Threading.Thread.Sleep(4000);
        }

        private void displayGeneralExceptionMessage()
        {
            Console.WriteLine("something went wrong, Try again.");
            System.Threading.Thread.Sleep(1500);
        }
    }
}