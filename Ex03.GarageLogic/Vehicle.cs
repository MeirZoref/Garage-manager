using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        //private string m_ModelName; //readonly?
        //private string m_LicenseNumber; //readonly?
        private float m_CurrentEnergyLevel;
        ////private eEnergyType m_EnergyType;
        private List<Wheel> m_WheelsList = new List<Wheel>();
        bool? m_IsElectric = null;
        eFuelType? m_FuelType = null;
        //private EnergySource m_EnergySource;

        //public Vehicle(string i_ModelName, string i_LicenseNumber, float i_CurrentEnergyPercentage, List<Wheel> i_WheelsList)// EnergySource i_EnergySource)
        //{
        //    r_ModelName = i_ModelName;
        //    r_LicenseNumber = i_LicenseNumber;
        //    m_CurrentEnergyPercentage = i_CurrentEnergyPercentage;
        //    m_WheelsList = i_WheelsList;
        //    //m_EnergySource = i_EnergySource;
        //}

        public bool? IsElectric
        {
            get
            {
                if (!m_IsElectric.HasValue)
                {
                    throw new ArgumentException("Energy type of the vehicle is not defined");
                }
                else
                {
                    return m_IsElectric;
                }
            }

            set
            {
                m_IsElectric = value;
            }
        }
        public eFuelType? VehicleFuelType
        {
            get
            {
                if (!m_IsElectric.HasValue)
                {
                    throw new ArgumentException("Energy type of the vehicle is not defined");
                }
                else if (IsElectric == true)
                {
                    throw new ArgumentException("This vehicle is electric, it does not have a fuel type");
                }
                else
                {
                    return m_FuelType;
                }

            }
            set
            {
                if (IsElectric == null)
                {
                    throw new ArgumentException("Electricity type is not defined");
                }
                else if (IsElectric == true)
                {
                    throw new ArgumentException("This vehicle is electric, it does not have a fuel type");
                }
                else
                {
                    m_FuelType = value;
                }

            }

        }

        public virtual void SetProperty(string i_propertyName, string i_PropertyValue)
        {
            try
            {
                switch (i_propertyName)
                {
                    case "Model name":
                    {
                        ModelName = i_PropertyValue;
                        break;
                    }
                    //case "License number":
                    //{
                    //    LicenseNumber = i_PropertyValue;
                    //    break;
                    //}
                    case "Wheel manufacturer name":
                    {
                        foreach (Wheel wheel in WheelsList)
                        {
                            wheel.ManufacturerName = i_PropertyValue;
                        }

                        break;
                    }
                    case "Current air pressure":
                    {
                        if (float.TryParse(i_PropertyValue, out float value))
                        {
                            foreach (Wheel wheel in WheelsList)
                            {
                                wheel.CurrentAirPressure = value;
                            }
                        }
                        else
                        {
                            throw new FormatException("Invalid input - Current air pressure should be a float number");
                        }

                        break;
                    }
                    case "Max air pressure":
                    {
                        if (float.TryParse(i_PropertyValue, out float value))
                        {
                            foreach (Wheel wheel in WheelsList)
                            {
                                wheel.MaxAirPressure = value;
                            }
                        }
                        else
                        {
                            throw new FormatException("Invalid input - Max air pressure should be a float number");
                        }

                        break;
                    }
                    default:
                        throw new ArgumentException("Property name is not valid");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public virtual void SetAllValues (string i_ModelName, string i_LicenseNumber, float i_CurrentEnergyLevel, List<Wheel> i_WheelsList)
        //{
        //    ModelName = i_ModelName;
        //    LicenseNumber = i_LicenseNumber;
        //    m_CurrentEnergyLevel = i_CurrentEnergyLevel; //? calculate?
        //    WheelsList = i_WheelsList;
        //}

        public string ModelName { get; set; }
        //{
        //    get
        //    {
        //        return m_ModelName;
        //    }
        //    set
        //    {
        //           m_ModelName = value;
        //    }
        //}
        public string LicenseNumber { get; set;}
        //{
        //    get
        //    {
        //        return m_LicenseNumber;
        //    }
        //    set
        //    {
        //        m_LicenseNumber = value;
        //    }
        //}

        public float CurrentEnergyLevel
        {
            get
            {
                return m_CurrentEnergyLevel;
            }
            set
            {
                if (value >= 0)
                {         
                    m_CurrentEnergyLevel = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, 100); //???
                }
            }
        }

        //public void SetWheelsList(List<Wheel> i_WheelsList)
        //{
        //    WheelsList = i_WheelsList;
        //}

        public List<Wheel> WheelsList //{ get; set;} //add try catch?
        {
            get
            {
                return m_WheelsList;
            }
            //set
            //{
            //    m_WheelsList = value;
            //}
        }

        public virtual List<KeyValuePair<string, string>> GetListOfPropertiesAndPossibleValues()
        {
            List<KeyValuePair<string, string>> propertiesAndValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Model name", "String (any set of characters)"),
               //new KeyValuePair<string, string>("License number", "String (any set of characters)"),
                new KeyValuePair<string, string>("Wheel manufacturer name", "String (any set of characters)"),
                new KeyValuePair<string, string>("Max air pressure", "Float (positive number)"),
                new KeyValuePair<string, string>("Current air pressure", "Float (positive number)"),
            };

            return propertiesAndValues;
        }



        //public virtual OrderedDictionary GetListOfPropertiesAndPossibleValues()
        //{
        //    OrderedDictionary propertiesAndValues = new OrderedDictionary
        //    {
        //        { "Model name", "String (any set of characters)" },
        //        { "License number", "String (any set of characters)" },
        //        { "Wheel manufacturer name", "String (any set of characters)" },
        //        { "Current air pressure", "Float (positive number)" },
        //        { "Max air pressure", "Float (positive number)" }
        //    };

        //    return propertiesAndValues;
        //}

        //public virtual List<string> GetListOfPropertiesAndPossibleValues()
        //{
        //    List<string> listOfProperties = new List<string>
        //    {
        //        "Model name", "String (any set of charcters)",
        //        "License number", "String (any set of charcters)",
        //        "Wheel manufacturer name", "String (any set of charcters)",
        //        "Current air pressure", "Float (positive number)",
        //        "Max air pressure", "Float (positive number)"
        //    };
        //    //listOfProperties.AddRange(WheelsList.GetListOfProperties());

        //    //listOfValues.Add("Current Energy Percentage");
        //    return listOfProperties;
        //}

        public bool InflateWheelsToMax()
        {
            bool allWhellsAreAlreadyInflatedToMax = true;

            foreach (Wheel wheel in WheelsList)
            {
                if (wheel.CurrentAirPressure < wheel.MaxAirPressure)
                {
                    wheel.InflateWheel(wheel.MaxAirPressure - wheel.CurrentAirPressure);
                    allWhellsAreAlreadyInflatedToMax = false;
                }
            }

            return allWhellsAreAlreadyInflatedToMax;
        }

        public abstract bool TryFillEnergy(float i_EnergyToAdd, eFuelType? i_FuelType);
        

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Model Name: {ModelName}");
            sb.AppendLine($"License Number: {LicenseNumber}");
            //sb.AppendLine($"Current Energy Percentage: {m_CurrentEnergyPercentage}%");
            sb.AppendLine("Wheels:");
            foreach (Wheel wheel in m_WheelsList)
            {
                sb.AppendLine($"- {wheel.ToString()}");
            }
            return sb.ToString();
        }
    }
}
