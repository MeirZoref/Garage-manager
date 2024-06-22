using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricBattery
    {
        private float m_CurrentBatteryEnergyLevelInHours;
        private float m_MaxBatteryEnergyLevelInHours;

        public float MaxBatteryEnergyLevel
        {
            get
            {
                return m_MaxBatteryEnergyLevelInHours;
            }
            set
            {
                if (value > 0)
                {
                    m_MaxBatteryEnergyLevelInHours = value;
                }
                else
                {
                    throw new ArgumentException("Invalid max battery energy level, must be greater than 0");
                }
            }
        }

        public float CurrentBatteryEnergyLevel
        {
            get
            {
                return m_CurrentBatteryEnergyLevelInHours;
            }
            set
            {
                if (value >= 0 && value <= m_MaxBatteryEnergyLevelInHours)
                {
                    m_CurrentBatteryEnergyLevelInHours = value;
                }
                else
                {
                    throw new ValueOutOfRangeException("Battery energy level", 0, m_MaxBatteryEnergyLevelInHours);
                }
            }
        }

        public void SetBatteryProperty(string i_PropertyName, string i_Value)
        {
            try
            {
                switch (i_PropertyName)
                {
                    case "Current battery energy level":
                    {
                        if (float.TryParse(i_Value, out float result))
                        {
                            CurrentBatteryEnergyLevel = result;
                        }
                        else
                        {
                            throw new FormatException("Invalid input - Current battery energy level. Only float number is possible");
                        }
                            
                        break;
                    }
                    default:
                    {
                        throw new ArgumentException("Invalid property name");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }   

        public List<KeyValuePair<string, string>> GetListOfPropertiesAndPossibleValues()
        {
            List<KeyValuePair<string, string>> propertiesAndValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Current battery energy level", "Float (positive number)")
            };
            
            return propertiesAndValues;
        }

        public bool TryChargeBattery(float i_EnergyToAddInHours)
        {
            bool isEnergyAdded = false;

            if (i_EnergyToAddInHours <= 0)
            {
                throw new ValueOutOfRangeException("Energy to add", m_MaxBatteryEnergyLevelInHours - m_CurrentBatteryEnergyLevelInHours);
            }

            if (m_CurrentBatteryEnergyLevelInHours + i_EnergyToAddInHours <= m_MaxBatteryEnergyLevelInHours)
            {
                m_CurrentBatteryEnergyLevelInHours += i_EnergyToAddInHours;
                isEnergyAdded = true;

            }
            else
            {
                throw new ValueOutOfRangeException("Energy to add", m_MaxBatteryEnergyLevelInHours - m_CurrentBatteryEnergyLevelInHours);
            }

            return isEnergyAdded;
        }

        public override string ToString()
        {
            StringBuilder batteryDetails = new StringBuilder();

            batteryDetails.AppendFormat("Max battery energy level (in hours): {0}{1}", m_MaxBatteryEnergyLevelInHours, Environment.NewLine);
            batteryDetails.AppendFormat("Current battery energy level (in hours): {0}{1}", m_CurrentBatteryEnergyLevelInHours, Environment.NewLine);
            
            return batteryDetails.ToString();
        }
    }

    
}
