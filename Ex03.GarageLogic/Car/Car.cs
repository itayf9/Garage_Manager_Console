using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private readonly eNumberOfDoors r_NumberOfDoors;
        private eCarColor m_Color;

        public Car(string i_ModelName, string i_LisenceNumber, Energy i_EnergySource, List<Wheel> i_Wheels, eCarColor i_CarColor, eNumberOfDoors i_NumberOfDoors) : base(i_ModelName, i_LisenceNumber, i_EnergySource, i_Wheels)
        {
            this.m_Color = i_CarColor;
            this.r_NumberOfDoors = i_NumberOfDoors;
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();

            toStringBuilder.Append(base.ToString());
            toStringBuilder.Append("Car's color: ").Append(m_Color).Append("\n")
                .Append("Car's number of doors: ").Append(r_NumberOfDoors).Append("\n");

            return toStringBuilder.ToString();
        }
    }
}
