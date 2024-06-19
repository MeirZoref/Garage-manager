using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string r_ManufacturerName; //readonly?
        private float m_CurrentAirPressure;
        private float r_MaxAirPressure; //readonly?

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public void InflateWheel(float i_AirPressureToAdd)
        {
            if (m_CurrentAirPressure + i_AirPressureToAdd <= r_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AirPressureToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure - m_CurrentAirPressure);
            }
        }
        
        //public void inflateWheelToMax()
        //{
        //    InflateWheel(r_MaxAirPressure - m_CurrentAirPressure);
        //}

        public string ManufacturerName
        {
            get
            {
                return r_ManufacturerName;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (value <= r_MaxAirPressure && value >= 0)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, r_MaxAirPressure);
                }
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
            set
            {
                if (value > 0)
                {
                    r_MaxAirPressure = value;
                }
                else
                {
                    throw new ArgumentException("Invalid max air pressure");
                }
            }
        }

        public override string ToString()
        {
            return $"Manufacturer Name: {r_ManufacturerName}, Current Air Pressure: {m_CurrentAirPressure}, Max Air Pressure: {r_MaxAirPressure}";
        }
    }
}
