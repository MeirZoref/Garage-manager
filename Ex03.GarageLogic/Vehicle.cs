using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        //private List<Wheel> m_WheelsList = new List<Wheel>();
        //private EnergySource m_EnergySource;

        //public Vehicle(string i_ModelName, string i_LicenseNumber, float i_CurrentEnergyPercentage, List<Wheel> i_WheelsList)// EnergySource i_EnergySource)
        //{
        //    r_ModelName = i_ModelName;
        //    r_LicenseNumber = i_LicenseNumber;
        //    m_CurrentEnergyPercentage = i_CurrentEnergyPercentage;
        //    m_WheelsList = i_WheelsList;
        //    //m_EnergySource = i_EnergySource;
        //}

        public void SetVehicleValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList, float i_CurrentEnergyLevel)
        {
            try
            {
                ModelName = i_ModelName;
                LicenseNumber = i_LicenseNumber;
                WheelsList = i_WheelsList; //try catch?
                CurrentEnergyLevel = i_CurrentEnergyLevel; //? calculate?
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

        public void SetWheelsList(List<Wheel> i_WheelsList)
        {
            WheelsList = i_WheelsList;
        }

        public List<Wheel> WheelsList { get; set;} //add try catch?
        //{
        //    get
        //    {
        //        return m_WheelsList;
        //    }
        //    set
        //    {
        //        m_WheelsList = value;
        //    }
        //}


        public virtual List<string> GetListOfProperties()
        {
            List<string> listOfValues = new List<string>
            {
                "Model Name",
                "License Number",
                "Wheels Manufacturer", //? define by myself?
                "Current Air Pressure",
                "Max Air Pressure" //? define by myself?
            };
            //listOfValues.Add("Current Energy Percentage");
            return listOfValues;
        }

        public void InflateWheelsToMax()
        {
            foreach (Wheel wheel in WheelsList)
            {
                wheel.InflateWheel(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public abstract void FillEnergy(float i_EnergyToAdd, eFuelType? i_FuelType);
        

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Model Name: {r_ModelName}");
            sb.AppendLine($"License Number: {r_LicenseNumber}");
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
