using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private const int k_NumOfWheels = 5;
        private eColor m_Color;
        private eNumOfDoors m_NumOfDoors;

        //public Car(string i_ModelName, string i_LicenseNumber, float i_CurrentEnergyPercentage, List<Wheel> i_WheelsList,
        //    eColor i_Color, eNumOfDoors i_NumOfDoors)
        //    : base(i_ModelName, i_LicenseNumber, i_CurrentEnergyPercentage, i_WheelsList)
        //{
        //    m_Color = i_Color;
        //    m_NumOfDoors = i_NumOfDoors;
        //}

        public void SetCarValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
                             float i_CurrentEnergyLevel, eColor i_Color, eNumOfDoors i_NumOfDoors)
        {
            try
            {
                SetVehicleValues(i_ModelName, i_LicenseNumber, i_WheelsList, i_CurrentEnergyLevel);
                Color = i_Color;
                NumOfDoors = i_NumOfDoors;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override List<string> GetListOfProperties()
        {
            List<string> properties = base.GetListOfProperties();
            properties.Add("Color");
            properties.Add($"Supported colors are: " + string.Join(", ", GetListOfSupportedColors()));
            properties.Add("Number of doors");
            properties.Add("Supported number of doors are:" + string.Join(", ", GetListOfSupportedNumOfDoors()));
            //properties.Add($"Supported number of doors are:{GetListOfSupportedNumOfDoors()}");
            return properties;
        }

        private object GetListOfSupportedNumOfDoors()
        {
            List<string> supportedNumOfDoors = new List<string>();
            foreach (eNumOfDoors numOfDoors in Enum.GetValues(typeof(eNumOfDoors)))
            {
                supportedNumOfDoors.Add(numOfDoors.ToString());
            }

            return supportedNumOfDoors;
        }

        public List<string> GetListOfSupportedColors()
        {
            List<string> supportedColors = new List<string>();
            foreach (eColor color in Enum.GetValues(typeof(eColor)))
            {
                supportedColors.Add(color.ToString());
            }

            return supportedColors;
        }

        public eColor Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                if (Enum.IsDefined(typeof(eColor), value))
                {
                    m_Color = value;
                }
                else
                {
                    throw new ArgumentException("Invalid color value.");
                }
            }
        }

        public eNumOfDoors NumOfDoors
        {
            get
            {
                return m_NumOfDoors;
            }
            set
            {
                if (Enum.IsDefined(typeof(eNumOfDoors), value))
                {
                    m_NumOfDoors = value;
                }
                else
                {
                    throw new ArgumentException("Invalid number of doors value.");
                }
            }
        }

        public int NumOfWheels
        {
            get
            {
                return k_NumOfWheels;
            }
        }

        public override string ToString()
        {
            StringBuilder carDetails = new StringBuilder();
            carDetails.Append(base.ToString());
            carDetails.AppendFormat("Color: {0}{1}", m_Color, Environment.NewLine);
            carDetails.AppendFormat("Number of doors: {0}{1}", m_NumOfDoors, Environment.NewLine);
            return carDetails.ToString();
        }
    }
}
