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
    }
}
