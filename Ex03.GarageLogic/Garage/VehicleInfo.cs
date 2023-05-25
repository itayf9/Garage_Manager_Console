using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleInfo
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private Vehicle m_Vehicle;
        private eVehicleFixingState m_FixingState;

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
        }

        public eVehicleFixingState FixingState
        {
            get { return m_FixingState; }
            set { m_FixingState = value; }
        }

        public VehicleInfo(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle, eVehicleFixingState i_FixingState)
        {
            this.m_OwnerName = i_OwnerName;
            this.m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            this.m_Vehicle = i_Vehicle;
            this.FixingState = i_FixingState;
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();
            toStringBuilder.Append("----------------\n")
                .Append("Owner name: ").Append(m_OwnerName).Append("\n")
                .Append("Owner phone number: ").Append(m_OwnerPhoneNumber).Append("\n")
                .Append("Fixing state: ").Append(m_FixingState).Append("\n")
                .Append(Vehicle.ToString())
                .Append("----------------\n").Append("\n");

            return toStringBuilder.ToString();
        }
    }
}
