using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private eColor m_Color;
        private eNumOfDoors m_NumOfDoors;

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
                    throw new ArgumentException("Invalid number of doors.");
                }
            }
        }

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
            List<KeyValuePair<string, string>> propertiesAndValues = base.GetListOfPropertiesAndPossibleValues();

            string supportedColorsString = $"Supported colors are: {Environment.NewLine}{buildListOfSupportedColors()}";
            propertiesAndValues.Add(new KeyValuePair<string, string>("Color", supportedColorsString));
            string supportedNumOfDoors = "Supported number of doors are:" + string.Join(", ", Enum.GetNames(typeof(eNumOfDoors)));
            propertiesAndValues.Add(new KeyValuePair<string, string>("Number of doors", supportedNumOfDoors));

            return propertiesAndValues;
        }

        private string buildListOfSupportedColors()
        {
            string[] possibleColors = Enum.GetNames(typeof(eColor));
            StringBuilder supportedColorsBuilder = new StringBuilder();

            for (int i = 0; i < possibleColors.Length; i++)
            {
                supportedColorsBuilder.AppendLine($"{i + 1}. {possibleColors[i]}");
            }
            
            return supportedColorsBuilder.ToString();
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
