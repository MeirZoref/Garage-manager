using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        private bool m_IsCarryingDangerousMaterials;
        private float m_CargoVolume;

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
        public bool CarryingDangerousMaterials
        {
            get
            {
                return m_IsCarryingDangerousMaterials;
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
                                m_IsCarryingDangerousMaterials = value;
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
