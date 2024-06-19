﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class FuelTank
    {
        private eFuelType m_FuelType;
        private float m_CurrentFuelAmountInLiters;
        private float m_MaxFuelAmountInLiters;

        //public FuelTank(eFuelType i_FuelType, float i_CurrentFuelAmountLiters, float i_MaxFuelAmountLiters)
        //{
        //    m_FuelType = i_FuelType;
        //    m_CurrentFuelAmountLiters = i_CurrentFuelAmountLiters;
        //    m_MaxFuelAmountLiters = i_MaxFuelAmountLiters;
        //}

        public void SetFuelTankValues(eFuelType i_FuelType, float i_CurrentFuelAmountLiters, float i_MaxFuelAmountLiters)
        {
            try
            {
                FuelType = i_FuelType;  // if value is not defined - throws exception
                MaxFuelAmountLiters = i_MaxFuelAmountLiters; // if value is 0 - throws exception
                CurrentFuelAmountLiters = i_CurrentFuelAmountLiters; //if value is bigger than max - throws exception
            }
            catch (Exception)
            {
                throw;
            }   
        }

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
                    throw new ValueOutOfRangeException(0, m_MaxFuelAmountInLiters);
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

        public void SetFuelTankProperty(string i_PropertyName, string i_PropertyValue)
        {
            try
            {
                switch (i_PropertyName)
                {
                    case "Fuel type":
                        {
                            if (Enum.TryParse<eFuelType>(i_PropertyValue, out eFuelType result))
                            {
                                FuelType = result;
                            }
                            else
                            {
                                throw new ArgumentException("Invalid fuel type");
                            }
                            break;
                        }
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
                    case "Max fuel amount in liters":
                        {
                            if (float.TryParse(i_PropertyValue, out float value))
                            {
                                MaxFuelAmountLiters = value;
                            }
                            else
                            {
                                throw new FormatException("Invalid input - Max fuel amount in liters should be a float number");
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

        public List<string> GetListOfPropertiesAndPossibleValues()
        {
            string supportedFuelTypes = string.Join(", ", Enum.GetNames(typeof(eFuelType)));

            List<string> properties = new List<string>
            {
                "Fuel type", $"Supported fuel types are:{supportedFuelTypes}",
                //properties.Add($"Supported fuel types are:{GetListOfSupportedFuelTypes()}");
                "Current fuel amount in liters", "Float positive number",
                "Max fuel amount in liters", "Float positive number"
            };
            return properties;
        }
        private List<string> GetListOfSupportedFuelTypes() //private or public?
        {
            List<string> supportedFuelTypes = new List<string>();
            foreach (eFuelType fuelType in Enum.GetValues(typeof(eFuelType)))
            {
                supportedFuelTypes.Add(fuelType.ToString());
            }

            return supportedFuelTypes;
        }   

        public void Refuel(float i_FuelToAddLiters, eFuelType? i_FuelType)
        {
            if (!i_FuelType.HasValue)
            {
                throw new ArgumentNullException("There is no fuel type");
            }
            
            if (m_FuelType == i_FuelType)
            {
                if (m_CurrentFuelAmountInLiters + i_FuelToAddLiters <= m_MaxFuelAmountLiters)
                {
                    m_CurrentFuelAmountInLiters += i_FuelToAddLiters;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxFuelAmountLiters - m_CurrentFuelAmountInLiters);
                }
            }
            else
            {
                throw new ArgumentException("Wrong fuel type");
            }
        }

        
    }
}
