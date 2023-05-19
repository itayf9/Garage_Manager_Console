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

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();
            toStringBuilder.Append("Owner name: ").Append(m_OwnerName).Append("\n")
                .Append("Owner phone number: ").Append(m_OwnerPhoneNumber).Append("\n")
                .Append("Fixing state: ").Append(m_FixingState).Append("\n")
                .Append(Vehicle.ToString()).Append("\n");

            return toStringBuilder.ToString();
        }
    }
}
