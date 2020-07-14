using System;
using System.Text;
using Ex03.GarageLogic.EnumClasses;
using Ex03.GarageLogic.ExceptionsFolder;

namespace Ex03.GarageLogic.InnerVehicleObjects
{
    internal class FuelEngine : Engine
    {
        private readonly Resources.eResourceType r_ResourceType;

        internal FuelEngine(float i_MaxAmountOfResources, Resources.eResourceType i_ResourceType)
            : base(i_MaxAmountOfResources)
        {
            r_ResourceType = i_ResourceType;
        }

        internal override void AddResources(float i_AmountOfResourcesToAdd, Resources.eResourceType i_ResourceType)
        {
            if (i_ResourceType != r_ResourceType)
            {
                if (r_MaxAmountOfResources < i_AmountOfResourcesToAdd + m_AmountOfResources)
                {
                    throw new ArgumentException("You entered wrong gas type and amount for your vehicle");
                }

                throw new ArgumentException("You entered wrong gas type for your vehicle");
            }

            if(r_MaxAmountOfResources < i_AmountOfResourcesToAdd + m_AmountOfResources)
            {
                throw new ValueOutOfRangeException(null, i_AmountOfResourcesToAdd, "Fuel Engine", "You were trying to add more fuel than your fuel engine may have");
            }

            if(i_AmountOfResourcesToAdd < 0)
            {
                throw new ValueOutOfRangeException(
                    null,
                    i_AmountOfResourcesToAdd,
                    "Fuel Engine",
                    "Fuel may not be negative amount");
            }

            m_AmountOfResources += i_AmountOfResourcesToAdd;
            m_EnergyPercentage = m_AmountOfResources / r_MaxAmountOfResources;
        }

        public override string ToString()
        {
            StringBuilder stringToReturn = new StringBuilder();
            stringToReturn.AppendFormat(
                "Fuel Engine that accepts fuel of type {0} of maximum capacity of {1} and has {2} liters of fuel left (fuel percentage is {3})",
                r_ResourceType,
                r_MaxAmountOfResources,
                m_AmountOfResources,
                m_EnergyPercentage);
            return stringToReturn.ToString();
        }
    }
}
