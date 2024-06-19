using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricBattery
    {
        private float m_CurrentBatteryEnergyLevel;
        private float m_MaxBatteryEnergyLevel;

        //public ElectricBattery(float i_CurrentEnergy, float i_MaxEnergy)
        //{
        //    m_CurrentBatteryEnergyLevel = i_CurrentEnergy;
        //    m_MaxBatteryEnergyLevel = i_MaxEnergy;
        //}

        public void SetBatteryValues(float i_CurrentEnergy, float i_MaxEnergy)
        {
            try
            {
                MaxBatteryEnergyLevel = i_MaxEnergy;
                CurrentBatteryEnergyLevel = i_CurrentEnergy;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public float MaxBatteryEnergyLevel
        {
            get
            {
                return m_MaxBatteryEnergyLevel;
            }
            set
            {
                if (value > 0)
                {
                    m_MaxBatteryEnergyLevel = value;
                }
                else
                {
                    throw new ArgumentException("Invalid max battery energy level");
                }
            }
        }

        public float CurrentBatteryEnergyLevel
        {
            get
            {
                return m_CurrentBatteryEnergyLevel;
            }
            set
            {
                if (value <= m_MaxBatteryEnergyLevel || value < 0)
                {
                    m_CurrentBatteryEnergyLevel = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxBatteryEnergyLevel);
                }
            }
        }

        public List<string> GetListOfProperties()
        {
            List<string> listOfProperties = new List<string>
            {
                "Current battery energy level",
                "Max battery energy level"
            };
            return listOfProperties;
        }

        public void ChargeBattery(float i_EnergyToAdd)
        {
            if (m_CurrentBatteryEnergyLevel + i_EnergyToAdd <= m_MaxBatteryEnergyLevel)
            {
                m_CurrentBatteryEnergyLevel += i_EnergyToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, m_MaxBatteryEnergyLevel - m_CurrentBatteryEnergyLevel);
            }
        }

        public float CurrentEnergy
        {
            get
            {
                return m_CurrentBatteryEnergyLevel;
            }
        }

        public float MaxEnergy
        {
            get
            {
                return m_MaxBatteryEnergyLevel;
            }
        }

        public override string ToString()
        {
            StringBuilder batteryDetails = new StringBuilder();
            batteryDetails.AppendFormat("Current battery energy level: {0}{1}", m_CurrentBatteryEnergyLevel, Environment.NewLine);
            batteryDetails.AppendFormat("Max battery energy level: {0}{1}", m_MaxBatteryEnergyLevel, Environment.NewLine);
            return batteryDetails.ToString();
        }
    }

    
}
