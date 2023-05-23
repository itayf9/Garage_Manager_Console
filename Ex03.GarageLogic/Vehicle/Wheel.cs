using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
        }

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            this.m_ManufacturerName = i_ManufacturerName;
            this.m_CurrentAirPressure = i_CurrentAirPressure;
            this.m_MaxAirPressure = i_MaxAirPressure;
        }

        public void InflateWheel(float i_AmountOfAirToAdd)
        {
            if (i_AmountOfAirToAdd < 0)
            {
                throw new ArgumentException();
            }

            float newAirPressureAfterAdding = m_CurrentAirPressure + i_AmountOfAirToAdd;
            if (newAirPressureAfterAdding > m_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, m_MaxAirPressure);
            }
            else
            {
                m_CurrentAirPressure = newAirPressureAfterAdding;
            }
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();

            toStringBuilder.Append("Manufacturer name: ").Append(m_ManufacturerName).Append("\n")
                .Append("Air peressure (current/maximum): ").Append(m_CurrentAirPressure).Append(" / ").Append(m_MaxAirPressure).Append("\n");

            return toStringBuilder.ToString();
        }
    }
}
