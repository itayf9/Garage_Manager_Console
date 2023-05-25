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

        public FuelEnergy(float i_RemainEnergyPercentege, eFuelType i_FuelType, float i_CurrentFuelAmount, float i_MaxFuelAmount)
            : base(i_RemainEnergyPercentege)
        {
            this.m_FuelType = i_FuelType;
            this.m_CurrentFuelAmount = i_CurrentFuelAmount;
            this.r_MaxFuelAmount = i_MaxFuelAmount;
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
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

        public override void updatePercentegeOfRemainingEnergy()
        {
            this.m_RemainEnergyPercentege = m_CurrentFuelAmount / r_MaxFuelAmount * 100;
        }

        public override string ToString()
        {
            StringBuilder toStringBuilder = new StringBuilder();

            toStringBuilder.Append(base.ToString())
                .Append("Fuel Type: ").Append(m_FuelType).Append("\n")
                .Append("Fuel amount (current/maximum): ").Append(m_CurrentFuelAmount).Append(" / ").Append(r_MaxFuelAmount).Append("\n");

            return toStringBuilder.ToString();
        }

    }
}
