using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName; //readonly?
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure; //readonly?

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public void InflateWheel(float i_AirPressureToAdd)
        {
            if (m_CurrentAirPressure + i_AirPressureToAdd <= m_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AirPressureToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, m_MaxAirPressure - m_CurrentAirPressure);
            }
        }

        //public void inflateWheelToMax()
        //{
        //    InflateWheel(r_MaxAirPressure - m_CurrentAirPressure);
        //}

        //public List<string> GetListOfProperties()
        //{
        //    List<string> listOfProperties = new List<string>
        //    {
        //        "WheelManufacturerName", //? define by myself?
        //        "CurrentAirPressure",
        //        "MaxAirPressure" //? define by myself?
        //    };
        //    //listOfValues.Add("Current Energy Percentage");
        //    return listOfProperties;
        //}

        public string ManufacturerName { get; set; }    
        //{
        //    get
        //    {
        //        return m_ManufacturerName;
        //    }
        //    set
        //    {
        //        m_ManufacturerName = value;
        //    }
        //}

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (value <= m_MaxAirPressure && value >= 0)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxAirPressure);
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
                    throw new ArgumentException("Invalid max air pressure");
                }
            }
        }

        public override string ToString()
        {
            return $"Manufacturer Name: {r_ManufacturerName}, Current Air Pressure: {m_CurrentAirPressure}, Max Air Pressure: {m_MaxAirPressure}";
        }
    }
}
