using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public string ManufacturerName { get; set; }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (value >= 0 && value <= m_MaxAirPressure)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new ValueOutOfRangeException("Current air pressure", 0, m_MaxAirPressure);
                }
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
            set
            {
                if (value > 0)
                {
                    m_MaxAirPressure = value;
                }
                else
                {
                    throw new ArgumentException("Invalid max air pressure, must be greater than 0");    
                }
            }
        }

        public void InflateWheel(float i_AirPressureToAdd)
        {
            if (m_CurrentAirPressure + i_AirPressureToAdd <= m_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AirPressureToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException("Air pressure to add", 0, m_MaxAirPressure - m_CurrentAirPressure);
            }
        }

        public override string ToString()
        {
            return $"Manufacturer Name: {ManufacturerName}, Current Air Pressure: {m_CurrentAirPressure}, Max Air Pressure: {m_MaxAirPressure}";
        }
    }
}
