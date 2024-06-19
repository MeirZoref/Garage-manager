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

        public void SetTruckValues(string i_ModelName, string i_LicenseNumber, List<Wheel> i_WheelsList,
                                    float i_CurrentEnergyLevel, bool i_IsCarryingDangerousMaterials, float i_CargoVolume)
        {
            try
            {
                SetVehicleValues(i_ModelName, i_LicenseNumber, i_WheelsList, i_CurrentEnergyLevel);
                m_CarryingDangerousMaterials = i_IsCarryingDangerousMaterials;
                CargoVolume = i_CargoVolume;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override List<string> GetListOfProperties()
        {
            List<string> properties = base.GetListOfProperties();
            properties.Add("Carrying dangerous materials (true or false)");
            properties.Add("Cargo volume");

            return properties;
        }


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
            truckDetails.AppendFormat("Carrying dangerous materials: {0}{1}", m_CarryingDangerousMaterials, Environment.NewLine);
            truckDetails.AppendFormat("Cargo volume: {0}{1}", m_CargoVolume, Environment.NewLine);
            return truckDetails.ToString();
        }
    }
}
