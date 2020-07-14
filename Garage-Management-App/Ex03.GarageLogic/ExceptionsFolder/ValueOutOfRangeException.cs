using System;

namespace Ex03.GarageLogic.ExceptionsFolder
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_Value;
        private string m_WhereTryingToAdd;
        private Exception m_InnerException;

        public ValueOutOfRangeException(Exception i_InnerException, float i_AmountOfEnergy, string i_WhereTryingToAdd, string i_Error) :
            base(i_Error)
        {
            m_Value = i_AmountOfEnergy;
            m_WhereTryingToAdd = i_WhereTryingToAdd;
            m_InnerException = i_InnerException;
        }
    }
}
