using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        //public void SetCarValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
        //                     float i_CurrentEnergyLevel, eColor i_Color, eNumOfDoors i_NumOfDoors)
        //{
        //    try
        //    {
        //        SetVehicleValues(i_ModelName, i_LicenseNumber, i_WheelsList, i_CurrentEnergyLevel);
        //        Color = i_Color;
        //        NumOfDoors = i_NumOfDoors;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public override void SetProperty(string i_PropertyName, string i_PropertyValue)
        {
            try
            {
                switch (i_PropertyName)
                {
                    case "Color":
                    {
                        if (Enum.TryParse<eColor>(i_PropertyValue, out eColor result))
                        {
                            Color = result;
                        }
                        else
                        {
                            throw new ArgumentException("Invalid color");
                        }

                        break;
                    }
                    case "Number of doors":
                    {
                        if (Enum.TryParse<eNumOfDoors>(i_PropertyValue, out eNumOfDoors result))
                        {
                            NumOfDoors = result;
                        }
                        else
                        {
                            throw new ArgumentException("Invalid number of doors");
                        }

                        break;
                    }
                    default:
                    {
                        base.SetProperty(i_PropertyName, i_PropertyValue);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override List<KeyValuePair<string, string>> GetListOfPropertiesAndPossibleValues()
        {
            string supportedColors = "Supported colors are:" + string.Join(", ", Enum.GetNames(typeof(eColor)));
            string supportedNumOfDoors = "Supported number of doors are:" + string.Join(", ", Enum.GetNames(typeof(eNumOfDoors)));
            List<KeyValuePair<string, string>> propertiesAndValues = new List<KeyValuePair<string, string>> { };

            propertiesAndValues.AddRange(base.GetListOfPropertiesAndPossibleValues());
            propertiesAndValues.Add(new KeyValuePair<string, string>("Color", supportedColors));
            propertiesAndValues.Add(new KeyValuePair<string, string>("Number of doors", supportedNumOfDoors));

            return propertiesAndValues;
        }

        //public override OrderedDictionary GetListOfPropertiesAndPossibleValues()
        //{
        //    OrderedDictionary propertiesAndValues = new OrderedDictionary



        //    {
        //        { "Color", "Supported colors are: " + string.Join(", ", Enum.GetNames(typeof(eColor))) },
        //        { "Number of doors", "Supported number of doors are: " + string.Join(", ", Enum.GetNames(typeof(eNumOfDoors))) }
        //    }
        //        base.GetListOfPropertiesAndPossibleValues();
        //    {
        //        { "Model name", "String (any set of characters)" },
        //        { "License number", "String (any set of characters)" },
        //        { "Wheel manufacturer name", "String (any set of characters)" },
        //        { "Current air pressure", "Float (positive number)" },
        //        { "Max air pressure", "Float (positive number)" }
        //    };

        //    return propertiesAndValues;
        //}

        //public override List<string> GetListOfPropertiesAndPossibleValues()
        //{
        //    string supportedColors = "Supported colors are:" + string.Join(", ", Enum.GetNames(typeof(eColor)));
        //    string supportedNumOfDoors = "Supported number of doors are:" + string.Join(", ", Enum.GetNames(typeof(eNumOfDoors)));

        //    List<string> properties = base.GetListOfPropertiesAndPossibleValues();
        //    properties.Add("Color");
        //    properties.Add(supportedColors);
        //    properties.Add("Number of doors");
        //    properties.Add(supportedNumOfDoors);
        //    return properties;
        //}

        //private object GetListOfSupportedNumOfDoors()
        //{
        //    List<string> supportedNumOfDoors = new List<string>();
        //    foreach (eNumOfDoors numOfDoors in Enum.GetValues(typeof(eNumOfDoors)))
        //    {
        //        supportedNumOfDoors.Add(numOfDoors.ToString());
        //    }

        //    return supportedNumOfDoors;
        //}

        //public List<string> GetListOfSupportedColors()
        //{
        //    List<string> supportedColors = new List<string>();
        //    foreach (eColor color in Enum.GetValues(typeof(eColor)))
        //    {
        //        supportedColors.Add(color.ToString());
        //    }

        //    return supportedColors;
        //}

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
