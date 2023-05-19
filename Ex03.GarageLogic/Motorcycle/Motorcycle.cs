using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLisenceType m_LisenceType;
        private int m_EngineVolume;

        public Motorcycle(string i_ModelName, string i_LisenceNumber, float i_RemainEnergyPercentege, Energy i_EnergySource, List<Wheel> i_Wheels, eLisenceType i_LisenceType, int i_EngineVolume) : base(i_ModelName, i_LisenceNumber, i_RemainEnergyPercentege, i_EnergySource, i_Wheels)
        {
            this.m_LisenceType = i_LisenceType;
            this.m_EngineVolume = i_EngineVolume;
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();

            toStringBuilder.Append(base.ToString());
            toStringBuilder.Append("Motorcycle's lisence type: ").Append(m_LisenceType).Append("\n")
                .Append("Motorcycle's engine volume: ").Append(m_EngineVolume).Append("\n");

            return toStringBuilder.ToString();
        }
    }
}
