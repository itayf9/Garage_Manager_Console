using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Energy
    {
        protected float m_RemainEnergyPercentege;

        protected Energy(float i_RemainEnergyPercentege)
        {
            this.m_RemainEnergyPercentege = i_RemainEnergyPercentege;
        }

        public float RemainEnergyPersentege
        {
            get { return m_RemainEnergyPercentege; }
            set { m_RemainEnergyPercentege = value; }
        }

        public abstract void updatePercentegeOfRemainingEnergy();

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();
            toStringBuilder.Append("Remaining Energy Persentege: ").Append(m_RemainEnergyPercentege).Append("%\n").ToString();
            return toStringBuilder.ToString();
        }
    }
}
