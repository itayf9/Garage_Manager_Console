using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {

        public enum eAvailableVehicleTypes
        {
            FuelBasedCar = 1,
            ElectricBasedCar,
            FuelBasedMotorcycle,
            ElectricBasedMotorcycle,
            Truck,
        }

        public static readonly string sr_PropertyNameOfCarColor = "Car Color";
        public static readonly string sr_PropertyNameOfNumberOfDoors = "Number Of Doors";
        public static readonly string sr_PropertyNameOfLisenceType = "Lisence Type";
        public static readonly string sr_PropertyNameOfEngineVolume = "Engine Volume (a natural number)";
        public static readonly string sr_PropertyNameOfIsCarriesDangerousLoads = "Is Carries Dangarous Loads (Yes/No)";
        public static readonly string sr_PropertyNameOfLoadVolume = "Load Volume (a positive real number)";

        public static readonly float sr_MaxFuelAmountForFuelBasedCar = 46f;
        public static readonly float sr_MaxBatteryTimeInHoursForElectricBasedCar = 5.2f;
        public static readonly float sr_MaxWheelAirPressureForCar = 33f;
        public static readonly int sr_NumberOfWheelsForCar = 5;

        public static readonly float sr_MaxFuelAmountForFuelBasedMotocycle = 6.4f;
        public static readonly float sr_MaxBatteryTimeInHoursForElectricBasedMotorcycle = 2.6f;
        public static readonly float sr_MaxWheelAirPressureForMotorcycle = 31f;
        public static readonly int sr_NumberOfWheelsForMotorcycle = 2;

        public static readonly float sr_MaxFuelAmountForFuelBasedTruck = 135f;
        public static readonly float sr_MaxWheelAirPressureForTruck = 26f;
        public static readonly float sr_NumberOfWheelsForTruck = 14;

        public static Dictionary<string, Type> GetAddidtionalSpecificPropertiesNameAndTypesForAVehicle(eAvailableVehicleTypes i_VehicleType)
        {
            Dictionary<string, Type> dictionaryOfAditionalProperties = new Dictionary<string, Type>();

            switch (i_VehicleType)
            {
                case eAvailableVehicleTypes.FuelBasedCar:
                case eAvailableVehicleTypes.ElectricBasedCar:
                    dictionaryOfAditionalProperties.Add(sr_PropertyNameOfCarColor, typeof(eCarColor));
                    dictionaryOfAditionalProperties.Add(sr_PropertyNameOfNumberOfDoors, typeof(eNumberOfDoors));
                    break;
                case eAvailableVehicleTypes.FuelBasedMotorcycle:
                case eAvailableVehicleTypes.ElectricBasedMotorcycle:
                    dictionaryOfAditionalProperties.Add(sr_PropertyNameOfLisenceType, typeof(eLisenceType));
                    dictionaryOfAditionalProperties.Add(sr_PropertyNameOfEngineVolume, typeof(int));
                    break;
                case eAvailableVehicleTypes.Truck:
                    dictionaryOfAditionalProperties.Add(sr_PropertyNameOfIsCarriesDangerousLoads, typeof(bool));
                    dictionaryOfAditionalProperties.Add(sr_PropertyNameOfLoadVolume, typeof(float));
                    break;
            }

            return dictionaryOfAditionalProperties;
        }

        public static Vehicle createNewVehicle(eAvailableVehicleTypes i_VehicleType, string i_ModelName, string i_LicenseNumber, string i_OwnerName, string i_OwnerPhoneNumber, float i_RemainEnergyPercentege, string i_WheelManufacturerName, float i_WheelCurrentAirPressure, Dictionary<string, object> i_AdditionalSpecificProperties)
        {
            Vehicle newlyCreatedVehicle;
            Energy energySourceOfNewlyCreatedVehicle;
            List<Wheel> wheelsOfNewlyCreatedVehicle;

            try
            {
                verifyValidEnergyPercentegeValue(i_RemainEnergyPercentege);
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                throw new ArgumentException($"The energy percentage is out of range. The allowed range is bettwen {valueOutOfRangeException.MinValue} and {valueOutOfRangeException.MaxValue}.");
            }

            try
            {
                switch (i_VehicleType)
                {
                    case eAvailableVehicleTypes.FuelBasedCar:
                        energySourceOfNewlyCreatedVehicle = new FuelEnergy(i_RemainEnergyPercentege, eFuelType.Octan95, calculateAmountOfEnergyFromPercentage(i_RemainEnergyPercentege, sr_MaxFuelAmountForFuelBasedCar), sr_MaxFuelAmountForFuelBasedCar);
                        verifyValidWheelAirPressure(i_WheelCurrentAirPressure, sr_MaxWheelAirPressureForCar);
                        wheelsOfNewlyCreatedVehicle = new List<Wheel>();
                        for (int i = 0; i < sr_NumberOfWheelsForCar; i++)
                        {
                            wheelsOfNewlyCreatedVehicle.Add(new Wheel(i_WheelManufacturerName, i_WheelCurrentAirPressure, sr_MaxWheelAirPressureForCar));
                        }

                        newlyCreatedVehicle = new Car(i_ModelName, i_LicenseNumber, energySourceOfNewlyCreatedVehicle, wheelsOfNewlyCreatedVehicle, (eCarColor)i_AdditionalSpecificProperties[sr_PropertyNameOfCarColor], (eNumberOfDoors)i_AdditionalSpecificProperties[sr_PropertyNameOfNumberOfDoors]);
                        break;
                    case eAvailableVehicleTypes.ElectricBasedCar:
                        energySourceOfNewlyCreatedVehicle = new ElectricEnergy(i_RemainEnergyPercentege, calculateAmountOfEnergyFromPercentage(i_RemainEnergyPercentege, sr_MaxBatteryTimeInHoursForElectricBasedCar), sr_MaxBatteryTimeInHoursForElectricBasedCar);
                        verifyValidWheelAirPressure(i_WheelCurrentAirPressure, sr_MaxWheelAirPressureForCar);
                        wheelsOfNewlyCreatedVehicle = new List<Wheel>();
                        for (int i = 0; i < sr_NumberOfWheelsForCar; i++)
                        {
                            wheelsOfNewlyCreatedVehicle.Add(new Wheel(i_WheelManufacturerName, i_WheelCurrentAirPressure, sr_MaxWheelAirPressureForCar));
                        }

                        newlyCreatedVehicle = new Car(i_ModelName, i_LicenseNumber, energySourceOfNewlyCreatedVehicle, wheelsOfNewlyCreatedVehicle, (eCarColor)i_AdditionalSpecificProperties[sr_PropertyNameOfCarColor], (eNumberOfDoors)i_AdditionalSpecificProperties[sr_PropertyNameOfNumberOfDoors]);
                        break;
                    case eAvailableVehicleTypes.FuelBasedMotorcycle:
                        energySourceOfNewlyCreatedVehicle = new FuelEnergy(i_RemainEnergyPercentege, eFuelType.Octan98, calculateAmountOfEnergyFromPercentage(i_RemainEnergyPercentege, sr_MaxFuelAmountForFuelBasedMotocycle), sr_MaxFuelAmountForFuelBasedMotocycle);
                        verifyValidWheelAirPressure(i_WheelCurrentAirPressure, sr_MaxWheelAirPressureForMotorcycle);
                        wheelsOfNewlyCreatedVehicle = new List<Wheel>();
                        for (int i = 0; i < sr_NumberOfWheelsForMotorcycle; i++)
                        {
                            wheelsOfNewlyCreatedVehicle.Add(new Wheel(i_WheelManufacturerName, i_WheelCurrentAirPressure, sr_MaxWheelAirPressureForMotorcycle));
                        }

                        newlyCreatedVehicle = new Motorcycle(i_ModelName, i_LicenseNumber, energySourceOfNewlyCreatedVehicle, wheelsOfNewlyCreatedVehicle, (eLisenceType)i_AdditionalSpecificProperties[sr_PropertyNameOfLisenceType], (int)i_AdditionalSpecificProperties[sr_PropertyNameOfEngineVolume]);
                        break;
                    case eAvailableVehicleTypes.ElectricBasedMotorcycle:
                        energySourceOfNewlyCreatedVehicle = new ElectricEnergy(i_RemainEnergyPercentege, calculateAmountOfEnergyFromPercentage(i_RemainEnergyPercentege, sr_MaxBatteryTimeInHoursForElectricBasedMotorcycle), sr_MaxBatteryTimeInHoursForElectricBasedMotorcycle);
                        verifyValidWheelAirPressure(i_WheelCurrentAirPressure, sr_MaxWheelAirPressureForMotorcycle);
                        wheelsOfNewlyCreatedVehicle = new List<Wheel>();
                        for (int i = 0; i < sr_NumberOfWheelsForMotorcycle; i++)
                        {
                            wheelsOfNewlyCreatedVehicle.Add(new Wheel(i_WheelManufacturerName, i_WheelCurrentAirPressure, sr_MaxWheelAirPressureForMotorcycle));
                        }

                        newlyCreatedVehicle = new Motorcycle(i_ModelName, i_LicenseNumber, energySourceOfNewlyCreatedVehicle, wheelsOfNewlyCreatedVehicle, (eLisenceType)i_AdditionalSpecificProperties[sr_PropertyNameOfLisenceType], (int)i_AdditionalSpecificProperties[sr_PropertyNameOfEngineVolume]);
                        break;
                    case eAvailableVehicleTypes.Truck:
                        energySourceOfNewlyCreatedVehicle = new FuelEnergy(i_RemainEnergyPercentege, eFuelType.Soler, calculateAmountOfEnergyFromPercentage(i_RemainEnergyPercentege, sr_MaxFuelAmountForFuelBasedTruck), sr_MaxFuelAmountForFuelBasedTruck);
                        verifyValidWheelAirPressure(i_WheelCurrentAirPressure, sr_MaxWheelAirPressureForTruck);
                        wheelsOfNewlyCreatedVehicle = new List<Wheel>();
                        for (int i = 0; i < sr_NumberOfWheelsForTruck; i++)
                        {
                            wheelsOfNewlyCreatedVehicle.Add(new Wheel(i_WheelManufacturerName, i_WheelCurrentAirPressure, sr_MaxWheelAirPressureForTruck));
                        }

                        newlyCreatedVehicle = new Truck(i_ModelName, i_LicenseNumber, energySourceOfNewlyCreatedVehicle, wheelsOfNewlyCreatedVehicle, (bool)i_AdditionalSpecificProperties[sr_PropertyNameOfIsCarriesDangerousLoads], (float)i_AdditionalSpecificProperties[sr_PropertyNameOfLoadVolume]);
                        break;
                    default:
                        newlyCreatedVehicle = null;
                        break;
                }
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                throw new ArgumentException($"The wheels' air pressure is out of range. The allowed range is bettwen {valueOutOfRangeException.MinValue} and {valueOutOfRangeException.MaxValue}.");
            }

            return newlyCreatedVehicle;
        }

        private static void verifyValidWheelAirPressure(float i_WheelCurrentAirPressure, float i_WheelMaxAirPressure)
        {
            if (i_WheelCurrentAirPressure < 0 || i_WheelCurrentAirPressure > i_WheelMaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, i_WheelMaxAirPressure);
            }
        }

        private static void verifyValidEnergyPercentegeValue(float i_RemainEnergyPercentege)
        {
            if (i_RemainEnergyPercentege < 0 || i_RemainEnergyPercentege > 100)
            {
                throw new ValueOutOfRangeException(0, 100);
            }
        }

        private static float calculateAmountOfEnergyFromPercentage(float i_CurrentAmount, float i_MaxAmount)
        {
            return i_CurrentAmount * i_MaxAmount / 100f;
        }

    }
}
