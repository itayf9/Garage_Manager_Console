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

        public Truck (string i_ModelName, string i_LisenceNumber, float i_RemainEnergyPercentege, Energy i_EnergySource, List<Wheel> i_Wheels, bool i_IsCarriesDangerousLoads, float i_LoadVolume) : base(i_ModelName, i_LisenceNumber, i_RemainEnergyPercentege, i_EnergySource, i_Wheels)
        {
            this.m_IsCarriesDangerousLoads = i_IsCarriesDangerousLoads;
            this.m_LoadVolume = i_LoadVolume;
        }
    }
}
