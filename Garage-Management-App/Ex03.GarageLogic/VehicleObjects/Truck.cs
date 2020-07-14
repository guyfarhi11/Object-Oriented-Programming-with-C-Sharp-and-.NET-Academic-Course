using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.ExceptionsFolder;
using Ex03.GarageLogic.GarageManagement;
using Ex03.GarageLogic.InnerVehicleObjects;

namespace Ex03.GarageLogic.VehicleObjects
{
    internal class Truck : Vehicle
    {
        private const int k_AmountOfSpecialAttribute = 2;
        private readonly List<KeyValuePair<string, Type>> r_SpecialAttribute;
        private bool m_Dangerous;
        private float m_CargoVolume;

        internal Truck(
            string i_ModelName,
            string i_ModelNumber,
            string i_WheelManufacturerName,
            Registration.eVehiclesType i_Type)
            : base(i_ModelName, i_ModelNumber, i_WheelManufacturerName, i_Type)
        {
            m_Engine = new FuelEngine(Registration.sr_AmountOfResources[i_Type], Registration.sr_ResourceType[i_Type]);
            m_EnergyLeftPercentage = m_AmountOfEnergyLeft / Registration.sr_AmountOfResources[i_Type];
            r_SpecialAttribute = new List<KeyValuePair<string, Type>>(k_AmountOfSpecialAttribute);
            r_SpecialAttribute.Add(new KeyValuePair<string, Type>("Dangerous cargo", m_Dangerous.GetType()));
            r_SpecialAttribute.Add(new KeyValuePair<string, Type>("Dangerous cargo", m_CargoVolume.GetType()));
        }

        public override string ToString()
        {
            StringBuilder stringToReturn = new StringBuilder();
            stringToReturn.AppendFormat("Truck's details:{0}{1}", Environment.NewLine, base.ToString());
            stringToReturn.AppendFormat("The truck has cargo volume of {0} ", m_CargoVolume);
            stringToReturn.AppendFormat(
                m_Dangerous == true ? "and has dangerous cargo" : "and doesn't contain dangerous cargo");
            return stringToReturn.ToString();
        }
        
        internal override void InsertSpecialAttribute(int i_AttributeIndex, string i_Attribute)
        {
            switch(i_AttributeIndex)
            {
                case 0:
                    bool boolToInsert; 
                    if (i_Attribute.Equals("Yes") || i_Attribute.Equals("yes"))
                    {
                        boolToInsert = true;
                    }
                    else
                    {
                        if (i_Attribute.Equals("No") || i_Attribute.Equals("no"))
                        {
                            boolToInsert = false;
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }

                    m_Dangerous = boolToInsert;
                    break;
                default:
                    bool checkIfParsedFloatSuccess;
                    checkIfParsedFloatSuccess = float.TryParse(i_Attribute, out float toInsert);
                    if (checkIfParsedFloatSuccess == false)
                    {
                        throw new ArgumentException();
                    }

                    if(toInsert < 0)
                    {
                        throw new ValueOutOfRangeException(null, toInsert, "Cargo volume", "Cargo volume may not be less than zero");
                    }

                    m_CargoVolume = toInsert;
                    break;
            }
        }

        internal override int GetAmountOfSpecialAttribute()
        {
            return k_AmountOfSpecialAttribute;
        }

        internal override KeyValuePair<string, Type> GetCurrentCarKeyValuePairByIndexFromVehicle(int i_Index)
        {
            return r_SpecialAttribute[i_Index];
        }
    }
}
