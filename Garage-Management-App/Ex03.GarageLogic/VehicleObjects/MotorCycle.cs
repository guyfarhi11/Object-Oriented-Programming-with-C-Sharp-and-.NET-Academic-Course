using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.EnumClasses;
using Ex03.GarageLogic.ExceptionsFolder;
using Ex03.GarageLogic.GarageManagement;
using Ex03.GarageLogic.InnerVehicleObjects;

namespace Ex03.GarageLogic.VehicleObjects
{
    internal class MotorCycle : Vehicle
    {
        private const int k_AmountOfSpecialAttribute = 2;
        private readonly List<KeyValuePair<string, Type>> r_SpecialAttribute;
        private License.eMotorCycleLicenseType m_MotorCycleLicenseType;
        private int m_EngineVolume;
        
        internal MotorCycle(
            string i_ModelName,
            string i_ModelNumber,
            string i_WheelManufacturerName,
            Registration.eVehiclesType i_Type)
            : base(i_ModelName, i_ModelNumber, i_WheelManufacturerName, i_Type)
        {
            if (i_Type == Registration.eVehiclesType.ElectricMotorcycle)
            {
                m_Engine = new ElectricEngine(Registration.sr_AmountOfResources[i_Type]);
            }
            else
            {
                m_Engine = new FuelEngine(Registration.sr_AmountOfResources[i_Type], Registration.sr_ResourceType[i_Type]);
            }

            m_EnergyLeftPercentage = m_AmountOfEnergyLeft / Registration.sr_AmountOfResources[i_Type];
            r_SpecialAttribute = new List<KeyValuePair<string, Type>>(k_AmountOfSpecialAttribute)
                                     {
                                         new KeyValuePair<string, Type>("License type", m_MotorCycleLicenseType.GetType()),
                                         new KeyValuePair<string, Type>("Engine volume", m_EngineVolume.GetType())
                                     };
        }

        public override string ToString()
        {
            StringBuilder stringToReturn = new StringBuilder();
            stringToReturn.AppendFormat("MotorCycle's details:{0}{1}", Environment.NewLine, base.ToString());
            stringToReturn.AppendFormat("The MotorCycle is for {0} license and has engine volume of {1}{2}", m_MotorCycleLicenseType, m_EngineVolume.ToString(), Environment.NewLine);
            return stringToReturn.ToString();
        }

        internal override int GetAmountOfSpecialAttribute()
        {
            return k_AmountOfSpecialAttribute;
        }

        internal override void InsertSpecialAttribute(int i_AttributeIndex, string i_Attribute)
        {
            switch (i_AttributeIndex)
            {
                case 0:
                    bool checkIfIntParsedSuccess;
                    checkIfIntParsedSuccess = int.TryParse(i_Attribute, out int attributeChoose);
                    if (checkIfIntParsedSuccess == false)
                    {
                        throw new ArgumentException();
                    }

                    if (!Enum.IsDefined(typeof(License.eMotorCycleLicenseType), attributeChoose))
                    {
                        throw new ArgumentException();
                    }

                    m_MotorCycleLicenseType = (License.eMotorCycleLicenseType)attributeChoose;
                    break;
                default:
                    bool checkIfParsedFloatSuccess;
                    checkIfParsedFloatSuccess = int.TryParse(i_Attribute, out int intToInsert);
                    if (checkIfParsedFloatSuccess == false)
                    {
                        throw new ArgumentException();
                    }

                    if (intToInsert < 0)
                    {
                        throw new ValueOutOfRangeException(null, intToInsert, "Engine Volume", "Engine volume may not be less than zero");
                    }

                    m_EngineVolume = intToInsert;
                    break;
            }
        }

        internal override KeyValuePair<string, Type> GetCurrentCarKeyValuePairByIndexFromVehicle(int i_Index)
        {
            return r_SpecialAttribute[i_Index];
        }
    }
}
