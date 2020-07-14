using System;
using System.Collections.Generic;
using Ex03.GarageLogic.EnumClasses;
using Ex03.GarageLogic.VehicleObjects;

namespace Ex03.GarageLogic.GarageManagement
{
    public static class Registration
    {
        internal static readonly Dictionary<ValueType, Resources.eResourceType> sr_ResourceType =
            new Dictionary<ValueType, Resources.eResourceType>()
                {
                    { eVehiclesType.ElectricCar, Resources.eResourceType.Electricity },
                    { eVehiclesType.ElectricMotorcycle, Resources.eResourceType.Electricity },
                    { eVehiclesType.FuelBasedCar, Resources.eResourceType.Octan96 },
                    { eVehiclesType.FuelBasedMotorcycle, Resources.eResourceType.Octan95 },
                    { eVehiclesType.FuelBasedTruck, Resources.eResourceType.Soler }
                };

        internal static readonly Dictionary<ValueType, AirPressure.eMaximalAirPressure> sr_MaximalAirPressure =
            new Dictionary<ValueType, AirPressure.eMaximalAirPressure>()
                {
                    { eVehiclesType.ElectricCar, AirPressure.eMaximalAirPressure.ThirtyTwo },
                    { eVehiclesType.ElectricMotorcycle, AirPressure.eMaximalAirPressure.Thirty },
                    { eVehiclesType.FuelBasedCar, AirPressure.eMaximalAirPressure.ThirtyTwo },
                    { eVehiclesType.FuelBasedMotorcycle, AirPressure.eMaximalAirPressure.ThirtyTwo },
                    { eVehiclesType.FuelBasedTruck, AirPressure.eMaximalAirPressure.TwentyEight }
                };

        internal static readonly Dictionary<ValueType, float> sr_AmountOfResources =
            new Dictionary<ValueType, float>()
                {
                    { eVehiclesType.ElectricCar, 2.1f },
                    { eVehiclesType.ElectricMotorcycle, 1.2f },
                    { eVehiclesType.FuelBasedCar, 60f },
                    { eVehiclesType.FuelBasedMotorcycle, 7f },
                    { eVehiclesType.FuelBasedTruck, 120f }
                };

        internal static readonly Dictionary<ValueType, int> sr_NumberOfWheels =
            new Dictionary<ValueType, int>()
                {
                    { eVehiclesType.ElectricCar, 4 },
                    { eVehiclesType.ElectricMotorcycle, 2 },
                    { eVehiclesType.FuelBasedCar, 4 },
                    { eVehiclesType.FuelBasedMotorcycle, 2 },
                    { eVehiclesType.FuelBasedTruck, 16 }
                };

        internal static VehicleGarageCard CreateFirstCard(
            string i_OwnerName,
            string i_OwnerPhone,
            string i_ModelName,
            string i_LicenseNumber,
            string i_WheelManufacturerName,
            eVehiclesType i_VehicleType)
        {
        Vehicle toReturn;
            switch (i_VehicleType)
            {
                case eVehiclesType.ElectricMotorcycle:
                case eVehiclesType.FuelBasedMotorcycle:
                    toReturn = new MotorCycle(i_ModelName, i_LicenseNumber, i_WheelManufacturerName, i_VehicleType);
                    break;
                case eVehiclesType.ElectricCar:
                case eVehiclesType.FuelBasedCar:
                    toReturn = new RegularCar(i_ModelName, i_LicenseNumber, i_WheelManufacturerName, i_VehicleType);
                    break;
                default:
                    toReturn = new Truck(i_ModelName, i_LicenseNumber, i_WheelManufacturerName, i_VehicleType);
                    break;
            }

            VehicleGarageCard cardToReturn = new VehicleGarageCard(i_OwnerName, i_OwnerPhone, toReturn);
            return cardToReturn;
        }

        public enum eVehiclesType
        {
            FuelBasedMotorcycle,
            ElectricMotorcycle,
            FuelBasedCar,
            ElectricCar,
            FuelBasedTruck
        }
    }
}
