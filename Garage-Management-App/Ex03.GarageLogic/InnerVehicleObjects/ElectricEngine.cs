using System;
using System.Text;
using Ex03.GarageLogic.EnumClasses;
using Ex03.GarageLogic.ExceptionsFolder;

namespace Ex03.GarageLogic.InnerVehicleObjects
{
    internal class ElectricEngine : Engine
    {
        private const Resources.eResourceType k_ResourceType = Resources.eResourceType.Electricity;

        internal ElectricEngine(float i_MaxAmountOfResources)
            : base(i_MaxAmountOfResources)
        {
        }

        internal override void AddResources(float i_AmountOfResourcesToAdd, Resources.eResourceType i_ResourceType)
        {
            if(i_ResourceType != Resources.eResourceType.Electricity)
            {
                throw new ArgumentException("You entered gas for electric vehicle, Lets check if you need any other assistance ", new Exception("out"));
            }

            if (r_MaxAmountOfResources < i_AmountOfResourcesToAdd + m_AmountOfResources)
            {
                throw new ValueOutOfRangeException(null, i_AmountOfResourcesToAdd, "Electric Engine", "Sorry, but you were trying to add more electric battery time than your electric engine can contain");
            }

            if (i_AmountOfResourcesToAdd < 0)
            {
                throw new ValueOutOfRangeException(null, i_AmountOfResourcesToAdd, "Electric Engine", "electric battery time may not be negative amount");
            }

            m_AmountOfResources += i_AmountOfResourcesToAdd;
            m_EnergyPercentage = m_AmountOfResources / r_MaxAmountOfResources;
        }

        public override string ToString()
        {
            StringBuilder stringToReturn = new StringBuilder();
            stringToReturn.AppendFormat(
                "ElectricEngine that has maximum capacity battery of {0} and has {1} hours of battery left (battery percentage is {2})",
                r_MaxAmountOfResources,
                m_AmountOfResources,
                m_EnergyPercentage);
            return stringToReturn.ToString();
        }
    }
}
