using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_color;
        private eNumberOfDoors m_numberOfDoors;

        public Car(string i_ModelName, string i_LisenceNumber, float i_RemainEnergyPercentege, Energy i_EnergySource, List<Wheel> i_Wheels, eCarColor i_CarColor, eNumberOfDoors i_NumberOfDoors) : base(i_ModelName, i_LisenceNumber, i_RemainEnergyPercentege, i_EnergySource, i_Wheels)
        {
            this.m_color = i_CarColor;
            this.m_numberOfDoors = i_NumberOfDoors;
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();

            toStringBuilder.Append(base.ToString());
            toStringBuilder.Append("Car's color: ").Append(m_color).Append("\n")
                .Append("Car's number of doors: ").Append(m_numberOfDoors).Append("\n");

            return toStringBuilder.ToString();
        }
    }
}
