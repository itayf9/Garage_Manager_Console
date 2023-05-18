namespace Ex03.ConsoleUI
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Ex03.GarageLogic;

    public class UserInterface
    {
        GarageManager m_GarageManager;

        public UserInterface()
        {
            this.m_GarageManager = new GarageManager();
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

        private static void printMenu()
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

        private static void getChoice(ref eMenuOption io_UserChoice)
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

        private void applyUserChoice(eMenuOption i_UserChoice)
        {
            if (i_UserChoice == eMenuOption.DispayListOfLicenseNumbers)
            {
                displayAllLicenseNumbers();
            }
            else if (i_UserChoice != eMenuOption.Exit)
            {
                getLisencePlateNumber(out string lisencePlateNumber);
                bool isVehicleExist = checkIfVehicleExist(lisencePlateNumber);

                if (i_UserChoice == eMenuOption.InsertNewVehicle && !isVehicleExist)
                {
                    VehicleFactory.eVehicleType userVehicle = chooseVehicleType();
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
                            m_GarageManager.ChangeVehicleState(lisencePlateNumber, VehicleState.eVehicleState.InRepair);
                            break;
                        case eMenuOption.ChangeVehicleState:
                            VehicleState.eVehicleState newState = chooseVehicleState();
                            m_GarageManager.ChangeVehicleState(lisencePlateNumber, newState);
                            Console.WriteLine("The vehicle state has changed.");
                            System.Threading.Thread.Sleep(1000);
                            break;
                        case eMenuOption.InflateWheelAirToMax:
                            m_GarageManager.InflateWheels(lisencePlateNumber);
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

        private static VehicleFactory.eVehicleType chooseVehicleType()
        {
            bool validInput = false;
            VehicleFactory.eVehicleType userVehicle = VehicleFactory.eVehicleType.RegularCar;
            string inputFromUser;
            int userChioce;

            do
            {
                try
                {
                    printAllTypeOfVehicles();
                    inputFromUser = Console.ReadLine();
                    userChioce = int.Parse(inputFromUser);
                    if (userChioce > 0 && userChioce <= Enum.GetNames(typeof(VehicleFactory.eVehicleType)).Length)
                    {
                        userVehicle = (VehicleFactory.eVehicleType)Enum.Parse(typeof(VehicleFactory.eVehicleType), inputFromUser);
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

        private void addVehicle(string i_lisencePlateNumber, VehicleFactory.eVehicleType i_UserVehicle)
        {
            Dictionary<string, Type> InfoNeededForUser;
            bool validInput = false;
            object userInput = null;
            List<object> InfoForUser = new List<object>();

            insertSpecificName(out string modelName, "Model Vehicle");
            float amountOfEnergy = insertAmountOf("Vehicle Energy");
            insertSpecificName(out string manufactureName, "Wheel Manufacturer");
            float currentAirPressure = insertAmountOf("wheels Air Pressure");
            getPersonalDetails(out Customer owner);

            VehicleFactory.GetMoreInfo(i_UserVehicle, out InfoNeededForUser);
            foreach (KeyValuePair<string, Type> entry in InfoNeededForUser)
            {
                Console.WriteLine(entry.Key);
                string userInputInString = Console.ReadLine();
                while (!validInput)
                {
                    try
                    {
                        if (entry.Value == typeof(bool))
                        {
                            if (userInputInString != "yes" && userInputInString != "no")
                            {
                                Console.WriteLine("You must type yes/no");
                                userInputInString = Console.ReadLine();
                                continue;
                            }
                            else if (userInputInString == "yes")
                            {
                                userInput = Convert.ChangeType(true, typeof(bool));
                            }
                            else if (userInputInString == "no")
                            {
                                userInput = Convert.ChangeType(false, typeof(bool));
                            }
                        }
                        else
                        {
                            userInput = Convert.ChangeType(userInputInString, entry.Value);
                        }

                        validInput = true;
                        InfoForUser.Add(userInput);
                    }
                    catch (FormatException formatException)
                    {
                        displayFormatExceptionMessage(formatException);
                        userInputInString = Console.ReadLine();
                        validInput = false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid input! Please Try Again: ");
                        userInputInString = Console.ReadLine();
                        validInput = false;
                    }
                }

                validInput = false;
            }

            m_GarageManager.AddNewVehicle(i_UserVehicle, modelName, i_lisencePlateNumber, owner, manufactureName, currentAirPressure, InfoForUser, amountOfEnergy);
            Console.WriteLine("vehicle is created succefully.");
            System.Threading.Thread.Sleep(2000);
        }

        private bool checkIfVehicleExist(string i_LisencePlateNumber)
        {
            return m_GarageManager.IsLisencePlateNumberExist(i_LisencePlateNumber);
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

        private static float insertAmountOf(string i_DescriptionAmount)
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

        private static void insertSpecificName(out string o_Name, string i_NameDescription)
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

        private static void insertPhoneNumber(out string o_PhoneNumber)
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

        private static void getPersonalDetails(out Customer o_Owner)
        {
            insertSpecificName(out string ownerName, "vehicle owner");
            insertPhoneNumber(out string phoneNumber);
            o_Owner = new Customer(ownerName, phoneNumber);
        }

        private static VehicleState.eVehicleState chooseVehicleState()
        {
            bool validInput = false;
            VehicleState.eVehicleState newState = VehicleState.eVehicleState.InRepair;
            do
            {
                try
                {
                    displayAllTypeOfState();
                    validInput = VehicleState.TryParse(Console.ReadLine(), out newState);
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

                    displayAllTypeOfFuel();
                    validInput = EnergySourceType.eEnergySourceType.TryParse(Console.ReadLine(), out EnergySourceType.eEnergySourceType userFuelType);

                    m_GarageManager.LoadEnergySource(i_LisencePlateNumber, amountOfFuel, validInput, userFuelType);
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
                    validInput = true;
                    m_GarageManager.LoadEnergySource(i_LisencePlateNumber, amountOfEnergy, validInput, EnergySourceType.eEnergySourceType.Electric);
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

        private static void displayAllTypeOfState()
        {
            string stateName;
            for (int i = 0; i < Enum.GetNames(typeof(VehicleState.eVehicleState)).Length; i++)
            {
                stateName = ((VehicleState.eVehicleState)i).ToString();
                Console.WriteLine("To select {0} press {1}.", stateName, i);
            }
        }

        private static void displayAllTypeOfFuel()
        {
            string fuelName;
            for (int i = 2; i <= Enum.GetNames(typeof(EnergySourceType.eEnergySourceType)).Length; i++)
            {
                fuelName = ((EnergySourceType.eEnergySourceType)i).ToString();
                Console.WriteLine("To select {0} press {1}.", fuelName, i);
            }
        }

        private static void printAllTypeOfVehicles()
        {
            string vehicleType;
            for (int i = 1; i <= Enum.GetNames(typeof(VehicleFactory.eTypeVehicles)).Length; i++)
            {
                vehicleType = ((VehicleFactory.eTypeVehicles)i).ToString();
                Console.WriteLine("To select {0} press {1}.", vehicleType, i);
            }
        }

        private void displayDataOfVehicle(string i_LisencePlateNumber)
        {
            Console.WriteLine(m_GarageManager.GetDataOfVehicle(i_LisencePlateNumber));
            System.Threading.Thread.Sleep(10000);
        }

        private void displayAllLicenseNumbers()
        {
            string userChoice = "";
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
                VehicleState.eVehicleState newState = chooseVehicleState();
                m_GarageManager.GetAllLicenseNumbersByState(ref vehicleLicenses, newState);
            }
            else
            {
                m_GarageManager.GetAllLicenseNumbers(ref vehicleLicenses);
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

        private static void displayFormatExceptionMessage(FormatException i_FormatException)
        {
            System.Console.Clear();
            Console.WriteLine(i_FormatException.Message);
            System.Threading.Thread.Sleep(4000);
        }

        private static void displayValueOutOfRangeExceptionMessage(ValueOutOfRangeException i_ValueOutOfRangeException)
        {
            System.Console.Clear();
            Console.WriteLine(i_ValueOutOfRangeException.Message);
            System.Threading.Thread.Sleep(4000);
        }

        private static void displayArgumentExceptionMessage(ArgumentException i_ArgumentException)
        {
            System.Console.Clear();
            Console.WriteLine(i_ArgumentException.Message);
            System.Threading.Thread.Sleep(4000);
        }

        private static void displayGeneralExceptionMessage()
        {
            Console.WriteLine("something went wrong, Try again.");
            System.Threading.Thread.Sleep(1500);
        }
    }
}