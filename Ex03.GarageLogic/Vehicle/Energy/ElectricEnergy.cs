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
        private readonly float r_BatteryMaxTimeInHours;

        public ElectricEnergy(float i_RemainEnergyPercentege, float i_BatteryRemainingTimeInHours, float i_BatteryMaxTimeInHours) : base(i_RemainEnergyPercentege)
        {
            this.m_BatteryRemainingTimeInHours = i_BatteryRemainingTimeInHours;
            this.r_BatteryMaxTimeInHours = i_BatteryMaxTimeInHours;
        }

        public float BatteryRemainingTimeInHours
        {
            get
            {
                return m_BatteryRemainingTimeInHours;
            }
        }

        public void ChargeBattery(float i_TimeInHoursToCharge)
        {
            if (i_TimeInHoursToCharge < 0)
            {
                throw new ArgumentException();
            }

            float newTimeInHoursAfterCharging = m_BatteryRemainingTimeInHours + i_TimeInHoursToCharge;
            if (newTimeInHoursAfterCharging > r_BatteryMaxTimeInHours)
            {
                throw new ValueOutOfRangeException(0, r_BatteryMaxTimeInHours);
            }
            else
            {
                m_BatteryRemainingTimeInHours = newTimeInHoursAfterCharging;
            }
        }

        public override void updatePercentegeOfRemainingEnergy()
        {
            m_RemainEnergyPercentege = m_BatteryRemainingTimeInHours / r_BatteryMaxTimeInHours * 100;
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();

            toStringBuilder.Append(base.ToString()).Append("Battery time (remaining/maximum): ").Append(m_BatteryRemainingTimeInHours).Append(" / ").Append(r_BatteryMaxTimeInHours).Append(" hours.\n");

            return toStringBuilder.ToString();
        }
    }
}
