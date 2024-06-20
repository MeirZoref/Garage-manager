using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        private const int k_NumberOfWheels = 2;
        //private const float k_MaxAirPressure = 30;
        private eLicenseType m_LicenseType;
        private int r_EngineVolume; //readonly?

        //public Motorcycle(string i_ModelName, string i_LicenseNumber, float i_CurrentEnergyPercentage,
        //    List<Wheel> i_WheelsList, eLicenseType i_LicenseType, int i_EngineVolume)
        //    : base(i_ModelName, i_LicenseNumber, i_CurrentEnergyPercentage, i_WheelsList)
        //{
        //    m_LicenseType = i_LicenseType;
        //    r_EngineVolume = i_EngineVolume;
        //}

        //public void SetMotorcycleValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
        //               float i_CurrentEnergyLevel, eLicenseType i_LicenseType, int i_EngineVolume)
        //{
        //    try
        //    {
        //        SetVehicleValues(i_ModelName, i_LicenseNumber, i_WheelsList, i_CurrentEnergyLevel);
        //        LicenseType = i_LicenseType;
        //        EngineVolume = i_EngineVolume;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

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
                return r_EngineVolume;
            }

            set
            {
               if (value > 0)
               {
                        r_EngineVolume = value;
               }
               else
               {
                    throw new ArgumentException(ModelName + " Engine volume must be a positive integer number");
               }
            }
        }

        public int NumberOfWheels
        {
            get
            {
                return k_NumberOfWheels;
            }
        }

        public override List<KeyValuePair<string, string>> GetListOfPropertiesAndPossibleValues()
        {
            List<KeyValuePair<string, string>> propertiesAndValues = base.GetListOfPropertiesAndPossibleValues();
            string supportedLicenseTypes = "Supported license types:" + string.Join(", ", Enum.GetNames(typeof(eLicenseType)));
            propertiesAndValues.Add(new KeyValuePair<string, string>("License type", supportedLicenseTypes));
            propertiesAndValues.Add(new KeyValuePair<string, string>("Engine volume", "Positive integer number"));

            return propertiesAndValues;
        }

        //public override List<string> GetListOfPropertiesAndPossibleValues()
        //{
        //    List<string> properties = base.GetListOfPropertiesAndPossibleValues();
        //    properties.Add("License type");
        //    string supportedLicenseTypes = "Supported license types:" + string.Join(", ", GetListOfSupportedLicenseTypes());
        //    properties.Add(supportedLicenseTypes);
        //    //properties.Add("Supported license types: " + string.Join(", ", GetListOfSupportedLicenseTypes()));  
        //    properties.Add("Engine volume");
        //    properties.Add("Positive integer number");
        //    return properties;
        //}


        public List<string> GetListOfSupportedLicenseTypes()
        {
            List<string> supportedLicenseTypes = new List<string>();
            foreach (eLicenseType licenseType in Enum.GetValues(typeof(eLicenseType)))
            {
                supportedLicenseTypes.Add(licenseType.ToString());
            }

            return supportedLicenseTypes;
        }

        public override string ToString()
        {
            StringBuilder motorcycleDetails = new StringBuilder();
            motorcycleDetails.Append(base.ToString());
            motorcycleDetails.AppendLine(string.Format("License type: {0}", m_LicenseType));
            motorcycleDetails.AppendLine(string.Format("Engine volume: {0}", r_EngineVolume));
            return motorcycleDetails.ToString();
        }
    }
}
