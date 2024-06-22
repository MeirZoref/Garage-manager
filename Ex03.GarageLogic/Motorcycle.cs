using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                if (Enum.IsDefined(typeof(eLicenseType), value))
                {
                    m_LicenseType = value;
                }
                else
                {
                    throw new ArgumentException("License type is not valid.");
                }
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }

            set
            {
               if (value > 0)
               {
                        m_EngineVolume = value;
               }
               else
               {
                    throw new ArgumentException("Engine volume must be a positive integer number");
               }
            }
        }

        public override void SetProperty(string i_PropertyName, string i_PropertyValue)
        {
            try
            {
                switch (i_PropertyName)
                {
                    case "License type":
                        {
                            if (Enum.TryParse<eLicenseType>(i_PropertyValue, out eLicenseType result))
                            {
                                LicenseType = result;
                            }
                            else
                            {
                                throw new ArgumentException("License type is not valid.");
                            }
                            
                            break;
                        }
                    case "Engine volume":
                    {
                        if (int.TryParse(i_PropertyValue, out int value) && value > 0)
                        {
                            EngineVolume = value;
                        }
                        else
                        {
                            throw new ArgumentException("Engine volume must be a positive integer number");
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
                        
            string supportedLicenseTypesList = buildListOfSupportedLicenseTypes();
            string supportedLicenseTypesString = $"Supported license types: {Environment.NewLine}{supportedLicenseTypesList}";
            propertiesAndValues.Add(new KeyValuePair<string, string>("License type", supportedLicenseTypesString));
            propertiesAndValues.Add(new KeyValuePair<string, string>("Engine volume", "Positive integer number"));

            return propertiesAndValues;
        }

        private string buildListOfSupportedLicenseTypes()
        {
            string[] licenseTypes = Enum.GetNames(typeof(eLicenseType));
            StringBuilder supportedLicenseTypesBuilder = new StringBuilder();
            for (int i = 0; i < licenseTypes.Length; i++)
            {
                supportedLicenseTypesBuilder.AppendLine($"{i + 1}. {licenseTypes[i]}");
            }
            
            return supportedLicenseTypesBuilder.ToString();
        }

        public override string ToString()
        {
            StringBuilder motorcycleDetails = new StringBuilder();
            motorcycleDetails.Append(base.ToString());
            motorcycleDetails.AppendLine(string.Format("License type: {0}", m_LicenseType));
            motorcycleDetails.AppendLine(string.Format("Engine volume: {0}", m_EngineVolume));
            return motorcycleDetails.ToString();
        }
    }
}
