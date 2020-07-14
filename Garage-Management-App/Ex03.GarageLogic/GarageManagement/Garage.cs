using System;
using System.Collections.Generic;
using Ex03.GarageLogic.EnumClasses;
using Ex03.GarageLogic.InnerVehicleObjects;
using Ex03.GarageLogic.VehicleObjects;

namespace Ex03.GarageLogic.GarageManagement
{
    public class Garage
    {
        private Dictionary<string, VehicleGarageCard> m_Vehicles;
        private VehicleGarageCard m_CurrentVehicleCardToInsert;

        public Garage()
        {
            m_Vehicles = new Dictionary<string, VehicleGarageCard>();
            m_CurrentVehicleCardToInsert = null;
        }

        public void InsertANewVehicleCardIntoTheGarage(
            string i_OwnerName,
            string i_OwnerCellPhone,
            string i_ModelName,
            string i_LicenseNumber,
            string i_WheelManufacturerName,
            Registration.eVehiclesType i_Type)
        {
            if (this.IsLicenseNumberExist(i_LicenseNumber))
            {
                throw new ArgumentException("Sorry.. you must have a mistake, the license number already exists in our system.");
            }

            VehicleGarageCard cardVehicle = Registration.CreateFirstCard(i_OwnerName, i_OwnerCellPhone, i_ModelName, i_LicenseNumber, i_WheelManufacturerName, i_Type);
            m_Vehicles.Add(i_LicenseNumber, cardVehicle);
            
            m_CurrentVehicleCardToInsert = cardVehicle;
        }

        public string TypeOfCurrentCarEnergy()
        {
            return m_CurrentVehicleCardToInsert.Vehicle.AskForTypeOfFuel();
        }
        
        public void InsertAmountOfEnergyVehicleCard(float i_AmountOfEnergy)
        {
            m_CurrentVehicleCardToInsert.Vehicle.AddResourcesToEngine(i_AmountOfEnergy, Registration.sr_ResourceType[m_CurrentVehicleCardToInsert.Vehicle.Type]);
        }

        public void InsertWheelPressureToCurrentVehicle(float i_WheelPressure)
        {
            m_CurrentVehicleCardToInsert.Vehicle.AddCurrentWheelPressure(i_WheelPressure);
        }

        public List<string> LicenseNumbersByStatus(VehicleStatus.eStatus i_Status)
        {
            List<string> listInfo = new List<string>(m_Vehicles.Count);
            foreach (VehicleGarageCard card in m_Vehicles.Values)
            {
                if(i_Status == card.VehicleStatus)
                {
                    listInfo.Add(card.Vehicle.LicenseNumber);
                }
            }

            return listInfo;
        }

        public bool ChangeStatus(string i_LicenseNumber, VehicleStatus.eStatus i_Status)
        {
            bool changedStatus = m_Vehicles.TryGetValue(i_LicenseNumber, out VehicleGarageCard vehicleCard);
            if(changedStatus)
            {
                vehicleCard.VehicleStatus = i_Status;
            }

            return changedStatus;
        }

        public bool CheckIfCarIsElectricTypeByLicense(string i_LicenseNumber)
        {
            bool boolToReturn = false;
            Vehicle vehicleCheek = getVehicleByLicenseNumber(i_LicenseNumber);
            if (vehicleCheek.Engine is ElectricEngine)
            {
                boolToReturn = true;
            }

            return boolToReturn;
        }

        public KeyValuePair<string, Type> GetCurrentCarKeyValuePairByIndex(int i_Index)
        {
            return m_CurrentVehicleCardToInsert.Vehicle.GetCurrentCarKeyValuePairByIndexFromVehicle(i_Index);
        }
        
        public void InflateWheelsToMaximum(string i_LicenseNumber)
        {
            foreach (Wheel wheel in m_Vehicles[i_LicenseNumber].Vehicle.ListOfWheels)
            {
                wheel.AddCurrentWheelPressure(wheel.MaximalAirPressure - wheel.CurrentAirPressure);
            }
        }

        public void RefuelAFuelBasedVehicle(string i_LicenseNumber, Resources.eResourceType i_FuelType, float i_AmountToFill)
        {
            Vehicle vehicle = getVehicleByLicenseNumber(i_LicenseNumber);
            if (vehicle != null)
            {
                vehicle.AddResourcesToEngine(i_AmountToFill, i_FuelType);
            }
        }

        public void ChargeAnElectricBasedVehicle(string i_LicenseNumber, float i_AmountToFill)
        {
            Vehicle vehicle = getVehicleByLicenseNumber(i_LicenseNumber);
            if (vehicle != null)
            {
                vehicle.AddResourcesToEngine(i_AmountToFill, Resources.eResourceType.Electricity);
            }
        }

        public string GetVehicleData(string i_LicenseNumber)
        {
            Vehicle vehicle = this.getVehicleByLicenseNumber(i_LicenseNumber);
            return vehicle.ToString();
        }

        private Vehicle getVehicleByLicenseNumber(string i_LicenseNumber)
        {
            bool isCardExists = false;
            Vehicle vehicleToReturn = null;
            isCardExists = m_Vehicles.TryGetValue(i_LicenseNumber, out VehicleGarageCard vehicleCard);
            if(isCardExists)
            {
                vehicleToReturn = vehicleCard.Vehicle;
            }

            return vehicleToReturn;
        }

        public bool IsLicenseNumberExist(string i_LicenseNumber)
        {
           return m_Vehicles.ContainsKey(i_LicenseNumber);
        }

        public int GetAmountOfSpecialAttribute()
        {
            return m_CurrentVehicleCardToInsert.Vehicle.GetAmountOfSpecialAttribute();
        }

        public void InsertSpecialAttributeToCurrentCarByIndex(int i_AttributeIndex, string i_Attribute)
        {
            m_CurrentVehicleCardToInsert.Vehicle.InsertSpecialAttribute(i_AttributeIndex, i_Attribute);
        }

        public void MoveCurrentVehicleCardOutOfRegistration()
        {
            m_CurrentVehicleCardToInsert = null;
        }
    }
}