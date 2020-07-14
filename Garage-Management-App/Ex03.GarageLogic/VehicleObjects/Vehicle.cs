using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic.EnumClasses;
using Ex03.GarageLogic.GarageManagement;
using Ex03.GarageLogic.InnerVehicleObjects;

namespace Ex03.GarageLogic.VehicleObjects
{
	internal abstract class Vehicle
	{
		protected Engine m_Engine;
		protected string m_ModelName;
		protected string m_LicenseNumber;
		protected float m_AmountOfEnergyLeft;
        protected float m_EnergyLeftPercentage;
		protected List<Wheel> m_ListOfWheels;
		protected Registration.eVehiclesType m_Type;

        protected Vehicle(string i_ModelName, string i_LicenseNumber, string i_WheelManufacturerName, Registration.eVehiclesType i_Type)
		{
			m_ModelName = i_ModelName;
			m_LicenseNumber = i_LicenseNumber;
			m_AmountOfEnergyLeft = 0;
			int amountOfWheel = (int)Registration.sr_NumberOfWheels[i_Type];
			m_ListOfWheels = new List<Wheel>(amountOfWheel);
			for (int i = 0; i < amountOfWheel; i++)
			{
				m_ListOfWheels.Add(new Wheel(i_WheelManufacturerName, 0, Registration.sr_MaximalAirPressure[i_Type]));
			}

			m_Type = i_Type;
		}
		
		internal abstract KeyValuePair<string, Type> GetCurrentCarKeyValuePairByIndexFromVehicle(int i_Index);

        internal abstract int GetAmountOfSpecialAttribute();

        internal abstract void InsertSpecialAttribute(int i_AttributeIndex, string i_Attribute);

        internal string AskForTypeOfFuel()
		{
			StringBuilder stringToReturn = new StringBuilder();
			if(m_Engine is FuelEngine)
			{
				stringToReturn.AppendFormat("Fuel");
			}
			else
			{
				stringToReturn.AppendFormat("Electric battery life");
			}

			return stringToReturn.ToString();
		}

        internal string LicenseNumber
		{
			get { return m_LicenseNumber; }
			set { m_LicenseNumber = value; }
		}

        internal List<Wheel> ListOfWheels
		{
            get { return m_ListOfWheels; }
            set { m_ListOfWheels = value; }
        }

        internal Engine Engine
        {
            get { return m_Engine; }
        }

        internal Registration.eVehiclesType Type
        {
            get { return m_Type; }
        }

        internal void AddResourcesToEngine(float i_AmountOfResourcesToAdd, Resources.eResourceType i_ResourceType)
		{
			m_Engine.AddResources(i_AmountOfResourcesToAdd, i_ResourceType);
		}

        internal void AddCurrentWheelPressure(float i_WheelPressure)
		{
			foreach(Wheel wheel in m_ListOfWheels)
			{
				wheel.AddCurrentWheelPressure(i_WheelPressure);
			}
		}

		public override string ToString()
		{
			StringBuilder stringToReturn = new StringBuilder();
			stringToReturn.AppendFormat("Model name: {0} License number {1} has {2} wheels:{3}", m_ModelName, m_LicenseNumber, m_ListOfWheels.Count(), Environment.NewLine);
            int i = 0;
            foreach (Wheel wheel in this.m_ListOfWheels)
            {
                i++;
				stringToReturn.AppendFormat("wheel no.{0}: {1}{2}", i, wheel.ToString(), Environment.NewLine);
			}

			stringToReturn.AppendFormat("Engine info: {0} {1}", m_Engine.ToString(), Environment.NewLine);
			return stringToReturn.ToString();
		}
    }
}
