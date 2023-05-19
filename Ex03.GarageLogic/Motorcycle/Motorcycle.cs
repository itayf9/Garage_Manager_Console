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
