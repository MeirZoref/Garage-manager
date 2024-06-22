using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class FuelTank
    {
        private eFuelType m_FuelType;
        private float m_CurrentFuelAmountInLiters;
        private float m_MaxFuelAmountInLiters;

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
            set 
            { 
                if (Enum.IsDefined(typeof(eFuelType), value))
                {
                    m_FuelType = value;
                }
                else
                {
                    throw new ArgumentException("Invalid fuel type");
                }
            }
        }

        public float CurrentFuelAmountLiters
        {
            get
            {
                return m_CurrentFuelAmountInLiters;
            }
            set
            {
                if (value >= 0 && value <= m_MaxFuelAmountInLiters)
                {
                    m_CurrentFuelAmountInLiters = value;
                }
                else
                {
                    throw new ValueOutOfRangeException( "Current fuel amount in liters", 0, m_MaxFuelAmountInLiters);
                }
            }
        }

        public float MaxFuelAmountLiters
        {
            get
            {
                return m_MaxFuelAmountInLiters;
            }
            set
            {
                if (value > 0)
                {
                    m_MaxFuelAmountInLiters = value;
                }
                else
                {
                    throw new ArgumentException("Invalid max fuel amount - must be a positive float number");
                }
            }
        }

        public void SetCurrentFuelAmountInLiters(string i_PropertyName, string i_PropertyValue)
        {
            try
            {
                switch (i_PropertyName)
                {
                    case "Current fuel amount in liters": 
                        {

                            if (float.TryParse(i_PropertyValue, out float value))
                            {
                                CurrentFuelAmountLiters = value;
                            }
                            else
                            {
                                throw new FormatException("Invalid input - Current fuel amount in liters should be a float number");
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

        public List<KeyValuePair<string, string>> GetListOfPropertiesAndPossibleValues()
        {
            string supportedFuelTypesString = BuildListOfSupportedFuelTypes();

            List<KeyValuePair<string, string>> propertiesAndValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Current fuel amount in liters", "Float positive number")
            };

            return propertiesAndValues;
        }

        private string BuildListOfSupportedFuelTypes()
        {
            string[] fuelTypes = Enum.GetNames(typeof(eFuelType));
            StringBuilder supportedFuelTypesBuilder = new StringBuilder();
            for (int i = 0; i < fuelTypes.Length; i++)
            {
                supportedFuelTypesBuilder.AppendLine($"{i + 1}. {fuelTypes[i]}");
            }
            
            return supportedFuelTypesBuilder.ToString();
        } 

        public bool TryRefuel(float i_FuelToAddLiters, eFuelType? i_FuelType)
        {
            if (!i_FuelType.HasValue)
            {
                throw new ArgumentNullException("There is no fuel type");
            }

            if (i_FuelToAddLiters <= 0)
            {
                throw new ValueOutOfRangeException("Fuel to add", m_MaxFuelAmountInLiters - m_CurrentFuelAmountInLiters);
            }

            if (m_FuelType == i_FuelType)
            {
                bool isFuelAdded = false;

                if (m_CurrentFuelAmountInLiters + i_FuelToAddLiters <= m_MaxFuelAmountInLiters)
                {
                    m_CurrentFuelAmountInLiters += i_FuelToAddLiters;
                    isFuelAdded = true;
                }
                else
                {
                    throw new ValueOutOfRangeException("Fuel to add", m_MaxFuelAmountInLiters - m_CurrentFuelAmountInLiters);
                }

                return isFuelAdded;
            }
            else
            {
                throw new ArgumentException("Wrong fuel type");
            }
        }

        public override string ToString()
        {
            StringBuilder fuelTankDetails = new StringBuilder();
            fuelTankDetails.AppendFormat("Fuel type: {0}{1}", FuelType, Environment.NewLine);
            fuelTankDetails.AppendFormat("Max fuel amount (in liters): {0}{1}", MaxFuelAmountLiters, Environment.NewLine);
            fuelTankDetails.AppendFormat("Current fuel amount (in liters): {0}{1}", CurrentFuelAmountLiters, Environment.NewLine);

            return fuelTankDetails.ToString();
        }
    }
}
