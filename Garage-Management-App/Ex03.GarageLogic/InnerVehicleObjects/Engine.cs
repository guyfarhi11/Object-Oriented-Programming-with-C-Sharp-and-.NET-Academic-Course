using Ex03.GarageLogic.EnumClasses;

namespace Ex03.GarageLogic.InnerVehicleObjects
{
    internal abstract class Engine
    {
        protected readonly float r_MaxAmountOfResources;
        protected float m_AmountOfResources;
        protected float m_EnergyPercentage;

        internal Engine(float i_MaxAmountOfResources)
        {
            r_MaxAmountOfResources = i_MaxAmountOfResources;
            m_AmountOfResources = 0;
            m_EnergyPercentage = 0;
        }

        internal abstract void AddResources(float i_AmountOfResourcesToAdd, Resources.eResourceType i_ResourceType);
    }
}
