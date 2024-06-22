using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private float m_CurrentEnergyLevel;
        private List<Wheel> m_WheelsList = new List<Wheel>();
        bool? m_IsElectric = null;
        eFuelType? m_FuelType = null;

        public string ModelName { get; set; }
        public string LicenseNumber { get; set; }
        public List<Wheel> WheelsList
        {
            get
            {
                return m_WheelsList;
            }
        }

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
                    throw new ArgumentException("Energy type is not defined");
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
        
        public float CurrentEnergyLevelInPercentage
        {
            get
            {
                return m_CurrentEnergyLevel;
            }
            set
            {
                if (value >= 0)
                {         
                    m_CurrentEnergyLevel = value * 100;
                }
                else
                {
                    throw new ValueOutOfRangeException ("Current energy level in percentage", 0, 100);
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
                    default:
                    {
                        throw new ArgumentException("Property name is not valid");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual List<KeyValuePair<string, string>> GetListOfPropertiesAndPossibleValues()
        {
            List<KeyValuePair<string, string>> propertiesAndValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Model name", "String (any set of characters)"),
                new KeyValuePair<string, string>("Wheel manufacturer name", "String (any set of characters)"),
                new KeyValuePair<string, string>("Current air pressure", "Float (positive number)"),
            };

            return propertiesAndValues;
        }

        public abstract bool TryFillEnergy(float i_EnergyToAdd, eFuelType? i_FuelType);
        
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

        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();
            vehicleDetails.AppendLine($"Model Name: {ModelName}");
            vehicleDetails.AppendLine($"License Number: {LicenseNumber}");
            vehicleDetails.AppendLine($"Current Energy Level: {CurrentEnergyLevelInPercentage}%");
            vehicleDetails.AppendLine($"There are {m_WheelsList.Count} wheels:");
            int wheelIndex = 1;
            foreach (Wheel wheel in m_WheelsList)
            {
                vehicleDetails.AppendLine($"{wheelIndex}. {wheel.ToString()}");
                wheelIndex++;
            }

            return vehicleDetails.ToString();
        }
    }
}
