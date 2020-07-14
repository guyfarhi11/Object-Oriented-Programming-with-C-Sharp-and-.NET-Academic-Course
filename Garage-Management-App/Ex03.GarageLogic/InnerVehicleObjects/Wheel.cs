using System.Text;
using Ex03.GarageLogic.EnumClasses;
using Ex03.GarageLogic.ExceptionsFolder;

namespace Ex03.GarageLogic.InnerVehicleObjects
{
    internal class Wheel
    {
        private readonly AirPressure.eMaximalAirPressure r_MaximalAirPressure;
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;
        
        internal Wheel(string i_ManufacturerName, float i_CurrentAirPressure, AirPressure.eMaximalAirPressure i_MaximalAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaximalAirPressure = i_MaximalAirPressure;
        }
        
        internal void AddCurrentWheelPressure(float i_WheelPressure)
        {
            float maxAirPressure = (int)r_MaximalAirPressure;
            if (i_WheelPressure + m_CurrentAirPressure > maxAirPressure)
            {
                throw new ValueOutOfRangeException(null, i_WheelPressure, "Wheel air pressure", "You were trying to add more air pressure than your maximum air pressure is");
            }
            else if(i_WheelPressure < 0)
            {
                throw new ValueOutOfRangeException(null, i_WheelPressure, "Wheel air pressure", "Wheel pressure may not be negative");
            } 
            else
            {
                m_CurrentAirPressure += i_WheelPressure;
            }
        }

        internal float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        internal float MaximalAirPressure
        {
            get { return (int)r_MaximalAirPressure; }
        }

        public override string ToString()
        {
            StringBuilder stringToReturn = new StringBuilder();
            stringToReturn.AppendFormat("manufacturer name: {0}, current air pressure: {1}", r_ManufacturerName, m_CurrentAirPressure);
            return stringToReturn.ToString();
        }
    }
}
