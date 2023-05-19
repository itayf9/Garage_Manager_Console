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

        public void AddNewVehicle(
            VehicleFactory.eVehicleType i_UserVehicle,
            string i_ModelName,
            string i_LicensePlateNumber,
            Customer i_Owner,
            string i_ManufacturerName,
            float i_CurrentAirPressure,
            List<object> i_InfoFromUser,
            float i_CurrentEnergy)
        {
        }

        public void ChangeVehicleState(string i_LisencePlateNumber, VehicleState.eVehicleState i_NewState)
        {
        }

        public void InflateWheels(string i_LisencePlateNumber)
        {
        }

        public void LoadEnergySource(string i_LisencePlateNumber, float i_AmountOfEnergy, bool i_LegalInput, EnergySourceType.eEnergySourceType i_EnergySource)
        {
        }

        public bool IsLisencePlateNumberExist(string i_LisencePlateNumber)
        {
            return false;
        }

        public string GetDataOfVehicle(string i_LisencePlateNumber)
        {
            return null;
        }

        public void GetAllLicenseNumbers(ref List<string> io_LicenseNumbers)
        {
        }

        public void GetAllLicenseNumbersByState(ref List<string> io_LicenseNumbers, VehicleState.eVehicleState i_State)
        {
        }
    }
}
