using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        private const int k_NumOfWheels = 12;
        private bool m_CarryingDangerousMaterials;
        private float m_CargoVolume;

        //public Truck(string i_ModelName, string i_LicenseNumber, float i_CurrentEnergyPercentage, List<Wheel> i_WheelsList,
        //               bool i_IsCarryingDangerousMaterials, float i_CargoVolume)
        //    : base(i_ModelName, i_LicenseNumber, i_CurrentEnergyPercentage, i_WheelsList)
        //{
        //    m_CarryingDangerousMaterials = i_IsCarryingDangerousMaterials;
        //    m_CargoVolume = i_CargoVolume;
        //}
        
        //public Truck(bool i_IsCarryingDangerousMaterials, float i_CargoVolume)
        //{
        //    m_CarryingDangerousMaterials = i_IsCarryingDangerousMaterials;
        //    m_CargoVolume = i_CargoVolume;
        //}

        //public void SetTruckValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
        //                            float i_CurrentEnergyLevel, bool i_IsCarryingDangerousMaterials, float i_CargoVolume)
        //{
        //    try
        //    {
        //        SetVehicleValues(i_ModelName, i_LicenseNumber, i_WheelsList, i_CurrentEnergyLevel);
        //        m_CarryingDangerousMaterials = i_IsCarryingDangerousMaterials;
        //        CargoVolume = i_CargoVolume;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public Truck()
        {
            for (int i = 0; i < k_NumOfWheels; i++)
            {
                WheelsList.Add(new Wheel());
            }
        }

        public override void SetProperty(string i_propertyName, string i_PropertyValue)
        {
            try
            {
                switch
                (i_propertyName)
                {
                    case "Carrying dangerous materials":
                        {
                            if (bool.TryParse(i_PropertyValue, out bool value))
                            {
                                m_CarryingDangerousMaterials = value;
                            }
                            else
                            {
                                throw new ArgumentException("Invalid input for carrying dangerous materials. Only true or false");
                            }

                            break;
                        }
                    case "Cargo volume":
                        {
                            if (float.TryParse(i_PropertyValue, out float value))
                            {
                                CargoVolume = value;
                            }
                            else
                            {
                                throw new ArgumentException("Invalid input for cargo volume. Only positive float number");
                            }

                            break;
                        }
                    default:
                        {
                            base.SetProperty(i_propertyName, i_PropertyValue);
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
            propertiesAndValues.Add(new KeyValuePair<string, string>("Carrying dangerous materials", "True or false"));
            propertiesAndValues.Add(new KeyValuePair<string, string>("Cargo volume", "Positive float number"));

            return propertiesAndValues;
        }

        //public override List<string> GetListOfPropertiesAndPossibleValues()
        //{
        //    List<string> properties = base.GetListOfPropertiesAndPossibleValues();
        //    properties.Add("Carrying dangerous materials");
        //    properties.Add("True or false");    
        //    properties.Add("Cargo volume");
        //    properties.Add("Positive float number");

        //    return properties;
        //}


        public bool CarryingDangerousMaterials
        {
            get
            {
                return m_CarryingDangerousMaterials;
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
            set
            {
                if (value > 0)
                {
                    m_CargoVolume = value;
                }
                else
                {
                    throw new ArgumentException("Cargo volume must be greater than 0.");
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
            StringBuilder truckDetails = new StringBuilder();
            truckDetails.Append(base.ToString());
            if (CarryingDangerousMaterials)
            {
                truckDetails.AppendFormat("This truck is carrying dangerous materials{0}", Environment.NewLine);
            }
            else
            {
                truckDetails.AppendFormat("This truck is not carrying dangerous materials{0}", Environment.NewLine);
            }
            truckDetails.AppendFormat("Cargo volume: {0}{1}", m_CargoVolume, Environment.NewLine);

            return truckDetails.ToString();
        }
    }
}
