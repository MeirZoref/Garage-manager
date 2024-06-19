using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class FuelTank
    {
        private eFuelType m_FuelType;
        private float m_CurrentFuelAmountLiters;
        private float m_MaxFuelAmountLiters;

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
                return m_CurrentFuelAmountLiters;
            }
            set
            {
                if (value >= 0 && value <= m_MaxFuelAmountLiters)
                {
                    m_CurrentFuelAmountLiters = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxFuelAmountLiters);
                }
            }
        }

        public float MaxFuelAmountLiters
        {
            get
            {
                return m_MaxFuelAmountLiters;
            }
            set
            {
                if (value > 0)
                {
                    m_MaxFuelAmountLiters = value;
                }
                else
                {
                    throw new ArgumentException("Invalid max fuel amount");
                }
            }
        }



        public List<string> GetListOfProperties()
        {
            List<string> properties = new List<string>();
            properties.Add("Fuel type");
            properties.Add($"Supported fuel types are:{GetListOfSupportedFuelTypes()}");
            properties.Add("Current fuel amount");
            properties.Add("Max fuel amount");
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
                if (m_CurrentFuelAmountLiters + i_FuelToAddLiters <= m_MaxFuelAmountLiters)
                {
                    m_CurrentFuelAmountLiters += i_FuelToAddLiters;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxFuelAmountLiters - m_CurrentFuelAmountLiters);
                }
            }
            else
            {
                throw new ArgumentException("Wrong fuel type");
            }
        }

        
    }
}
