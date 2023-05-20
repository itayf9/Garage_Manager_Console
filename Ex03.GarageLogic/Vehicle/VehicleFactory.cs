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
            FuelBasedCar,
            ElectricBasedCar,
            FuelBasedMotorcycle,
            ElectricBasedMotorcycle,
            Truck,
        }

        public static readonly string sr_PropertyNameOfCarColor = "Car Color";
        public static readonly string sr_PropertyNameOfNumberOfDoors = "Number Of Doors";
        public static readonly string sr_PropertyNameOfLisenceType = "Lisence Type";
        public static readonly string sr_PropertyNameOfEngineVolume = "Engine Volume";
        public static readonly string sr_PropertyNameOfIsCarriesDangerousLoads = "Is Carries Dangarous Loads";
        public static readonly string sr_PropertyNameOfLoadVolume = "Load Volume";

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

        public static Vehicle createNewVehicle(eAvailableVehicleTypes i_VehicleType, string i_ModelName, string i_LicenseNumber, string i_OwnerName, string i_OwnerPhoneNumber, float i_RemainEnergyPercentege, string i_WheelManufacturerName, float i_WheelCurrentAirPressure, Dictionary<string ,object> i_AdditionalSpecificProperties)
        {
            Vehicle newlyCreatedVehicle;
            Energy energySourceOfNewlyCreatedVehicle;
            List<Wheel> wheelsOfNewlyCreatedVehicle;

            switch (i_VehicleType)
            {
                case eAvailableVehicleTypes.FuelBasedCar:
                    energySourceOfNewlyCreatedVehicle = new FuelEnergy(eFuelType.Octan95, i_RemainEnergyPercentege, 46);
                    wheelsOfNewlyCreatedVehicle = new List<Wheel>();
                    for (int i = 0; i < 5; i++)
                    {
                        wheelsOfNewlyCreatedVehicle.Add(new Wheel(i_WheelManufacturerName, i_WheelCurrentAirPressure, 33));
                    }

                    newlyCreatedVehicle = new Car(i_ModelName, i_LicenseNumber, i_RemainEnergyPercentege, energySourceOfNewlyCreatedVehicle, wheelsOfNewlyCreatedVehicle, (eCarColor)i_AdditionalSpecificProperties[sr_PropertyNameOfCarColor], (eNumberOfDoors)i_AdditionalSpecificProperties[sr_PropertyNameOfNumberOfDoors]);
                    break;
                case eAvailableVehicleTypes.ElectricBasedCar:
                    energySourceOfNewlyCreatedVehicle = new ElectricEnergy(i_RemainEnergyPercentege, 5.2f);
                    wheelsOfNewlyCreatedVehicle = new List<Wheel>();
                    for (int i = 0; i < 5; i++)
                    {
                        wheelsOfNewlyCreatedVehicle.Add(new Wheel(i_WheelManufacturerName, i_WheelCurrentAirPressure, 33));
                    }

                    newlyCreatedVehicle = new Car(i_ModelName, i_LicenseNumber, i_RemainEnergyPercentege, energySourceOfNewlyCreatedVehicle, wheelsOfNewlyCreatedVehicle, (eCarColor)i_AdditionalSpecificProperties[sr_PropertyNameOfCarColor], (eNumberOfDoors)i_AdditionalSpecificProperties[sr_PropertyNameOfNumberOfDoors]);
                    break;
                case eAvailableVehicleTypes.FuelBasedMotorcycle:
                    energySourceOfNewlyCreatedVehicle = new FuelEnergy(eFuelType.Octan98, i_RemainEnergyPercentege, 6.4f);
                    wheelsOfNewlyCreatedVehicle = new List<Wheel>();
                    for (int i = 0; i < 2; i++)
                    {
                        wheelsOfNewlyCreatedVehicle.Add(new Wheel(i_WheelManufacturerName, i_WheelCurrentAirPressure, 31));
                    }

                    newlyCreatedVehicle = new Motorcycle(i_ModelName, i_LicenseNumber, i_RemainEnergyPercentege, energySourceOfNewlyCreatedVehicle, wheelsOfNewlyCreatedVehicle, (eLisenceType)i_AdditionalSpecificProperties[sr_PropertyNameOfLisenceType], (int)i_AdditionalSpecificProperties[sr_PropertyNameOfEngineVolume]);
                    break;
                case eAvailableVehicleTypes.ElectricBasedMotorcycle:
                    energySourceOfNewlyCreatedVehicle = new ElectricEnergy(i_RemainEnergyPercentege, 2.6f);
                    wheelsOfNewlyCreatedVehicle = new List<Wheel>();
                    for (int i = 0; i < 2; i++)
                    {
                        wheelsOfNewlyCreatedVehicle.Add(new Wheel(i_WheelManufacturerName, i_WheelCurrentAirPressure, 31));
                    }

                    newlyCreatedVehicle = new Motorcycle(i_ModelName, i_LicenseNumber, i_RemainEnergyPercentege, energySourceOfNewlyCreatedVehicle, wheelsOfNewlyCreatedVehicle, (eLisenceType)i_AdditionalSpecificProperties[sr_PropertyNameOfLisenceType], (int)i_AdditionalSpecificProperties[sr_PropertyNameOfEngineVolume]);
                    break;
                case eAvailableVehicleTypes.Truck:
                    energySourceOfNewlyCreatedVehicle = new FuelEnergy(eFuelType.Soler, i_RemainEnergyPercentege, 135);
                    wheelsOfNewlyCreatedVehicle = new List<Wheel>();
                    for (int i = 0; i < 14; i++)
                    {
                        wheelsOfNewlyCreatedVehicle.Add(new Wheel(i_WheelManufacturerName, i_WheelCurrentAirPressure, 31));
                    }

                    newlyCreatedVehicle = new Truck(i_ModelName, i_LicenseNumber, i_RemainEnergyPercentege, energySourceOfNewlyCreatedVehicle, wheelsOfNewlyCreatedVehicle, (bool)i_AdditionalSpecificProperties[sr_PropertyNameOfIsCarriesDangerousLoads], (float)i_AdditionalSpecificProperties[sr_PropertyNameOfLoadVolume]);
                    break;
                default:
                    newlyCreatedVehicle = null;
                    break;
            }

            return newlyCreatedVehicle;
        }
    }
}
