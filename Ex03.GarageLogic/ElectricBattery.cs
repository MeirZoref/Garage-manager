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
        private float m_CurrentBatteryEnergyLevelInHours;
        private float m_MaxBatteryEnergyLevelInHours;

        //public ElectricBattery(float i_CurrentEnergy, float i_MaxEnergy)
        //{
        //    m_CurrentBatteryEnergyLevel = i_CurrentEnergy;
        //    m_MaxBatteryEnergyLevel = i_MaxEnergy;
        //}

        //public void SetBatteryValues(float i_CurrentEnergy, float i_MaxEnergy)
        //{
        //    try
        //    {
        //        MaxBatteryEnergyLevel = i_MaxEnergy;
        //        CurrentBatteryEnergyLevel = i_CurrentEnergy;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

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
                    //case "Max battery energy level":
                    //{
                    //    if (float.TryParse(i_Value, out float result))
                    //    {
                    //        MaxBatteryEnergyLevel = result;
                    //    }
                    //    else
                    //    {
                    //        throw new FormatException("Invalid input - Max battery energy level. Only float number is possible");
                    //    }
                    //    break;
                    //}
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
                    throw new ArgumentException("Invalid max battery energy level");
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
                if (value <= m_MaxBatteryEnergyLevelInHours || value < 0)
                {
                    m_CurrentBatteryEnergyLevelInHours = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxBatteryEnergyLevelInHours);
                }
            }
        }

        public List<KeyValuePair<string, string>> GetListOfPropertiesAndPossibleValues()
        {
            List<KeyValuePair<string, string>> propertiesAndValues = new List<KeyValuePair<string, string>>
            {
                //new KeyValuePair<string, string>("Max battery energy level", "Float (positive number)"),
                new KeyValuePair<string, string>("Current battery energy level", "Float (positive number)")
            };
            
            return propertiesAndValues;
        }


        //public List<string> GetListOfPropertiesAndPossibleValues()
        //{
        //    List<string> listOfProperties = new List<string>
        //    {
        //        "Current battery energy level", "Float (positive number)",
        //        "Max battery energy level", "Float (positive number)"
        //    };
        //    return listOfProperties;
        //}

        public bool TryChargeBattery(float i_EnergyToAddInHours)
        {
            bool isEnergyAdded = false;

            if (m_CurrentBatteryEnergyLevelInHours + i_EnergyToAddInHours <= m_MaxBatteryEnergyLevelInHours)
            {
                m_CurrentBatteryEnergyLevelInHours += i_EnergyToAddInHours;
                isEnergyAdded = true;

            }
            else
            {
                throw new ValueOutOfRangeException(0, m_MaxBatteryEnergyLevelInHours - m_CurrentBatteryEnergyLevelInHours);
            }

            return isEnergyAdded;
        }

        //public float CurrentEnergy
        //{
        //    get
        //    {
        //        return m_CurrentBatteryEnergyLevel;
        //    }
        //}

        //public float MaxEnergy
        //{
        //    get
        //    {
        //        return m_MaxBatteryEnergyLevel;
        //    }
        //}

        public override string ToString()
        {
            StringBuilder batteryDetails = new StringBuilder();
            batteryDetails.AppendFormat("Max battery energy level (in hours): {0}{1}", m_MaxBatteryEnergyLevelInHours, Environment.NewLine);
            batteryDetails.AppendFormat("Current battery energy level (in hours): {0}{1}", m_CurrentBatteryEnergyLevelInHours, Environment.NewLine);
            
            return batteryDetails.ToString();
        }
    }

    
}
