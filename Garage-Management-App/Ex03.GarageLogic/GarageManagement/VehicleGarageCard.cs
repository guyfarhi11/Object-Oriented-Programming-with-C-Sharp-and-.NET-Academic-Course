using System;
using System.Text;
using Ex03.GarageLogic.EnumClasses;
using Ex03.GarageLogic.VehicleObjects;

namespace Ex03.GarageLogic.GarageManagement
{
    internal class VehicleGarageCard
    {
        private readonly Vehicle r_Vehicle;
        private readonly string r_VehicleOwner;
        private string m_VehicleOwnerPhone;
        private VehicleStatus.eStatus m_VehicleStatus;
      
        internal VehicleGarageCard(string i_VehicleOwner, string i_VehicleOwnerPhone, Vehicle i_Vehicle)
        {
            r_VehicleOwner = i_VehicleOwner;
            m_VehicleOwnerPhone = i_VehicleOwnerPhone;
            m_VehicleStatus = EnumClasses.VehicleStatus.eStatus.InRepair;
            r_Vehicle = i_Vehicle;
        }

        internal VehicleStatus.eStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        internal Vehicle Vehicle
        {
            get { return r_Vehicle; }
        }

        public override string ToString()
        {
            StringBuilder stringToReturn = new StringBuilder();
            stringToReturn.AppendFormat("Owner name: {0}{2}VehicleStatus {1}{2}", r_VehicleOwner, m_VehicleStatus, Environment.NewLine);
            stringToReturn.AppendLine(r_Vehicle.ToString());
            return stringToReturn.ToString();
        }
    }
}
