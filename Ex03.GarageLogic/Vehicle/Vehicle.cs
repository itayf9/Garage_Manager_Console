using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        private string m_ModelName;
        private string m_LisenceNumber;
        private Energy m_EnergySource;
        private List<Wheel> m_Wheels;

        public Energy Energy
        {
            get { return m_EnergySource; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
        }

        public Vehicle(string i_ModelName, string i_LisenceNumber, Energy i_EnergySource, List<Wheel> i_Wheels)
        {
            this.m_ModelName = i_ModelName;
            this.m_LisenceNumber = i_LisenceNumber;
            this.m_EnergySource = i_EnergySource;
            this.m_Wheels = i_Wheels;
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();

            toStringBuilder.Append("Model name: ").Append(m_ModelName).Append("\n")
                .Append("Lisence number: ").Append(m_LisenceNumber).Append("\n")
                .Append("Wheels: ").Append("\n");
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                toStringBuilder.Append(i + 1).Append(". ").Append(m_Wheels[i].ToString()).Append("\n");
            }

            FuelEnergy vehicleEnergySourceAsFuel = m_EnergySource as FuelEnergy;
            if (vehicleEnergySourceAsFuel != null)
            {
                toStringBuilder.Append(vehicleEnergySourceAsFuel.ToString());
            }
            else
            {
                ElectricEnergy vehicleEnergySourceAsElectric = m_EnergySource as ElectricEnergy;
                if (vehicleEnergySourceAsElectric != null)
                {
                    toStringBuilder.Append(vehicleEnergySourceAsElectric.ToString());
                }
            }

            return toStringBuilder.ToString();
        }
    }
}
