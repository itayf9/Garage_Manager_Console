using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsCarriesDangerousLoads;
        private float m_LoadVolume;

        public Truck(string i_ModelName, string i_LisenceNumber, Energy i_EnergySource, List<Wheel> i_Wheels, bool i_IsCarriesDangerousLoads, float i_LoadVolume) : base(i_ModelName, i_LisenceNumber, i_EnergySource, i_Wheels)
        {
            this.m_IsCarriesDangerousLoads = i_IsCarriesDangerousLoads;
            this.m_LoadVolume = i_LoadVolume;
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();
            toStringBuilder.Append(base.ToString())
                .Append("Carries Dangarous Loads: ").Append(m_IsCarriesDangerousLoads).Append("\n")
                .Append("Load Volume: ").Append(m_LoadVolume).Append("\n");

            return toStringBuilder.ToString();
        }
    }
}
