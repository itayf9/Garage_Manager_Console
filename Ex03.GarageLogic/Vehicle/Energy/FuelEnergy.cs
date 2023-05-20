using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEnergy : Energy
    {
        private readonly float r_MaxFuelAmount;
        private eFuelType m_FuelType;
        private float m_CurrentFuelAmount;

        public FuelEnergy(eFuelType fuelType, float currentFuelAmount, float maxFuelAmount)
            : base()
        {
            this.m_FuelType = fuelType;
            this.m_CurrentFuelAmount = currentFuelAmount;
            this.r_MaxFuelAmount = maxFuelAmount;
        }

        public void AddFuel(float i_FuelAmoutToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType != m_FuelType || i_FuelAmoutToAdd < 0)
            {
                throw new ArgumentException();
            }

            float newFuelAmountAfterAdding = m_CurrentFuelAmount + i_FuelAmoutToAdd;
            if (newFuelAmountAfterAdding > r_MaxFuelAmount)
            {
                throw new ValueOutOfRangeException(0, r_MaxFuelAmount);
            }
            else
            {
                m_CurrentFuelAmount = newFuelAmountAfterAdding;
            }
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();

            toStringBuilder.Append("Fuel Type: ").Append(m_FuelType).Append("\n")
                .Append("Fuel amount (current/maximum): ").Append(m_CurrentFuelAmount).Append(" / ").Append(r_MaxFuelAmount).Append("\n");

            return toStringBuilder.ToString();
        }
    }
}
