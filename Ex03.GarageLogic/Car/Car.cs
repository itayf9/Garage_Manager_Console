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
