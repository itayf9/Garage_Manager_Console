using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private Dictionary<string, VehicleInfo> m_VehicleLisenceNumberToVehiclesInformationsInTheGarage;

        public GarageManager()
        {
            m_VehicleLisenceNumberToVehiclesInformationsInTheGarage = new Dictionary<string, VehicleInfo>();
        }

        public void AddNewVehicle(VehicleFactory.eAvailableVehicleTypes i_VehicleType, string i_ModelName, string i_LicenseNumber, string i_OwnerName, string i_OwnerPhoneNumber, float i_RemainEnergyPercentege, string i_WheelManufacturerName, float i_WheelCurrentAirPressure, Dictionary<string, object> i_AdditionalSpecificProperties)
        {
            Vehicle newVehicle = VehicleFactory.createNewVehicle(i_VehicleType, i_ModelName, i_LicenseNumber, i_OwnerName, i_OwnerPhoneNumber, i_RemainEnergyPercentege, i_WheelManufacturerName, i_WheelCurrentAirPressure, i_AdditionalSpecificProperties);

            VehicleInfo newVehicleInformation = new VehicleInfo(i_OwnerName, i_OwnerPhoneNumber, newVehicle, eVehicleFixingState.InProgress);

            m_VehicleLisenceNumberToVehiclesInformationsInTheGarage.Add(i_LicenseNumber, newVehicleInformation);
        }

        public void ChangeVehicleState(string i_LisenceNumber, eVehicleFixingState i_NewState)
        {
            VehicleInfo desiredVehicleInformation = findVehicleInformationByLisenceNumber(i_LisenceNumber);

            desiredVehicleInformation.FixingState = i_NewState;
        }

        private VehicleInfo findVehicleInformationByLisenceNumber(string i_LisenceNumber)
        {
            if (!m_VehicleLisenceNumberToVehiclesInformationsInTheGarage.ContainsKey(i_LisenceNumber))
            {
                throw new ArgumentException();
            }

            return m_VehicleLisenceNumberToVehiclesInformationsInTheGarage[i_LisenceNumber];
        }

        public void InflateWheelsToMaxAirPressure(string i_LisenceNumber)
        {
            VehicleInfo desiredVehicleInformation = findVehicleInformationByLisenceNumber(i_LisenceNumber);

            foreach (Wheel wheel in desiredVehicleInformation.Vehicle.Wheels)
            {
                wheel.InflateWheel(wheel.MaxAirPressure);
            }
        }

        public void LoadEnergySource(string i_LisenceNumber, float i_AmountOfEnergy, eEnergySourceType i_EnergySourceType, eFuelType i_FuelType = eFuelType.Undefined)
        {
            VehicleInfo desiredVehicleInformation = findVehicleInformationByLisenceNumber(i_LisenceNumber);

            if (i_EnergySourceType == eEnergySourceType.Fuel)
            {
                FuelEnergy fuelEnergyOfTheDesiredVehicle = desiredVehicleInformation.Vehicle.Energy as FuelEnergy;
                if (fuelEnergyOfTheDesiredVehicle == null)
                {
                    throw new ArgumentException();
                }

                fuelEnergyOfTheDesiredVehicle.AddFuel(i_AmountOfEnergy, i_FuelType);
            }
            else if (i_EnergySourceType == eEnergySourceType.Electric)
            {
                ElectricEnergy electricEnergyOfTheDesiredVehicle = desiredVehicleInformation.Vehicle.Energy as ElectricEnergy;
                if (electricEnergyOfTheDesiredVehicle == null)
                {
                    throw new ArgumentException();
                }

                electricEnergyOfTheDesiredVehicle.ChargeBattery(i_AmountOfEnergy);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public bool IsLisenceNumberExistsInGarage(string i_LisenceNumber)
        {
            return m_VehicleLisenceNumberToVehiclesInformationsInTheGarage.ContainsKey(i_LisenceNumber);
        }

        public string GetDataOfVehicle(string i_LisenceNumber)
        {
            VehicleInfo desiredVehicleInformation = findVehicleInformationByLisenceNumber(i_LisenceNumber);

            return desiredVehicleInformation.ToString();
        }

        public void GetAllLicenseNumbers(ref List<string> io_LicenseNumbers)
        {
            io_LicenseNumbers.AddRange(m_VehicleLisenceNumberToVehiclesInformationsInTheGarage.Keys);
        }

        public void GetAllLicenseNumbersByState(ref List<string> io_FilteredLicenseNumbers, eVehicleFixingState i_VehicleFixingState)
        {
            foreach (KeyValuePair<string, VehicleInfo> pairOfLisenceNumberToVehicleInfo in m_VehicleLisenceNumberToVehiclesInformationsInTheGarage)
            {
                if (pairOfLisenceNumberToVehicleInfo.Value.FixingState == i_VehicleFixingState)
                {
                    io_FilteredLicenseNumbers.Add(pairOfLisenceNumberToVehicleInfo.Key);
                }
            }
        }
    }
}
