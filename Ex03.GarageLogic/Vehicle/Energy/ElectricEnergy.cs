using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEnergy : Energy
    {
        private float m_BatteryRemainingTimeInHours;
        private float m_BatteryMaxTimeInHours;

        public void ChargeBattery(float i_TimeInHoursToCharge)
        {
            if (i_TimeInHoursToCharge < 0)
            {
                throw new ArgumentException();
            }

            float newTimeInHoursAfterCharging = m_BatteryRemainingTimeInHours + i_TimeInHoursToCharge;
            if (newTimeInHoursAfterCharging > m_BatteryMaxTimeInHours)
            {
                throw new ValueOutOfRangeException(0, m_BatteryMaxTimeInHours);
            }
            else
            {
                m_BatteryRemainingTimeInHours = newTimeInHoursAfterCharging;
            }
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();

            toStringBuilder.Append("Battery time (remaining/maximum): ").Append(m_BatteryRemainingTimeInHours).Append(" / ").Append(m_BatteryMaxTimeInHours).Append(" hours.\n");

            return toStringBuilder.ToString();
        }
    }
}
