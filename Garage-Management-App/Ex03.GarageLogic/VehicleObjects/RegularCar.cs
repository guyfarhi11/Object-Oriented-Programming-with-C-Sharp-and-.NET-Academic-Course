using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.EnumClasses;
using Ex03.GarageLogic.GarageManagement;
using Ex03.GarageLogic.InnerVehicleObjects;

namespace Ex03.GarageLogic.VehicleObjects
{
    internal class RegularCar : Vehicle
    {
        private const int k_AmountOfSpecialAttribute = 2;
        private readonly List<KeyValuePair<string, Type>> r_SpecialAttribute;
        private Color.eColorTypeForRegularCar m_ColorTypeForRegularCar;
        private Doors.eAmountOfDoorsForRegularCar m_AmountOfDoors;

        internal RegularCar(string i_ModelName, string i_ModelNumber, string i_WheelManufacturerName, Registration.eVehiclesType i_Type)
       : base(i_ModelName, i_ModelNumber, i_WheelManufacturerName, i_Type)
        {
            if (i_Type == Registration.eVehiclesType.ElectricCar)
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
                                         new KeyValuePair<string, Type>("Color", m_ColorTypeForRegularCar.GetType()),
                                         new KeyValuePair<string, Type>("Amount of doors", m_AmountOfDoors.GetType())
                                     };
        }

        public override string ToString()
        {
            StringBuilder stringToReturn = new StringBuilder();
            stringToReturn.AppendFormat("Car's details:{0}{1}", Environment.NewLine, base.ToString());
            stringToReturn.AppendFormat("The car is {0} and has {1} doors {2}", m_ColorTypeForRegularCar, (int)m_AmountOfDoors, Environment.NewLine);
            return stringToReturn.ToString();
        }

        internal override int GetAmountOfSpecialAttribute()
        {
            return k_AmountOfSpecialAttribute;
        }

        internal override void InsertSpecialAttribute(int i_AttributeIndex, string i_Attribute)
        {
            int attributeChoose;
            bool checkIfIntParsedSuccess;
            switch (i_AttributeIndex)
            {
                case 0:
                    checkIfIntParsedSuccess = int.TryParse(i_Attribute, out attributeChoose);
                    if (checkIfIntParsedSuccess == false)
                    {
                        throw new ArgumentException();
                    }

                    if (!Enum.IsDefined(typeof(Color.eColorTypeForRegularCar), attributeChoose))
                    {
                        throw new ArgumentException();
                    }

                    m_ColorTypeForRegularCar = (Color.eColorTypeForRegularCar)attributeChoose;
                    break;
                default:
                    checkIfIntParsedSuccess = int.TryParse(i_Attribute, out attributeChoose);
                    if (checkIfIntParsedSuccess == false)
                    {
                        throw new ArgumentException();
                    }

                    if (!Enum.IsDefined(typeof(Doors.eAmountOfDoorsForRegularCar), attributeChoose))
                    {
                        throw new ArgumentException();
                    }

                    m_AmountOfDoors = (Doors.eAmountOfDoorsForRegularCar)attributeChoose;
                    break;
            }
        }

        internal override KeyValuePair<string, Type> GetCurrentCarKeyValuePairByIndexFromVehicle(int i_Index)
        {
            return r_SpecialAttribute[i_Index];
        }
    }
}
